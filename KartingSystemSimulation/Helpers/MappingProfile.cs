using AutoMapper;
using KartingSystemSimulation.DTOs;
using KartingSystemSimulation.DTOs.MembershipDTOs;
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
            CreateMap<RaceHistory, RaceHistoryDTO>().ReverseMap();

            // Map Racer to RacerOutputDTO
            CreateMap<Racer, RacerOutputDTO>()
                .ForMember(dest => dest.Membership, opt => opt.MapFrom(src => src.Membership));
            // Map Membership to MembershipOutputDTO
            CreateMap<Membership, MembershipOutputDTO>()
                .ForMember(dest => dest.RacerName, opt => opt.MapFrom(src => src.Racer.FirstName + " " + src.Racer.LastName));

        }
    }
}
