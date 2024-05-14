using crudlab.Models.DTO.Student;
using crudlab.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crudlab.Controllers;

public class StudentController : Controller
{
    private readonly IRepository<Student> _studentService;
    private readonly IRepository<Specialization> _specService;
    private readonly IRepository<Grade> _gradeService;
    private readonly IRepository<Subject> _subjectService;
    public StudentController(IRepository<Student> studentService, ISpecializzationRepository specService, IRepository<Grade> gradeService, IRepository<Subject> subjectService)
    {
        _studentService = studentService;
        _specService = specService;
        _gradeService = gradeService;
        _subjectService = subjectService;
    }

    public async Task<IActionResult> Index()
    {
        var stundents = await _studentService.GetAll();
        return View(stundents);
    }

    public async Task<IActionResult> Create()
    {
        var specs = await _specService.GetAll();
        SelectList specsList = new(specs, "Id", "Name");
        ViewBag.Specs = specsList;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddStudentDto studentDto)
    {
        Student student = new()
        {
            Name = studentDto.Name,
            Surname = studentDto.Surname,
            SpecializationId = studentDto.SpecializationId
        };
        await _studentService.Add(student);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Get(int id)
    {
        var student = await _studentService.Get(id);
        if(student != null) return View("Student", student);
        return NotFound();
    }

    public async Task<IActionResult> Update(int id)
    {
        var student = await _studentService.Get(id);
        if (student != null)
        {
            var studentDto = new UpdateStudentDto()
            {
                Student = new AddStudentDto
                {
                    Name = student.Name,
                    Surname = student.Surname,
                    Id = id,
                    SpecializationId = student.SpecializationId,
                    Grades = student.Grades
                },
                Grade = new Models.DTO.Grade.AddGradeDto
                {
                    StudentId = id
                }
            };
            var specs = await _specService.GetAll();
            SelectList specsList = new(specs, "Id", "Name");
            ViewBag.Specs = specsList;
            var subjects = await _subjectService.GetAll();
            SelectList subjectList = new(subjects, "Id", "Name");

            ViewBag.Subjects = subjectList;  
            return View(studentDto);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, UpdateStudentDto studentDto)
    {
        Student student = new()
        {
            Name = studentDto.Student.Name,
            Surname = studentDto.Student.Surname,
            SpecializationId = studentDto.Student.SpecializationId
        };
        
        await _studentService.Update(id, student);
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> AddGrade(UpdateStudentDto gradeDto)
    {
        Grade grade = new()
        {
            StudentId = gradeDto.Grade.StudentId,
            EntryScore = gradeDto.Grade.EntryScore,
            ExamScore = gradeDto.Grade.ExamScore,
            SubjectId = gradeDto.Grade.SubjectId
        };
        await _gradeService.Add(grade);
        return RedirectToAction("Update", new {id = gradeDto.Grade.StudentId});
    }
    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.Delete(id);
        return RedirectToAction("Index");
    }
}
