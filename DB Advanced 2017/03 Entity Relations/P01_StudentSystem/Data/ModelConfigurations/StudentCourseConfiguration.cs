namespace P01_StudentSystem.Data.ModelConfigurations
{
    using P01_StudentSystem.Data.Models;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> entity)
        {
            entity.HasKey(k => new { k.StudentId, k.CourseId });

            entity.HasOne(c => c.Course).WithMany(s => s.StudentsEnrolled);

            entity.HasOne(s => s.Student).WithMany(c => c.CourseEnrollments);
        }
    }
}
