using BugTracker.Models;

namespace BugTracker.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
