using Microsoft.EntityFrameworkCore;

namespace ifood_core_api_7.Models
{
    public class MyDBContext: DbContext
    {

        public MyDBContext(DbContextOptions<MyDBContext> options):base (options) { 
        
        }

        public DbSet<Users> users { get; set; }
    }
}
