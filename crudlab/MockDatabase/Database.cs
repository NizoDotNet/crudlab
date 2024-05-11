using Entities;

namespace crudlab.MockDatabase;

public static class Database
{
    public static List<Faculty> Faculties { get; set; } = [new() { Id = 1, Name = "Muhendislik" }, new() { Id = 2, Name = "Riyaziyyat ve Fizika" }];
    public static List<Specialization> Specializations { get; set; } = [];
    public static List<Teacher> Teachers { get; set; }               = [];
    public static List<Student> Students { get; set; }               = [];
}
