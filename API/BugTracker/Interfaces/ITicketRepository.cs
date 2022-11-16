using BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Interfaces
{
    public interface ITicketRepository
    {
        //get all tickets
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();

        //get tickets by user
        Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int Id);

        //get tickets by project
        Task<IEnumerable<Ticket>> GetTicketsByProjectAsync(int Id);



    }
}
