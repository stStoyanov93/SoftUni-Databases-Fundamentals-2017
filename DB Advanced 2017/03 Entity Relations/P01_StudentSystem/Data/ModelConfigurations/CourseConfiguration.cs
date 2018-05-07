namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P01_StudentSystem.Data.Models;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> entity)
        {
            entity.HasKey(c => c.CourseId);

            entity.Property(n => n.Name).HasColumnType("NVARCHAR(80)").IsRequired(true);

            entity.Property(d => d.Description).HasColumnType("NVARCHAR(MAX)").IsRequired(false);

            entity.Property(sd => sd.StartDate).HasColumnType("DATETIME2").IsRequired(true);

            entity.Property(ed => ed.EndDate).HasColumnType("DATETIME2");

            entity.Property(p => p.Price).HasColumnType("DECIMAL(20,5)");

            entity.HasMany(s => s.StudentsEnrolled).WithOne(c => c.Course);

            entity.HasMany(r => r.Resources).WithOne(c => c.Course);

            entity.HasMany(h => h.HomeworkSubmissions).WithOne(c => c.Course);
        }
    }
}