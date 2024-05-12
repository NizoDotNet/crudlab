using Entities;

namespace crudlab.Models.DTO.Spec;

public class AddSpecDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int FacultyId { get; set; }
    public IList<int> SubjectIds { get; set; } = [];
}
