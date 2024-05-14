using crudlab.DatabaseContext;
using crudlab.ExtantionMethods;
using crudlab.Migrations;
using crudlab.Models.DTO.Grade;
using crudlab.Repositories;
using crudlab.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddServices();
builder.Services.AddDbContext<AppDbContext>(c => c.UseMySQL(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/grade", async (IRepository<Grade> gradeService) =>
{
    var grades = await gradeService.GetAll();
    grades = grades.ToList();
    return Results.Ok(grades);
});

app.MapPost("/grade", async (IRepository<Grade> gradeSercice, [FromBody] AddGradeDto gradeDto) =>
{
    var grade = new Grade
    {
        EntryScore = gradeDto.EntryScore,
        ExamScore = gradeDto.ExamScore,
        SubjectId = gradeDto.SubjectId
    };
    await gradeSercice.Add(grade);
    return Results.Created();
});

app.MapPatch("/grade/{id}", async (IRepository<Grade> gradeService, [FromRoute] int id, [FromBody] AddGradeDto gradeDto) =>
{
    var grade = new Grade
    {
        EntryScore = gradeDto.EntryScore,
        ExamScore = gradeDto.ExamScore,
        SubjectId = gradeDto.SubjectId
    };
    await gradeService.Update(id, grade);
});

app.MapDelete("/grade/{id}", async (IRepository<Grade> gradeService, [FromRoute] int id) =>
{
    var grade = await gradeService.Get(id);
    return Results.Ok(grade);
});
app.Run();
