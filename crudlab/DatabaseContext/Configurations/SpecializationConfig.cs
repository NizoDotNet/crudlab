using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace crudlab.DatabaseContext.Configurations;

public class SpecializationConfig : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasOne(c => c.Faculty)
            .WithMany(c => c.Specializations)
            .IsRequired();

        builder.HasMany(c => c.Students)
            .WithOne(c => c.Specialization);

        builder.HasMany(c => c.Teachers)
            .WithMany(c => c.Specializations);
    }
}
