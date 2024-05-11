using Entities;

namespace crudlab.Models.DTO.Teacher;

public class AddTeacherDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int Experience { get; set; }
    public IList<int> SpecializationIds { get; set; } = [];
}

