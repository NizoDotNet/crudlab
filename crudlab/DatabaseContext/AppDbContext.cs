using crudlab.DatabaseContext.Configurations;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace crudlab.DatabaseContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions op) : base(op)
    {
    }

    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new SpecializationConfig());
        modelBuilder.ApplyConfiguration(new StudentConfig());
        modelBuilder.ApplyConfiguration(new  GradeConfig());
    }

}
