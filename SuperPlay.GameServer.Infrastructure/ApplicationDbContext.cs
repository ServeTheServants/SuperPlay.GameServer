using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SuperPlay.GameServer.Application.Common.Interfaces;
using SuperPlay.GameServer.Domain.Entities;

namespace SuperPlay.GameServer.Infrastructure
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Player> Players =>
            Set<Player>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Player>().HasData(
                new Player { PlayerId = 1, DeviceId = "1" },
                new Player { PlayerId = 2, DeviceId = "2" }
            );

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}