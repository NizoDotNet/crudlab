using crudlab.DatabaseContext;
using crudlab.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace crudlab.Services;

public class SubjectService : IRepository<Subject>
{
    private readonly AppDbContext _db;

    public SubjectService(AppDbContext db)
    {
        _db = db;
    }

    public async Task Add(Subject entity)
    {
        await _db.Subjects.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var sub = await _db.Subjects.SingleOrDefaultAsync(c => c.Id == id);
        if(sub is not null)
        {
            _db.Subjects.Remove(sub);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<Subject> Get(int id)
    {
        return await _db.Subjects.Include(c => c.Specializations).Include(c => c.Teachers).SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Subject>> Get(Expression<Func<Subject, bool>> expression)
    {
        return await _db.Subjects.Include(c => c.Specializations).Include(c => c.Teachers).Where(expression).ToListAsync();

    }

    public async Task<IEnumerable<Subject>> GetAll()
    {
        return await _db.Subjects.Include(c => c.Specializations).AsNoTracking().ToListAsync();
    }

    public async Task<Subject> GetByName(string name)
    {
        return await _db.Subjects.Include(c => c.Specializations).Include(c => c.Teachers).FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task Update(int id, Subject entity)
    {
        var sub = await _db.Subjects.Include(c => c.Specializations).SingleOrDefaultAsync(c => c.Id == id); 
        if(sub is not null)
        {
            sub.Name = entity.Name;
            sub.Specializations = entity.Specializations;
            await _db.SaveChangesAsync();
        }
    }
}
