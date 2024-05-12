using Entities;

namespace crudlab.Models.DTO.Subject;

public class AddSubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public IList<int> SpecializationIds { get; set; } = [];
    
}
