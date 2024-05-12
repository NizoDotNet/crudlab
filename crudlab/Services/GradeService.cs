using crudlab.DatabaseContext;
using crudlab.Repositories;
using crudlab.Services.Helpers;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace crudlab.Services;

public class GradeService : IRepository<Grade>
{
    private readonly AppDbContext _db;
    private readonly GpaCalculator _gpa;

    public GradeService(AppDbContext db, GpaCalculator gpa)
    {
        _db = db;
        _gpa = gpa;
    }

    private async Task UpdateGpa(int studentId)
    {
        var student = await _db.Students.FirstOrDefaultAsync(c => c.Id == studentId);
        student.Gpa = _gpa.GetGpa(student.Grades);
    }

    public async Task Add(Grade entity)
    {
        await _db.Grades.AddAsync(entity);
        await UpdateGpa(entity.StudentId);
        await _db.SaveChangesAsync();

    }

    public async Task Delete(int id)
    {
        var grade = await _db.Grades.SingleOrDefaultAsync(c => c.Id == id);
        if (grade is not null)
        {
            _db.Grades.Remove(grade);
            await UpdateGpa(grade.StudentId);
            await _db.SaveChangesAsync();

        }
    }

    public Task<Grade> Get(int id)
    {
        return _db.Grades.Include(c => c.Subject).FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Grade>> Get(Expression<Func<Grade, bool>> expression)
    {
        return await _db.Grades.Include(c => c.Subject).Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<Grade>> GetAll()
    {
        return await _db.Grades.Include(c => c.Subject).ToListAsync();
    }

    public async Task<Grade> GetByName(string name)
    {
        return await _db.Grades.Include(c => c.Subject).FirstOrDefaultAsync(c => c.Subject.Name == name);
    }

    public async Task Update(int id, Grade entity)
    {
        var grade = await _db.Grades.Include(c => c.Subject).FirstOrDefaultAsync(_ => _.Id == id);
        if(grade is not null)
        {
            grade.EntryScore = entity.EntryScore;
            grade.ExamScore = entity.ExamScore;
            await UpdateGpa(grade.StudentId);
            await _db.SaveChangesAsync();
        }
    }
}
