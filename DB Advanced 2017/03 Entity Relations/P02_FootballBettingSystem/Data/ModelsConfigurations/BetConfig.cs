namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class BetConfig : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> bet)
        {
            bet.HasKey(b => b.BetId);

            bet.HasOne(g => g.Game).WithMany(b => b.Bets).HasForeignKey(g => g.GameId);

            bet.HasOne(p => p.User).WithMany(b => b.Bets).HasForeignKey(u => u.UserId);
        }
    }
}
