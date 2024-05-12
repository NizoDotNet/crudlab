namespace Entities;

public class Specialization
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Faculty Faculty { get; set; } = null!;
    public int FacultyId { get; set; }

    public IList<Student> Students { get; set; } = [];
    public IList<Subject> Subjects { get; set; } = [];
}
