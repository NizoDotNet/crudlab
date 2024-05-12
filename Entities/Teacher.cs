namespace Entities;

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int Experience { get; set; }
    public IList<Subject> Subjects { get; set; } = [];
}
