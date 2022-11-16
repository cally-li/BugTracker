using BugTracker.Data;
using BugTracker.Interfaces;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Repositories
{
    public class TicketRepository:ITicketRepository
    {
        private readonly DataContext _context;

        public TicketRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.Include(t=>t.AssignedDeveloper).Include(t=>t.Submitter).ToListAsync();
        }  
        
        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int Id)
        {
            return await _context.Tickets
                .Where(t=>t.AssignedDeveloperId==Id || t.SubmitterId==Id)
                .Include(t => t.AssignedDeveloper)
                .Include(t => t.Submitter)
                .ToListAsync();
        }  
        
        public async Task<IEnumerable<Ticket>> GetTicketsByProjectAsync(int Id)
        {
            return await _context.Tickets
                .Where(t=>t.ProjectId==Id)
                .Include(t => t.AssignedDeveloper)
                .Include(t => t.Submitter)
                .ToListAsync();
        }
    }
}
