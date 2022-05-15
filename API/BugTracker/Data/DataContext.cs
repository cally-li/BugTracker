using BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Data
{
    public class DataContext: DbContext
    {
        //constructor: call DbContext class
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //define DbSet properties
        public DbSet<User> Users{ get; set; }

        //configure database w/ fluent API 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
