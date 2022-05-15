using BugTracker.DTOs;
using BugTracker.Interfaces;
using BugTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class AccountController:BaseApiController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountRepository accountRepository, ITokenService tokenService)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
        }
    

        //register a new user (api/account/register)
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            //validation: check if email already exists in db
            if (await _accountRepository.EmailExists(registerDto.Email)) 
                return BadRequest("There is already an account associated with this email.");
            
            //register the user - returns UserDto obj with email and JWT token
            var user = await _accountRepository.Register(registerDto);
            return Ok(user);
        }
       
        //login 
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            //validate email exists in db
            if (!await _accountRepository.EmailExists(loginDto.Email)) 
                return Unauthorized("Invalid email address.");

            //locate user in db
            var user = await _accountRepository.Login(loginDto);

            //verify password
            if (!_accountRepository.CorrectPassword(user, loginDto))
                return Unauthorized("Invalid password.");

            //if password valid, return email and JWT token to client
            var userToReturn = new UserDto
            {
                FirstName = user.FirstName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };

            return Ok(userToReturn);
           
            

        }
    }
}
