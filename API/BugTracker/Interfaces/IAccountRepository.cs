using BugTracker.DTOs;
using BugTracker.Models;

namespace BugTracker.Interfaces
{
    public interface IAccountRepository
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<bool> EmailExists(string email);
        Task<User> Login(LoginDto loginDto);
        bool CorrectPassword(User user, LoginDto loginDto);


    }
}
