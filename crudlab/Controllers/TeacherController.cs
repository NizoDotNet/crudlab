using crudlab.Models.DTO.Teacher;
using crudlab.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crudlab.Controllers
{
    public class TeacherController : Controller
    {
        private readonly IRepository<Teacher> _teacherService;
        private readonly IRepository<Subject> _subjectService;


        public TeacherController(IRepository<Teacher> teacherService, IRepository<Subject> subjectService)
        {
            _teacherService = teacherService;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _teacherService.GetAll();
            teachers = teachers.OrderBy(c => c.Name);
            return View(teachers);
        }

        public async Task<IActionResult> Get(int id)
        {
            var teacher = await _teacherService.Get(id);

            if(teacher is not null) return View("Teacher", teacher);
            return NotFound();
        }

        public async Task<IActionResult> Create()
        {
            var subjects = await _subjectService.GetAll();
            SelectList specsList = new(subjects, "Id", "Name");
            ViewBag.Specs = specsList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddTeacherDto teacherDto)
        {
            var teacher = new Teacher()
            {
                Name = teacherDto.Name,
                Surname = teacherDto.Surname,
                Experience = teacherDto.Experience
            };
            var subjects = await _subjectService.Get(c => teacherDto.SubjectIds.Contains(c.Id));
            teacher.Subjects = subjects;
            await _teacherService.Add(teacher);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var teacher = await _teacherService.Get(id);
            if (teacher is not null)
            {
                var teacherDto = new AddTeacherDto()
                {
                    Id = id,
                    Name = teacher.Name,
                    Surname = teacher.Surname,
                    Experience = teacher.Experience
                };
                var subjects = await _subjectService.GetAll();
                SelectList specsList = new(subjects, "Id", "Name");
                ViewBag.Specs = specsList;

                return View(teacherDto);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, AddTeacherDto teacherDto)
        {
            var teacher = new Teacher()
            {
                Name = teacherDto.Name,
                Surname = teacherDto.Surname,
                Experience = teacherDto.Experience
            };
            var subs = await _subjectService.Get(c => teacherDto.SubjectIds.Contains(c.Id));
            teacher.Subjects = subs;
            await _teacherService.Update(id, teacher);

            return RedirectToAction("Index");
        }
    }
}
