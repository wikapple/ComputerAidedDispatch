using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;

namespace ComputerAidedDispatchAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            
            CreateMap<Unit, UnitReadDTO>()
                .ForMember(dto => dto.Name, act => act.MapFrom(unit => unit.UserInfo));

            CreateMap<Unit, UnitDetailsReadDTO>()
                .ForMember(dto => dto.Name, act => act.MapFrom(src => src.UserInfo.Name))
                .ForMember(dto => dto.CallForService, cfs => cfs.MapFrom(src => src.CallForService == null ?
                    null :
                    new CallForServiceReadDTO()
                    {
                        Id = src.CallForService.Id,
                        CallType = src.CallForService.CallType,
                        Address = src.CallForService.Address,
                        Status = src.CallForService.Status,
                        Description = src.CallForService.Description,
                        DateTimeCreated = src.CallForService.DateTimeCreated,
                        NumberOfUnitsAssigned =
                            (src.CallForService == null || src.CallForService.Units == null) ?
                                 0 : src.CallForService.Units.Count()
                }));

            CreateMap<CallForService, CallForServiceReadDTO>()
                .ForMember(dto => dto.NumberOfUnitsAssigned, act => act
                    .MapFrom(src => (src.Units == null) ? 0 : src.Units.Count()
                ));

            CreateMap<Dispatcher, DispatcherReadDTO>()
                .ForMember(dto => dto.Name, act => act.MapFrom(disp => disp.UserInfo.Name));
        
        }

    }
}
