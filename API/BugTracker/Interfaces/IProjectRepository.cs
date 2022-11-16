using BugTracker.Models;

namespace BugTracker.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<IEnumerable<Project>> GetProjectsByUserAsync(int userid);
        Task<Project> GetProjectByIdAsync(int projectId);



    }
}
