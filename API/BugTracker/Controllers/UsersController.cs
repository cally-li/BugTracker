
using AutoMapper;
using BugTracker.DTOs;
using BugTracker.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    //require authorization (login) for all methods in this class
    [Authorize]
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
            var users= await _userRepository.GetAllUsersAsync();
            var usersToReturn = _mapper.Map<List<UserDetailDto>>(users);
            return Ok(usersToReturn);  

        }

        //get user by id ; api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var userToReturn = _mapper.Map<UserDetailDto>(user);
            return Ok(userToReturn);
        }
        
        //get user by email
        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var userToReturn = _mapper.Map<UserDetailDto>(user);
            return Ok(userToReturn);
        }
    }
}
