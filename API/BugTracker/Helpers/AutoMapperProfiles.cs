using AutoMapper;
using BugTracker.DTOs;
using BugTracker.Models;

namespace BugTracker.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, AllUsersDto>().ReverseMap();
        }
    }
}
