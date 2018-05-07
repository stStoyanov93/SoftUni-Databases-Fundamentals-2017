namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerStatisticConfig : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> playerStatistic)
        {
            playerStatistic.HasKey(k => new { k.GameId, k.PlayerId });

            playerStatistic.HasOne(g => g.Game).WithMany(gs => gs.PlayerStatistics).HasForeignKey(g => g.GameId);

            playerStatistic.HasOne(p => p.Player).WithMany(ps => ps.PlayerStatistics).HasForeignKey(p => p.PlayerId);
        }
    }
}
