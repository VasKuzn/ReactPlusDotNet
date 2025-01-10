using Microsoft.EntityFrameworkCore;
using ReactPlusDotNet.Server.Models;

namespace ReactPlusDotNet.Server.DataContext
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
