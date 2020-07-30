using AutoMapper;
using Entities.Models;
using Entities.DTOs;

namespace MyPomodoroServer.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pomodoro, PomodoroDTO>();
            CreateMap<PomodoroDTO, Pomodoro>();
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
