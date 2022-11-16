using AutoMapper;
using BugTracker.DTOs;
using BugTracker.Interfaces;
using BugTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BugTracker.Controllers
{
    [Authorize]
    public class ProjectsController : BaseApiController
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public ProjectsController(IProjectRepository projectRepository, IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }


        //get all projects (/projects)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjects()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            var projectsToReturn = _mapper.Map<List<ProjectDto>>(projects);
            return Ok(projectsToReturn);

        }


        //get projects by user (/projects/all/email)
        [HttpGet("all/{email}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByUser(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            var userId = user.Id;
            var projects= await _projectRepository.GetProjectsByUserAsync(userId);
            var projectsToReturn = _mapper.Map<List<ProjectDto>>(projects);
            return Ok(projectsToReturn);
        }

        //get project by id (/projects/id)
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjectById(int id)
        {
            var project= await _projectRepository.GetProjectByIdAsync(id);
            var projectToReturn = _mapper.Map<ProjectDto>(project);
            return Ok(projectToReturn);
        }


    }
}
