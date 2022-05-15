using BugTracker.Models;

namespace BugTracker.Interfaces
{
    public interface IUserRepository
    {
        //get all users
        Task<List<User>> GetAllUsers();

        //get user by id
        Task<User> GetUser(int id);

    }
}
