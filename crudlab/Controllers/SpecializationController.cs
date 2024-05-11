using crudlab.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crudlab.Controllers;

public class SpecializationController : Controller
{
    private readonly ISpecializzationRepository _specService;
    private readonly IRepository<Faculty> _facultyService;


    public SpecializationController(ISpecializzationRepository specService, IRepository<Faculty> facultyService)
    {
        _specService = specService;
        _facultyService = facultyService;
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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Specialization specialization)
    {
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
            return View();
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Specialization entity)
    {
        await _specService.Update(id, entity);
        return RedirectToAction("Index");
    }
}


