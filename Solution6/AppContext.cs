using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Solution6
{
    public class AppContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Book> Books { get; set; }
        

        public void Delete()
        {
            Database.EnsureDeleted();
        }

        public void Create()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=EF;User Id=SA;Password=Bek6776326;TrustServerCertificate=true;");
        }
    }
}
