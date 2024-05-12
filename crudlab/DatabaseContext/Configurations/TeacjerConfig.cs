using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crudlab.DatabaseContext.Configurations;

public class TeacjerConfig : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasMany(c => c.Subjects)
            .WithMany(c => c.Teachers);
    }
}
