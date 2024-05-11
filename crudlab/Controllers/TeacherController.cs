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
        private readonly ISpecializzationRepository _specService;


        public TeacherController(IRepository<Teacher> teacherService, ISpecializzationRepository specService)
        {
            _teacherService = teacherService;
            _specService = specService;
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
            var specs = await _specService.GetAll();
            SelectList specsList = new(specs, "Id", "Name");
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
            var specs = await _specService.Get(c => teacherDto.SpecializationIds.Contains(c.Id));
            teacher.Specializations = specs;
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
            var teacherDto = new AddTeacherDto()
            {
                Id = id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                Experience = teacher.Experience
            };
            var specs = await _specService.GetAll();
            SelectList specsList = new(specs, "Id", "Name");
            ViewBag.Specs = specsList;  

            return View(teacherDto);
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
            var specs = await _specService.Get(c => teacherDto.SpecializationIds.Contains(c.Id));
            teacher.Specializations = specs;
            await _teacherService.Update(id, teacher);

            return RedirectToAction("Index");
        }
    }
}
