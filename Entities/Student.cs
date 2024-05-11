namespace Entities;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public double Gpa { get; set; }
    public Specialization Specialization { get; set; } = null!;
    public int SpecializationId { get; set; }
}
