using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;

namespace ComputerAidedDispatchAPI.Service;

public class UnitService
{
    private readonly IUnitRepository _unitRepository;
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	public UnitService(IUnitRepository unitRepository, IUserService userService, IMapper mapper)
	{
		_unitRepository = unitRepository;
		_userService = userService;
		_mapper = mapper;
	}

	// Create new
	public async Task CreateAsync(UnitCreateDTO createDTO)
	{
		if (_userService.DoesUserIdExist(createDTO.UserId))
		{
			Unit newUnit = new()
			{
				UnitNumber = createDTO.UnitNumber,
				UserId = createDTO.UserId,
				Status = "Unavailable"
			};

			await _unitRepository.CreateAsync(newUnit);
		}
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
			/*
			UnitReadDTO unitDTO = new()
			{
				UnitNumber = queriedUnit.UnitNumber,
				CallNumber = queriedUnit.CallNumber,
				Status = queriedUnit.Status,
			};
			*/
			return unitDTO;
		}
	}

	public async Task<UnitDetailsReadDTO?> GetDetailsAsync(string unitNumber)
	{
        var queriedUnit = await _unitRepository.GetAsync((x) => x.UnitNumber == unitNumber);

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

		return units.Select(unit => _mapper.Map<UnitReadDTO>(unit)).ToList();
	}

	// Filter By Call Number
	public async Task<List<UnitDetailsReadDTO>> FilterByCallNumberAsync(int callNumber)
	{
		List<Unit> units = await _unitRepository.GetAllAsync(x => x.CallNumber == callNumber);

		return units.Select(unit => _mapper.Map<UnitDetailsReadDTO>(unit)).ToList();
	}


	// Update
	public async Task UpdateAsync(UnitUpdateDTO updateDTO)
	{
		var unit = await _unitRepository.GetAsync(x => x.UnitNumber == updateDTO.UnitNumber);

		if (unit != null)
		{
			unit.CallNumber = updateDTO.CallNumber;
			unit.Status = updateDTO.Status;

			await _unitRepository.UpdateAsync(unit);
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





}
