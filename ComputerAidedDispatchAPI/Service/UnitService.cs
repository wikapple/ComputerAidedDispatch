using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq.Expressions;

namespace ComputerAidedDispatchAPI.Service;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _unitRepository;
	private readonly IMapper _mapper;
	private readonly ICadSharedService _sharedService;

	public UnitService(IUnitRepository unitRepository, ICadSharedService sharedService, IMapper mapper)
	{
		_unitRepository = unitRepository;
		_mapper = mapper;
		_sharedService = sharedService;
		CreateDefaultUsersIfNotExists();
	}

	// Create new
	public async Task<UnitReadDTO?> CreateAsync(UnitCreateDTO createDTO)
	{
		bool unitNumberIsUnique =
			await _unitRepository.GetAsync(x => x.UnitNumber == createDTO.UnitNumber) == null;
		
		if (_sharedService.DoesUserIdExist(createDTO.UserId) && unitNumberIsUnique)
		{
			Unit newUnit = new()
			{
				UnitNumber = createDTO.UnitNumber,
				UserId = createDTO.UserId,
				Status = "Unavailable",
				TimeStatusAssigned = DateTime.Now
			};

			await _unitRepository.CreateAsync(newUnit);

            var newlyCreatedUnit = _unitRepository.GetAsync(x => x.UnitNumber == createDTO.UnitNumber, includeProperties: "UserInfo");

            return _mapper.Map<UnitReadDTO>(newlyCreatedUnit);
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
			var userCreationResponse = await _sharedService.Register(createDTO.RegistrationDTO);

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

				var newlyCreatedUnit = _unitRepository.GetAsync(x => x.UnitNumber == createDTO.UnitNumber, includeProperties: "UserInfo").Result;
				return _mapper.Map<UnitReadDTO>(newlyCreatedUnit);
			}
		}
		return null;

    }

	// Read One
	public async Task<UnitReadDTO?> GetByUnitNumberAsync(string unitNumber)
	{
		return await _sharedService.GetByUnitNumberAsync(unitNumber);
	}

    public async Task<UnitDetailsReadDTO?> GetDetailsByUnitNumberAsync(string unitNumber)
	{
        var queriedUnit =
            await _unitRepository.GetAsync((x) => x.UnitNumber == unitNumber, includeProperties: "UserInfo,CallForService");

        if (queriedUnit == null)
        {
            return null;
        }
        else
        {
            return _mapper.Map<UnitDetailsReadDTO>(queriedUnit);
        }
    }

    // Read All
    public async Task<List<UnitReadDTO>> GetAllAsync(int? callNumber = null, string? status = null)
	{
		// Create filter func:
		Expression<Func<Unit, bool>>? filter;

		if(callNumber != null && status != null)
		{
			filter = u => u.CallNumber == callNumber && u.Status == status;
		}
		else if(callNumber != null)
		{
			filter = u => u.CallNumber == callNumber;
        }
		else if(status != null)
		{
			filter = u => u.Status == status;
        }
		else
		{
			filter = null;
		}
		
		List<Unit> unitList = await _unitRepository.GetAllAsync(filter, includeProperties: "UserInfo");
			return unitList.Select(u => _mapper.Map<UnitReadDTO>(u)).ToList();
			
	}

	public async Task<List<UnitDetailsReadDTO>> GetAllDetailsAsync(int? callNumber = null, string? status = null)
	{
        List<Unit> unitList = await _unitRepository.GetAllAsync(includeProperties: "UserInfo,CallForService");
        return unitList.Select(u => _mapper.Map<UnitDetailsReadDTO>(u)).ToList();
    }


    // Update
    public async Task<UnitReadDTO?> UpdateAsync(UnitUpdateDTO updateDTO)
	{
		var unit = await _unitRepository.GetAsync(x => x.UnitNumber == updateDTO.UnitNumber, includeProperties: "UserInfo");

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
       return await _sharedService.UpdateStatusAsync(unitNumber, status);
    }

	// Update Call
	public async Task<UnitReadDTO?> AssignCallAsync(string unitNumber, int? callNumber)
	{
		return await _sharedService.AssignCallAsync(unitNumber, callNumber);
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
