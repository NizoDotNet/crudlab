using crudlab.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace crudlab.Controllers;

public class FacultyController : Controller
{
    private readonly IRepository<Faculty> _facultyService;

    public FacultyController(IRepository<Faculty> facultyService)
    {
        _facultyService = facultyService;
    }
    public async Task<IActionResult> Index()
    {
        var faculties = await _facultyService.GetAll();
        faculties = faculties.OrderBy(c => c.Name);
        return View(faculties);
    }

    public async Task<IActionResult> Get(int id)
    {
        var faculty = await _facultyService.Get(id);
        if (faculty is null) return NotFound();
        return View("Faculty", faculty);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Faculty entity)
    {
        if (ModelState.IsValid)
        {
            await _facultyService.Add(entity);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    public async Task<IActionResult> Update(int id)
    {
        var faculty = await _facultyService.Get(id);
        if(faculty is not null) return View();
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, Faculty entity)
    {
        if (ModelState.IsValid)
        {
            await _facultyService.Update(id,entity);
            return RedirectToAction("Index");
        }
        return NotFound();
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _facultyService.Delete(id);
        return RedirectToAction("Index");
    }
}

