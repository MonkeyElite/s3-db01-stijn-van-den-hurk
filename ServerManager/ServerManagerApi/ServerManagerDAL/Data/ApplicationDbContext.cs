using Microsoft.EntityFrameworkCore;
using ServerManagerCore.Models;

namespace ServerManagerDAL.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Request> Requests { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Server> Servers { get; set; }
    }
}
