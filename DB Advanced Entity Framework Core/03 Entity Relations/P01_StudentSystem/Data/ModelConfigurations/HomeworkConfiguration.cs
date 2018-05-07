namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P01_StudentSystem.Data.Models;

    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> entity)
        {
            entity.HasKey(h => h.HomeworkId);

            entity.Property(c => c.Content).HasColumnType("VARCHAR(MAX)").IsRequired(false);

            entity.Property(ct => ct.ContentType).HasColumnType("INT").IsRequired(true);

            entity.Property(st => st.SubmissionTime).HasColumnType("DATETIME2");

            entity.HasOne(s => s.Student).WithMany(h => h.HomeworkSubmissions);

            entity.HasOne(c => c.Course).WithMany(h => h.HomeworkSubmissions);
        }
    }
}