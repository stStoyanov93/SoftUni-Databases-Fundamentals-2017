namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class TownConfig : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> town)
        {
            town.HasKey(t => t.TownId);

            town.Property(n => n.Name).IsRequired().HasMaxLength(80);

            town.HasOne(c => c.Country).WithMany(t => t.Towns).HasForeignKey(c => c.CountryId);
        }
    }
}
