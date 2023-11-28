using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Infrastructure.Configurations
{
	public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder
                .ToTable("Player")
                .HasKey(x => x.PlayerId);

            builder.Property(x => x.PlayerId).IsRequired().HasColumnName("PlayerId");

            builder.Property(x => x.DeviceId).IsRequired().HasColumnName("DeviceId");

            builder.Property(x => x.Coins).IsRequired().HasColumnName("Coins");

            builder.Property(x => x.Rolls).IsRequired().HasColumnName("Rolls");
        }
    }
}