using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;

namespace ComputerAidedDispatchAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            
            CreateMap<Unit, UnitReadDTO>();

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
                        Units = src.CallForService.Units.Select(unit => new UnitReadDTO()
                        {
                            UnitNumber = unit.UnitNumber,
                            CallNumber = unit.CallNumber,
                            Status = unit.Status
                        }).ToList()
                }));

            CreateMap<CallForService, CallForServiceReadDTO>()
                .ForMember(dto => dto.Units, act => act.MapFrom(src => src.Units.Select(unit => new UnitReadDTO()
                {
                    UnitNumber = unit.UnitNumber,
                    CallNumber = unit.CallNumber,
                    Status = unit.Status
                }
                ).ToList()
                ));
        }
    }
}
