using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookDataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {  }

        
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; } //it will be the database name that will be created after migration. after creating this, we will push the migration to the db

        public DbSet<Product> Products { get; set; }
    }

}