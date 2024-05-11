using crudlab.DatabaseContext;
using crudlab.Repositories;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace crudlab.Services;

public class SpecializationService : ISpecializzationRepository
{
    private readonly AppDbContext _db;

    public SpecializationService(AppDbContext db)
    {
        _db = db;
    }

    public async Task Add(Specialization entity)
    {
        await _db.Specializations.AddAsync(entity);
        await _db.SaveChangesAsync();

    }

    public async Task Delete(int id)
    {
        var spec = await _db.Specializations
            .Include(c => c.Students)
            .Include(c => c.Teachers)
            .SingleOrDefaultAsync(c => c.Id == id);
        
        if (spec is not null)
        {
            _db.Specializations.Remove(spec);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<Specialization> Get(int id)
    {
        return await _db.Specializations
            .Include(c => c.Teachers)
            .Include(c => c.Students)
            .Include(c => c.Faculty)
            .SingleOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Specialization>> Get(Expression<Func<Specialization, bool>> expression)
    {
        return await _db.Specializations.Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<Specialization>> GetAll()
    {
        return await _db.Specializations.Include(c => c.Faculty).AsNoTracking().ToListAsync();
    }

    public async Task<Specialization> GetByName(string name)
    {
        return await _db.Specializations
            .Include(c => c.Students)
            .Include(c => c.Teachers)
            .Include(c => c.Faculty)
            .FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task Update(int id, Specialization entity)
    {
        var spec = await _db.Specializations.FirstOrDefaultAsync(c => c.Id == id);
        if (spec is not null)
        {
            spec.Name = entity.Name;
            spec.FacultyId = entity.FacultyId;
            await _db.SaveChangesAsync();
        }
    }
}
