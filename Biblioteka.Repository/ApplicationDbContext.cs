using Biblioteka.Models;
using Biblioteka.Models.EntityMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace Biblioteka.Repository
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<ApplicationDbContextOptions> dbOptions) : base(options)
        {
            _dbOptions = dbOptions;
        }

        private IOptions<ApplicationDbContextOptions> _dbOptions;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(_dbOptions.Value.MySql);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new BookMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
