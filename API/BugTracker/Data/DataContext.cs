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

        //define queryable entities/tables
        public DbSet<User> Users{ get; set; }
        public DbSet<Project> Projects{ get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Ticket> Tickets{ get; set; }
        public DbSet<TicketComment> TicketComments{ get; set; }
        public DbSet<TicketHistoryItem> TicketHistory{ get; set; }

        //configure database w/ fluent API 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //configure many-to-many relationship
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });
            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu=>pu.Project)
                .WithMany(p=>p.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectId);
            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu=>pu.User)
                .WithMany(p=>p.ProjectUsers)
                .HasForeignKey(pu => pu.UserId);

            //configure 2 FKs to User within Ticket entity
            modelBuilder.Entity<Ticket>()
                    .HasOne(t => t.Submitter)
                    .WithMany(a => a.SubmittedTickets)
                    .HasForeignKey(t => t.SubmitterId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                    .HasOne(t => t.AssignedDeveloper)
                    .WithMany(a => a.AssignedTickets)
                    .HasForeignKey(t => t.AssignedDeveloperId)
                    .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
