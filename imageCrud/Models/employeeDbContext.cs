using Microsoft.EntityFrameworkCore;

namespace imageCrud.Models
{
    public class employeeDbContext : DbContext
    {
        public employeeDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
