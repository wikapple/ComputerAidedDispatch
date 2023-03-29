using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UserDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;

namespace ComputerAidedDispatchAPI.Service;

public class CadSharedService : ICadSharedService
{
    private readonly IUnitRepository _unitRepository;
    private readonly ICallForServiceRepository _callRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDispatcherRepository _dispatcherRepository;
    private readonly ICallCommentRepository _callCommentRepository;
    private readonly IMapper _mapper;

    public CadSharedService(IUnitRepository unitRepository, ICallForServiceRepository callRepository, IUserRepository userRepository, IDispatcherRepository dispatcherRepository, ICallCommentRepository callCommentRepository, IMapper mapper)
    {
        _unitRepository = unitRepository;
        _callRepository = callRepository;
        _userRepository = userRepository;
        _dispatcherRepository = dispatcherRepository;
        _callCommentRepository = callCommentRepository;
        _mapper = mapper;
    }

    public async Task<UnitReadDTO?> AssignCallAsync(string unitNumber, int? callNumber)
    {
        var unit = await _unitRepository.GetAsync(x => x.UnitNumber == unitNumber, includeProperties: "UserInfo");

        if (unit != null && unit.CallNumber == null &&
            (callNumber == null || await _callRepository.GetAsync(call => call.Id == callNumber) != null))
        {
            unit.CallNumber = callNumber;
            unit.Status = "Assigned";
            await _unitRepository.UpdateAsync(unit);

            return _mapper.Map<UnitReadDTO>(unit);
        }
        else
        {
            return null;
        }
    }

    public async Task<bool> DoesCallForServiceExist(int callId)
    {
        return await _callRepository.GetAsync(call => call.Id == callId) != null;
    }

    public bool DoesUserIdExist(string userId)
    {
        return _userRepository.GetUser(userId) != null;
    }

    public async Task<UnitReadDTO?> GetByUnitNumberAsync(string unitNumber)
    {
        var queriedUnit =
            await _unitRepository.GetAsync((x) => x.UnitNumber == unitNumber, includeProperties: "UserInfo");

        if (queriedUnit == null)
        {
            return null;
        }
        else
        {
            return _mapper.Map<UnitReadDTO>(queriedUnit);
        }
    }

    public string? GetUserIdByUserName(string userName)
    {
        var applicationUser = _userRepository.GetUser(user => user.UserName == userName);

        return applicationUser.Id;
    }

    public async Task<UserDTO?> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        return await _userRepository.Register(registrationRequestDTO);
    }

    public async Task<UnitReadDTO>? UpdateStatusAsync(string unitNumber, string status)
    {
        var unit = await _unitRepository.GetAsync(x => x.UnitNumber == unitNumber, includeProperties: "UserInfo");
        if (unit != null)
        {
            unit.Status = status;
            await _unitRepository.UpdateAsync(unit);
            return _mapper.Map<UnitReadDTO>(unit);
        }
        else
        {
            return null;
        }
    }

}
