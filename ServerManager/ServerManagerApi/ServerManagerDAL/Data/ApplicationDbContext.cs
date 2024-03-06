using Microsoft.EntityFrameworkCore;
using ServerManagerCore.Models;

namespace ServerManagerDAL.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Request> Requests { get; set; }
    }
}
