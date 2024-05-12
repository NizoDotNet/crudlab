using Entities;

namespace crudlab.Services.Helpers;

public class GpaCalculator
{
    public double GetGpa(IList<Grade> grades) => grades.Sum(c => c.ExamScore + c.EntryScore) / grades.Count;
    
}
