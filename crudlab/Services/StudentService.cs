using crudlab.DatabaseContext;
using crudlab.MockDatabase;
using crudlab.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace crudlab.Services;

public class StudentService : IRepository<Student>
{
    private readonly AppDbContext _db;
    public StudentService(AppDbContext db)
    {
        _db = db;
    }

    public async Task Add(Student entity)
    {
        await _db.Students.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var student = await _db.Students.SingleOrDefaultAsync(c => c.Id == id);
        if (student is not null)
        {
            _db.Students.Remove(student);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<Student> Get(int id)
    {
        return await _db.Students.Include(c => c.Specialization).SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Student>> Get(Expression<Func<Student, bool>> expression)
    {
        return await _db.Students.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        return await _db.Students.Include(c => c.Specialization).AsNoTracking().ToListAsync();
    }

    public async Task<Student> GetByName(string name)
    {
        return await _db.Students.Include(c => c.Specialization).SingleOrDefaultAsync(c => c.Name == name);
    }

    public async Task Update(int id, Student entity)
    {
        var student = await _db.Students.Include(c => c.Specialization).SingleOrDefaultAsync(c => c.Id == id);
        if (student is not null)
        {
            student.Name = entity.Name;
            student.Surname = entity.Surname;
            student.Specialization = entity.Specialization;
            student.Gpa = entity.Gpa;
            await _db.SaveChangesAsync();
        }
    }
}
