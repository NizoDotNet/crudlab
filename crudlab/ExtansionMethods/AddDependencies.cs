using crudlab.Repositories;
using crudlab.Services;
using Entities;

namespace crudlab.ExtantionMethods;

public static class AddDependencies
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Faculty>, FacultyService>();
        services.AddScoped<ISpecializzationRepository,  SpecializationService>();  
        services.AddScoped<IRepository<Teacher>, TeacherService>();
        services.AddScoped<IRepository<Student>, StudentService>();
    }
}
