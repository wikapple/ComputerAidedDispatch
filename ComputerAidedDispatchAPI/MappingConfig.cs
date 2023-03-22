using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.CallCommentDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UserDTOs;

namespace ComputerAidedDispatchAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ReverseMap();
            
            CreateMap<Unit, UnitReadDTO>()
                .ForMember(dto => dto.Name, act => act.MapFrom(unit => unit.UserInfo));

            CreateMap<Unit, UnitDetailsReadDTO>()
                .ForMember(dto => dto.Name, act => act.MapFrom(src => src.UserInfo.Name))
                .ForMember(dto => dto.CallForService, cfs => cfs.MapFrom(src => src.CallForService));

            

            CreateMap<Dispatcher, DispatcherReadDTO>()
                .ForMember(dto => dto.Name, act => act.MapFrom(disp => disp.UserInfo.Name));

            CreateMap<CallForService, CallForServiceReadDTO>()
                .ForMember(dto => dto.NumberOfUnitsAssigned, act => act
                    .MapFrom(src => (src.Units == null) ? 0 : src.Units.Count()
                ));

            CreateMap<CallForService, CallForServiceDetailsReadDTO>()
                .ForMember(dto => dto.Units, act => act.MapFrom(src => src.Units))
                .ForMember(dto => dto.CallComments, act => act.MapFrom(src => src.CallComments));

            CreateMap<CallComment, CallCommentReadDTO>()
                .ForMember(dto => dto.Name, act => act.MapFrom(comment => comment.ApplicationUser.Name));




        
        }

    }
}
