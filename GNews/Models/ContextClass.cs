using System.Data.Entity;

namespace GNews.Models
{
    public class ContextClass: DbContext
    {
        public DbSet<New> News { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Request> Requests { get; set; }
    }
}