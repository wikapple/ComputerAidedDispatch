using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ComputerAidedDispatchAPI.Service;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
	private readonly IUserService _userService;
	private readonly ICallForServiceService _callService;
	private readonly IMapper _mapper;

	public UnitService(IUnitRepository unitRepository, IUserService userService, ICallForServiceService callService, IMapper mapper)
	{
		_unitRepository = unitRepository;
		_callService = callService;
		_userService = userService;
		_mapper = mapper;
		CreateDefaultUsersIfNotExists();
	}

	// Create new
	public async Task<UnitReadDTO?> CreateAsync(UnitCreateDTO createDTO)
	{
		bool unitNumberIsUnique =
			await _unitRepository.GetAsync(x => x.UnitNumber == createDTO.UnitNumber) == null;
		
		if (_userService.DoesUserIdExist(createDTO.UserId) && unitNumberIsUnique)
		{
			Unit newUnit = new()
			{
				UnitNumber = createDTO.UnitNumber,
				UserId = createDTO.UserId,
				Status = "Unavailable",
				TimeStatusAssigned = DateTime.Now
			};

			await _unitRepository.CreateAsync(newUnit);

			return _mapper.Map<UnitReadDTO>(newUnit);
		}
		return null;
	}
	// Create new user and unit in one method
	public async Task<UnitReadDTO?> CreateUnitAndUserAsync(UnitAndUserCreateDTO createDTO)
	{
		bool unitNumberIsUnique =
            await _unitRepository.GetAsync(x => x.UnitNumber == createDTO.UnitNumber) == null;

		if(unitNumberIsUnique)
		{
			var userCreationResponse = await _userService.Register(createDTO.RegistrationDTO);

			if (userCreationResponse != null)
			{
				Unit unit = new Unit()
				{
					UnitNumber = createDTO.UnitNumber,
					UserId = userCreationResponse.Id,
					Status = "Unavailable",
					TimeStatusAssigned = DateTime.Now
				};

				await _unitRepository.CreateAsync(unit);

				return _mapper.Map<UnitReadDTO>(unit);
			}
		}
		return null;

    }

	// Read One
	public async Task<UnitReadDTO?> GetByUnitNumberAsync(string unitNumber)
	{
		var queriedUnit = await _unitRepository.GetAsync((x) => x.UnitNumber == unitNumber);

		if (queriedUnit == null)
		{
			return null;
		}
		else
		{

			UnitReadDTO unitDTO = _mapper.Map<UnitReadDTO>(queriedUnit);
			
			return unitDTO;
		}
	}

	// Get Details of one unit by UnitNumber:
	public UnitDetailsReadDTO? GetDetails(string unitNumber)
	{
        var queriedUnit = _unitRepository.GetDetails(unitNumber);

        if (queriedUnit == null)
        {

            return null;
        }
		else
		{

			UnitDetailsReadDTO unitDetailsDTO = _mapper.Map<UnitDetailsReadDTO>(queriedUnit);

			return unitDetailsDTO;
		}
    }

	// Read All
	public async Task<List<UnitReadDTO>> GetAllAsync()
	{
		List<Unit> units = await _unitRepository.GetAllAsync();

		var unitDTOs = units.Select(unit => _mapper.Map<UnitReadDTO>(unit)).OrderBy(x => x.Status).ThenBy(x => x.UnitNumber).ToList();


		return unitDTOs;
	}

    public async Task<List<UnitDetailsReadDTO>> GetAllDetailsAsync()
    {
        List<Unit> units = await _unitRepository.GetAllAsync();

        var unitDTOs = units.Select(unit => _mapper.Map<UnitDetailsReadDTO>(unit)).OrderBy(x => x.Status).ThenBy(x => x.StatusDuration).ToList();

        return unitDTOs;
    }

    // Filter By Call Number
    public async Task<List<UnitReadDTO>> FilterByCallNumberAsync(int callNumber)
	{
		List<Unit> units = await _unitRepository.GetAllAsync(x => x.CallNumber == callNumber);

		return units.Select(unit => _mapper.Map<UnitReadDTO>(unit)).ToList();
	}

    public async Task<List<UnitReadDTO>> FilterByStatusAsync(string status)
    {
        List<Unit> units = await _unitRepository.GetAllAsync(x => x.Status == status);

        return units.Select(unit => _mapper.Map<UnitReadDTO>(unit)).ToList();
    }

    public async Task<List<UnitReadDTO>> FilterByCallNumberAndStatusAsync(int callNumber, string status)
    {
        List<Unit> units = await _unitRepository.GetAllAsync(x => x.Status == status && x.CallNumber == callNumber);

        return units.Select(unit => _mapper.Map<UnitReadDTO>(unit)).ToList();
    }


    // Update
    public async Task<UnitReadDTO?> UpdateAsync(UnitUpdateDTO updateDTO)
	{
		var unit = await _unitRepository.GetAsync(x => x.UnitNumber == updateDTO.UnitNumber);

		if (unit != null)
		{
			unit.CallNumber = updateDTO.CallNumber;
			unit.Status = updateDTO.Status;

			var newUnit = await _unitRepository.UpdateAsync(unit);

			return _mapper.Map<UnitReadDTO>(newUnit);
		}
		else
		{
			return null;
		}
	}

	// Update Status
	public async Task<UnitReadDTO?> UpdateStatusAsync(string unitNumber, string status)
	{
        var unit = await _unitRepository.GetAsync(x => x.UnitNumber == unitNumber);

		if(unit != null)
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

	// Update Call
	public async Task<UnitReadDTO?> AssignCallAsync(string unitNumber, int? callNumber)
	{
        var unit = await _unitRepository.GetAsync(x => x.UnitNumber == unitNumber);

		if ( unit != null &&
			( callNumber == null || await _callService.GetAsync((int)callNumber!) != null) )
		{
			unit.CallNumber = callNumber;
			await _unitRepository.UpdateAsync(unit);
			return _mapper.Map<UnitReadDTO>(unit);
		}
		else
		{
			return null;
		}
    }



	// Delete
	public async Task DeleteAsync(string unitNumber)
	{
        var unitToDelete = await _unitRepository.GetAsync(x => x.UnitNumber == unitNumber);

		if(unitToDelete != null)
		{
			await _unitRepository.RemoveAsync(unitToDelete);
		}
    }

	private async void CreateDefaultUsersIfNotExists()
	{
		if ( await _unitRepository.GetAsync(x => x.UserInfo.Name == "TestUnit") == null)
		{
			UnitAndUserCreateDTO createDTO = new()
			{
				RegistrationDTO = new()
				{
					UserName = "TestUnit",
					Name = "TestUnit",
					Password= "Info-C342",
					Roles = new List<string> {"unit"}
				},
				UnitNumber = "Test-1"

			};

			await CreateUnitAndUserAsync(createDTO);
		}
	}





}
