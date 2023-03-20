using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;

namespace ComputerAidedDispatchAPI.Service;

public class DispatcherService : IDispatcherService
{
    
    private readonly IDispatcherRepository _dispatcherRepository;
    private readonly IMapper _mapper;
    private readonly ICadSharedService _sharedService;
    public DispatcherService(IDispatcherRepository dispatcherRepository, IMapper mapper, ICadSharedService sharedService)
    {
        _dispatcherRepository = dispatcherRepository;
        _mapper = mapper;
        _sharedService = sharedService;
        
    }
    public async Task<List<DispatcherReadDTO>> GetAllDispatchersAsync()
    {
        var dispatchList = await _dispatcherRepository.GetAllAsync();

        return dispatchList.Select(d => _mapper.Map<DispatcherReadDTO>(d)).ToList();


    }
    public async Task<DispatcherReadDTO> GetByDispatcherNumberAsync(string dispatchNumber)
    {
        var dispatcher = await _dispatcherRepository.GetAsync(d => d.DispatcherNumber == dispatchNumber);

        return _mapper.Map<DispatcherReadDTO>(dispatcher);
    }

    public async Task<DispatcherReadDTO?> CreateAsync(DispatcherCreateDTO createDTO)
    {
        if (_sharedService.DoesUserIdExist(createDTO.UserId)) {
            Dispatcher newDispatcher = new()
            {
                DispatcherNumber = createDTO.DispatcherNumber,
                UserId = createDTO.UserId
            };

            var newlyCreatedDispatcher = await _dispatcherRepository.GetAsync(d => d.DispatcherNumber == newDispatcher.DispatcherNumber, includeProperties: "UserInfo");

            return _mapper.Map<DispatcherReadDTO>(newlyCreatedDispatcher);

        }
        return null;
    }

    public async Task<DispatcherReadDTO?> CreateDispatcherAndUser(DispatcherAndUserCreateDTO createDTO)
    {
        bool dispatcherNumberIsUnique = 
             await _dispatcherRepository.GetAsync(d => d.DispatcherNumber == createDTO.DispatcherNumber) == null;

        if(dispatcherNumberIsUnique)
        {
            var userCreationResponse = await _sharedService.Register(createDTO.RegistrationDTO);

            if(userCreationResponse != null)
            {
                Dispatcher dispatcher = new()
                {
                    DispatcherNumber = createDTO.DispatcherNumber,
                    UserId = userCreationResponse.Id
                };

                await _dispatcherRepository.CreateAsync(dispatcher);

                var newlyCreatedDispatcher = await _dispatcherRepository.GetAsync(d => d.DispatcherNumber == dispatcher.DispatcherNumber, includeProperties: "UserInfo");

                return _mapper.Map<DispatcherReadDTO>(newlyCreatedDispatcher);
            }
        }

        return null;
    }

    public async Task DeleteAsync(string dispatchNumber)
    {
        var dispatcherToDelete = await _dispatcherRepository.GetAsync(d => d.DispatcherNumber == dispatchNumber);

        if(dispatcherToDelete != null)
        {
            await _dispatcherRepository.RemoveAsync(dispatcherToDelete);
        }
    }
}