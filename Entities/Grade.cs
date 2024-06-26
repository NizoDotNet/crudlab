﻿namespace Entities;

public class Grade
{
    public int Id { get; set; }
    public Subject Subject { get; set; }
    public int SubjectId { get; set; }
    public int EntryScore { get; set; }
    public int ExamScore { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
}