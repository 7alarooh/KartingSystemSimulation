using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.Models;

namespace KartingSystemSimulation.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Admin, AdminOutputDTO>();
            CreateMap<AdminInputDTO, Admin>();
            CreateMap<User, UserOutputDTO>();
            CreateMap<UserInputDTO, User>();
            CreateMap<Supervisor, SupervisorOutputDTO>();
            CreateMap<SupervisorInputDTO, Supervisor>();
            

        }
    }
}
