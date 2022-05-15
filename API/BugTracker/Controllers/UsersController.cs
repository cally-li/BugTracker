
using AutoMapper;
using BugTracker.DTOs;
using BugTracker.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        //get all users 
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users= await _userRepository.GetAllUsers();
            var usersToReturn = _mapper.Map<List<AllUsersDto>>(users);
            return Ok(usersToReturn);  

        }

        //get user by id ; api/users/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUser(id);
            var userToReturn = _mapper.Map<AllUsersDto>(user);
            return Ok(userToReturn);
        }
    }
}
