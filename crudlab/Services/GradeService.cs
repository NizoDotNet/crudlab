using crudlab.DatabaseContext;
using crudlab.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace crudlab.Services;

public class GradeService : IRepository<Grade>
{
    private readonly AppDbContext _db;

    public GradeService(AppDbContext db)
    {
        _db = db;
    }

    public async Task Add(Grade entity)
    {
        await _db.Grades.AddAsync(entity);
        await _db.SaveChangesAsync();

    }

    public async Task Delete(int id)
    {
        var grade = await _db.Grades.SingleOrDefaultAsync(c => c.Id == id);
        if (grade is not null)
        {
            _db.Grades.Remove(grade);
            await _db.SaveChangesAsync();

        }
    }

    public Task<Grade> Get(int id)
    {
        return _db.Grades.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Grade>> Get(Expression<Func<Grade, bool>> expression)
    {
        return await _db.Grades.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<Grade>> GetAll()
    {
        return await _db.Grades.ToListAsync();
    }

    public async Task<Grade> GetByName(string name)
    {
        return await _db.Grades.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task Update(int id, Grade entity)
    {
        var grade = await _db.Grades.FirstOrDefaultAsync(_ => _.Id == id);
        if(grade is not null)
        {
            grade.Name = entity.Name;
            grade.EntryScore = entity.EntryScore;
            grade.ExamScore = entity.ExamScore;
            grade.StudentId = entity.StudentId;
            await _db.SaveChangesAsync();
        }
    }
}
