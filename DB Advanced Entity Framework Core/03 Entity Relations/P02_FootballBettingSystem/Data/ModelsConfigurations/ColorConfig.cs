namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class ColorConfig : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> color)
        {
            color.HasKey(c => c.ColorId);

            color.Property(n => n.Name).HasColumnType("varchar(40)").IsRequired(true);
        }
    }
}
