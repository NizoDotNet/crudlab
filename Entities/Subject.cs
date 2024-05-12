namespace Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IList<Grade> Grades { get; set; } = [];
    public IList<Specialization> Specializations { get; set; } = [];
    public IList<Teacher> Teachers { get; set; } = [];
}
