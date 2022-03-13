using Biblioteka.Models.EntityMapper;
using Microsoft.EntityFrameworkCore;

namespace Biblioteka.Repository
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap())
                .ApplyConfiguration(new BookMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
