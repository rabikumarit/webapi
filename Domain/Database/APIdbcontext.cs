using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebAPI.Domain.Module;
namespace WebAPI.Domain.Database
{
    public class APIdbcontext:DbContext
    {
        public APIdbcontext(DbContextOptions options):base (options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
        }

    }
}
