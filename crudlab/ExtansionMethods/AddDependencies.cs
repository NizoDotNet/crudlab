using crudlab.Repositories;
using crudlab.Services;
using crudlab.Services.Helpers;
using Entities;
using Microsoft.AspNetCore.WebSockets;

namespace crudlab.ExtantionMethods;

public static class AddDependencies
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Faculty>, FacultyService>();
        services.AddScoped<ISpecializzationRepository,  SpecializationService>();  
        services.AddScoped<IRepository<Teacher>, TeacherService>();
        services.AddScoped<IRepository<Student>, StudentService>();
        services.AddScoped<IRepository<Grade>, GradeService>();
        services.AddScoped<IRepository<Subject>, SubjectService>();
        services.AddSingleton<GpaCalculator>();
    }
}
