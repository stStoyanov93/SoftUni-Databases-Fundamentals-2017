namespace P01_StudentSystem.Data.ModelConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P01_StudentSystem.Data.Models;

    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> entity)
        {
            entity.HasKey(r => r.ResourceId);

            entity.Property(n => n.Name).HasColumnType("NVARCHAR(50)").IsRequired(true);

            entity.Property(u => u.Url).HasColumnType("VARCHAR(MAX)").IsRequired(false);

            entity.Property(rt => rt.ResourceType)
                .HasColumnType("INT").IsRequired(true);

            entity.HasOne(c => c.Course).WithMany(r => r.Resources);
        }
    }
}