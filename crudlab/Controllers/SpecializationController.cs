using crudlab.Models.DTO.Spec;
using crudlab.Models.DTO.Subject;
using crudlab.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crudlab.Controllers;

public class SpecializationController : Controller
{
    private readonly ISpecializzationRepository _specService;
    private readonly IRepository<Faculty> _facultyService;
    private readonly IRepository<Subject> _subjectService;

    public SpecializationController(ISpecializzationRepository specService, IRepository<Faculty> facultyService,
         IRepository<Subject> subjectService)
    {
        _specService = specService;
        _facultyService = facultyService;
        _subjectService = subjectService;
    }

    public async Task<IActionResult> Index()
    {
        var specs = await _specService.GetAll();
        specs = specs.OrderBy(c => c.Name);
        return View(specs);
    }

    
    public async Task<IActionResult> Get(int id)
    {
        var spec = await _specService.Get(id);
        if(spec != null) return View("Specialization", spec);
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var faculties = await _facultyService.GetAll();
        SelectList facultiesList = new(faculties, "Id", "Name");
        ViewBag.Faculties = facultiesList;
        var subjects = await _subjectService.GetAll();
        SelectList subjectList = new(subjects, "Id", "Name");
        ViewBag.Subjects = subjectList;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddSpecDto specDto)
    {
        Specialization specialization = new()
        {
            Name = specDto.Name,
            FacultyId = specDto.FacultyId,
        };
        var subs = await _subjectService.Get(c => specDto.SubjectIds.Contains(c.Id));
        specialization.Subjects = subs;
        await _specService.Add(specialization);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _specService.Delete(id);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var spec = await _specService.Get(id);
        if (spec != null)
        {
            var faculties = await _facultyService.GetAll();
            SelectList facultiesList = new(faculties, "Id", "Name");
            ViewBag.Faculties = facultiesList;
            var subjects = await _subjectService.GetAll();
            SelectList subjectList = new(subjects, "Id", "Name");
            ViewBag.Subjects = subjectList;
            return View();
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, AddSpecDto specDto)
    {
        Specialization specialization = new()
        {
            Name = specDto.Name,
            FacultyId = specDto.FacultyId,
        };
        var subs = await _subjectService.Get(c => specDto.SubjectIds.Contains(c.Id));
        specialization.Subjects = subs;
        await _specService.Update(id, specialization);
        return RedirectToAction("Index");
    }
}


