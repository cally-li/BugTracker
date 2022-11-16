using BugTracker.Data;
using BugTracker.Interfaces;
using BugTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Repositories
{
    public class ProjectRepository: IProjectRepository
    {
        private readonly DataContext _context;

        //constructor - interact with DbContext
        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        //get all projects
        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        //get projects by assigned users
        public async Task<IEnumerable<Project>> GetProjectsByUserAsync(int userid)
        {
            var projectUser=await _context.ProjectUsers.Where(pu=>pu.UserId==userid).ToListAsync();

            //get project ids from ProjectUsers
            var projectIDs = new List<int>(); 
            foreach(var pu in projectUser)
            {
                projectIDs.Add(pu.ProjectId);
            }

            //get all projects by Ids in the list
            return await _context.Projects.Where(p => projectIDs.Contains(p.Id)).ToListAsync();

        }

        //get project by Id
        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            //include developer and submitter of every ticket in the collection
            return await _context.Projects
                .Include(p=>p.Tickets).ThenInclude(t => t.AssignedDeveloper)   
                .Include(p=>p.Tickets).ThenInclude(t => t.Submitter)
                .SingleOrDefaultAsync(p=>p.Id==projectId);
        }

    }
}
