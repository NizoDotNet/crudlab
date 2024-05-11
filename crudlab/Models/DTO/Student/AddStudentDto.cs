﻿using Entities;

namespace crudlab.Models.DTO.Student;

public class AddStudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public double Gpa { get; set; }
    public int SpecializationId { get; set; }
}