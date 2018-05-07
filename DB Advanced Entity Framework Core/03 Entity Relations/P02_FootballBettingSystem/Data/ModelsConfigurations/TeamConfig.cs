namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team.HasKey(t => t.TeamId);

            team.Property(url => url.LogoUrl).HasColumnType("varchar(max)");

            team.Property(n => n.Name).IsRequired(true).HasColumnType("nvarchar(80)");

            team.Property(i => i.Initials).HasColumnType("nchar(3)");

            team.HasOne(pc => pc.PrimaryKitColor).WithMany(t => t.PrimaryKitTeams).HasForeignKey(pc => pc.PrimaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            team.HasOne(sc => sc.SecondaryKitColor).WithMany(t => t.SecondaryKitTeams).HasForeignKey(sc => sc.SecondaryKitColorId).OnDelete(DeleteBehavior.Restrict);

            team.HasOne(tn => tn.Town).WithMany(t => t.Teams).HasForeignKey(tn => tn.TownId);
        }
    }
}
