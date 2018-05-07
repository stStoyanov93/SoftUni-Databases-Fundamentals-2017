namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game.HasKey(g => g.GameId);

            game.HasOne(ht => ht.HomeTeam).WithMany(hg => hg.HomeGames).HasForeignKey(ht => ht.HomeTeamId).OnDelete(DeleteBehavior.Restrict);

            game.HasOne(at => at.AwayTeam).WithMany(hg => hg.AwayGames).HasForeignKey(at => at.AwayTeamId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
