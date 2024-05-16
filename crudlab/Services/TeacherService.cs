using crudlab.DatabaseContext;
using crudlab.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace crudlab.Services;

public class TeacherService : IRepository<Teacher>
{
    private readonly AppDbContext _db;

    public TeacherService(AppDbContext db)
    {
        _db = db;
    }

    public async Task Add(Teacher entity)
    {
        await _db.Teachers.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var teacher = await _db.Teachers.SingleOrDefaultAsync(c => c.Id == id);
        if (teacher != null)
        {
            _db.Teachers.Remove(teacher);
            await _db.SaveChangesAsync();
        }
    }

    public Task<Teacher> Get(int id)
    {
        return _db.Teachers.Include(c => c.Subjects).SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Teacher>> Get(Expression<Func<Teacher, bool>> expression)
    {
        return await _db.Teachers.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<Teacher>> GetAll()
    {
        return await _db.Teachers.AsNoTracking().ToListAsync();
    }

    public async Task<Teacher> GetByName(string name)
    {
        return await _db.Teachers.SingleOrDefaultAsync(c => c.Name == name);
    }

    public async Task Update(int id, Teacher entity)
    {
        var teacher = await _db.Teachers
            .Include(c => c.Subjects)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (teacher is not null)
        {
            teacher.Name = entity.Name;
            teacher.Surname = entity.Surname;
            teacher.Experience = entity.Experience;
            teacher.Subjects = entity.Subjects;
            await _db.SaveChangesAsync();
        }
    }
}
