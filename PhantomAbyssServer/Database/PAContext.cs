using Microsoft.EntityFrameworkCore;
using PhantomAbyssServer.Database.Models;

namespace PhantomAbyssServer.Database
{
    public class PAContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<SavedRun> SavedRuns { get; set; }

        public PAContext(DbContextOptions<PAContext> options) : base(options)
        {
        }
    }
}