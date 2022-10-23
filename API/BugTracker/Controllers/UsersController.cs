
using AutoMapper;
using BugTracker.DTOs;
using BugTracker.Interfaces;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users= await _userRepository.GetAllUsersAsync();
            var usersToReturn = _mapper.Map<List<UsersDetailDto>>(users);
            return Ok(usersToReturn);  

        }

       
        //get user by email
        [HttpGet("{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var userToReturn = _mapper.Map<UsersDetailDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            //get user's email from the claim in token
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await _userRepository.GetUserByEmailAsync(email);

            _mapper.Map(userUpdateDto, user);
            
            //flag to avoid errors upon consecutive updates
            _userRepository.Update(user);

            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("Failed to update user.");



        }
    }
}
