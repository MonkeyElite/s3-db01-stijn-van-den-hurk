using Microsoft.EntityFrameworkCore;
using ServerManagerCore.Models;

namespace ServerManagerDAL.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SessionUser> SessionUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SessionUser>()
                .HasKey(su => new { su.SessionId, su.UserId });

            modelBuilder.Entity<SessionUser>()
                .HasOne(su => su.Session)
                .WithMany(s => s.SessionUsers)
                .HasForeignKey(su => su.SessionId);

            modelBuilder.Entity<SessionUser>()
                .HasOne(su => su.User)
                .WithMany(u => u.SessionUsers)
                .HasForeignKey(su => su.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
