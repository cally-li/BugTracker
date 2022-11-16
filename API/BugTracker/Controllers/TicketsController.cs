using AutoMapper;
using BugTracker.DTOs;
using BugTracker.Interfaces;
using BugTracker.Models;
using BugTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    //require authorization (login) for all methods in this class
    [Authorize]
    public class TicketsController: BaseApiController
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public TicketsController(ITicketRepository ticketRepository, IUserRepository userRepository,IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        //get all tickets 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            var ticketsToReturn = _mapper.Map<List<TicketDto>>(tickets);
            return Ok(ticketsToReturn);

        }

        //get tickets by user (/tickets/all/email)
        [HttpGet("all/{email}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByUser(string email)
        {

            var user = await _userRepository.GetUserByEmailAsync(email);
            var userId = user.Id;
            var tickets = await _ticketRepository.GetTicketsByUserAsync(userId);
            var ticketsToReturn = _mapper.Map<List<TicketDto>>(tickets);
            return Ok(ticketsToReturn);

        
        }

        //get tickets by user (/tickets/project/projectId)
        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTicketsByProject(int projectId)
        {
            var tickets = await _ticketRepository.GetTicketsByProjectAsync(projectId);
            var ticketsToReturn = _mapper.Map<List<TicketDto>>(tickets);
            return Ok(ticketsToReturn);


        }
    }

}
