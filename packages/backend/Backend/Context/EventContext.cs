using Backend.Entities;

using Microsoft.EntityFrameworkCore;

namespace Backend.Context
{
    public class EventContext(DbContextOptions<EventContext> options) : DbContext(options)
    {
        public DbSet<EventLog> EventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventLog>().HasIndex(e => e.Timestamp);
        }
    }
}
