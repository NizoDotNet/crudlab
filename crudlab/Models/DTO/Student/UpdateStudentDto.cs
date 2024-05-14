using crudlab.Models.DTO.Grade;

namespace crudlab.Models.DTO.Student;

public class UpdateStudentDto
{
    public AddStudentDto Student { get; set; }
    public AddGradeDto Grade { get; set; }
}
