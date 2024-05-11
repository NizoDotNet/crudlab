using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crudlab.DatabaseContext.Configurations;

public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasMany(c => c.Grades)
            .WithOne(c => c.Student)
            .IsRequired();
    }
}
