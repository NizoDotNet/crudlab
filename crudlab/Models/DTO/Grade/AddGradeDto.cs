namespace crudlab.Models.DTO.Grade;

public class AddGradeDto
{
    public int StudentId { get; set; }
    public int SubjectId { get; set; }
    public int EntryScore { get; set; }
    public int ExamScore { get; set; }
}
