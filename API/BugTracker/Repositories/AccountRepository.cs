using BugTracker.Data;
using BugTracker.DTOs;
using BugTracker.Interfaces;
using BugTracker.Models;
using BugTracker.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BugTracker.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        //constructor - interact with DbContext
        public AccountRepository(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        //register method
        public async Task<UserDto> Register(RegisterDto registerDto)
        {

            // create new instance of hashing class
            //using key word implements IDisposable method to dispose of HMACSHA512 class after use
            using var hmac = new HMACSHA512();

            //create new instance of User class and populate with data
            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            //add user registration data to db
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //return email and JWT token to client
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }

        //check if email exists
        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        //login method
        public async Task<User> Login(LoginDto loginDto)
        {
            //locate user with inputted email (email already verified in controller)
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);

            return user;
        }

        //Verify password is correct upon login
        public bool CorrectPassword(User user, LoginDto loginDto)
        {
            bool correctPassword = true;

            //calculate the computed hash using the password salt(key)
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //validate the stored password matches the input password
            for (int i = 0; i < computedHash.Length && correctPassword; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                {
                    correctPassword=false;
                }
            }

            return correctPassword;

        }

    }
}
