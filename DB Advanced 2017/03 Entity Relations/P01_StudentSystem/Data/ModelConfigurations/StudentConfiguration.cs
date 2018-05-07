namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P01_StudentSystem.Data.Models;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.HasKey(s => s.StudentId);

            entity.Property(n => n.Name).HasColumnType("NVARCHAR(100)").IsRequired(true);

            entity.Property(n => n.PhoneNumber).HasColumnType("CHAR(10)").IsRequired(false);

            entity.Property(r => r.RegisteredOn).HasColumnType("DATETIME2");

            entity.Property(b => b.Birthday).HasColumnType("DATETIME2");

            entity.HasMany(c => c.CourseEnrollments).WithOne(s => s.Student);

            entity.HasMany(h => h.HomeworkSubmissions).WithOne(s => s.Student);
        }
    }
}