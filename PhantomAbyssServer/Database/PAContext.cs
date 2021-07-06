using Microsoft.EntityFrameworkCore;
using PhantomAbyssServer.Database.Models;

namespace PhantomAbyssServer.Database
{
    public class PAContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SavedRun> SavedRuns { get; set; }
        public DbSet<DungeonLayout> DungeonLayouts { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Dungeon> Dungeons { get; set; }

        public PAContext(DbContextOptions<PAContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.CurrentRoute).WithOne(r => r.CurrentUser);
        }
    }
}