namespace P03_FootballBetting.Data.ModelsConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using P03_FootballBetting.Data.Models;

    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(u => u.UserId);

            user.Property(p => p.Password).HasColumnType("varchar(30)");

            user.Property(e => e.Email).HasColumnType("varchar(60)");
        }
    }
}
