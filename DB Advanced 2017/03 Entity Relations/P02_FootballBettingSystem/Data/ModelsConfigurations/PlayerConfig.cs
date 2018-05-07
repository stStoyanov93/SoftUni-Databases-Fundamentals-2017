namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class PlayerConfig : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> player)
        {
            player.HasKey(p => p.PlayerId);

            player.Property(n => n.Name).HasColumnType("nvarchar(100)").IsRequired(true);

            player.Property(sn => sn.SquadNumber).IsRequired(true);

            player.Property(i => i.IsInjured).HasColumnType("BIT").HasDefaultValue(false);

            player.HasOne(t => t.Team).WithMany(x => x.Players).HasForeignKey(t => t.TeamId);

            player.HasOne(p => p.Position).WithMany(x => x.Players).HasForeignKey(p => p.PositionId);
        }
    }
}
