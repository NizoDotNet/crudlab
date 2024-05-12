﻿using crudlab.Models.DTO.Student;
using crudlab.Repositories;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace crudlab.Controllers;

public class StudentController : Controller
{
    private readonly IRepository<Student> _studentService;
    private readonly IRepository<Specialization> _specService;

    public StudentController(IRepository<Student> studentService, ISpecializzationRepository specService)
    {
        _studentService = studentService;
        _specService = specService;
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
            var studentDto = new AddStudentDto()
            {
                Name = student.Name,
                Surname =student.Surname,
                Id = id,
                SpecializationId = student.SpecializationId
            };
            var specs = await _specService.GetAll();
            SelectList specsList = new(specs, "Id", "Name");
            ViewBag.Specs = specsList;
            return View(studentDto);
        }
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, AddStudentDto studentDto)
    {
        Student student = new()
        {
            Name = studentDto.Name,
            Surname = studentDto.Surname,
            SpecializationId = studentDto.SpecializationId
        };
        await _studentService.Update(id, student);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _studentService.Delete(id);
        return RedirectToAction("Index");
    }
}
