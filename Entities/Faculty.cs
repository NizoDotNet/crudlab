namespace Entities;

public class Faculty
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IList<Specialization> Specializations { get; set; } = [];
}
