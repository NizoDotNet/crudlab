using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crudlab.DatabaseContext.Configurations;

public class GradeConfig : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.HasOne(c => c.Subject)
            .WithMany(c => c.Grades)
            .HasForeignKey(c => c.SubjectId)
            .IsRequired();

        builder.HasOne(c => c.Student)
            .WithMany(c => c.Grades)
            .HasForeignKey(c => c.StudentId)
            .IsRequired();
    }
}
