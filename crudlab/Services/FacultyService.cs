using crudlab.DatabaseContext;
using crudlab.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace crudlab.Services;

public class FacultyService : IRepository<Faculty>
{
    private readonly AppDbContext _db;

    public FacultyService(AppDbContext db)
    {
        _db = db;
    }
    public async Task Add(Faculty entity)
    {
        await _db.Faculties.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var faculty = await _db.Faculties
            .Include(c => c.Specializations)
            .Include(c => c.Specializations)
            .ThenInclude(c => c.Students)
            .SingleOrDefaultAsync(c => c.Id == id);
        if (faculty is not null)
        {
            _db.Faculties.Remove(faculty);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<Faculty> Get(int id)
    {
        return await _db.Faculties
            .Include(c => c.Specializations)
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public Task<List<Faculty>> Get(Expression<Func<Faculty, bool>> expression)
    {
        return _db.Faculties.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<Faculty>> GetAll()
    {
        return await _db.Faculties.AsNoTracking().ToListAsync();
    }

    public async Task<Faculty> GetByName(string name)
    {
        return await _db.Faculties
                .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task Update(int id, Faculty entity)
    {
        var faculty = await _db.Faculties.FirstOrDefaultAsync(c => c.Id == id);
        if (faculty is not null)
        {
            faculty.Name = entity.Name;
            await _db.SaveChangesAsync();
        }
    }
}
