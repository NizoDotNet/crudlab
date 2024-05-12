using crudlab.Models.DTO.Subject;
using crudlab.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace crudlab.Controllers;

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
        Subject subject = new() { Name = subjectDto.Name };
        await _subjectService.Add(subject);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Get(int id)
    {
        var sub = await _subjectService.Get(id);
        if (sub != null)
        {
            return View("Subject", sub);
        }
        return NotFound();
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _subjectService.Delete(id);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var sub = await _subjectService.Get(id);
        if (sub != null)
        {
            var subDto = new AddSubjectDto()
            {
                Name = sub.Name
            };
            return View(subDto);
        }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Update(int id, AddSubjectDto subjectDto)
    {
        Subject subject = new()
        {
            Name = subjectDto.Name
        };
        var specs = await _specService.Get(c => subjectDto.SpecializationIds.Contains(c.Id));
        subject.Specializations = specs;
        await _subjectService.Update(id, subject);
        return RedirectToAction("Index");
    }
}
