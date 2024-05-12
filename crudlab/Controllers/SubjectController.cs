using crudlab.Models.DTO.Subject;
using crudlab.Repositories;
using crudlab.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crudlab.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IRepository<Subject> _subjectService;
        private readonly ISpecializzationRepository _specService;


        public SubjectController(IRepository<Subject> subjectService, ISpecializzationRepository specService)
        {
            _subjectService = subjectService;
            _specService = specService;
        }

        public async Task<IActionResult> Index()
        {
            var subs = await _subjectService.GetAll();
            return View(subs);
        }

        public async Task<IActionResult> Create()
        {
            var specs = await _specService.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddSubjectDto subjectDto)
        {
            Subject subject = new() {Name = subjectDto.Name };
            await _subjectService.Add(subject);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Get(int id)
        {
            var sub = await _subjectService.Get(id);
            if(sub != null)
            {
                return View("Subject", sub);
            }
            return NotFound();
        }
    }
}
