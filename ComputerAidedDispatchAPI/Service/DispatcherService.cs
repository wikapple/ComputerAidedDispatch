using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;

namespace ComputerAidedDispatchAPI.Service;

public class DispatcherService : IDispatcherService
{

    IDispatcherRepository _dispatcherRepository;
    public DispatcherService(IDispatcherRepository dispatcherRepository)
    {
        _dispatcherRepository = dispatcherRepository;
    }
    public Task<List<DispatcherReadDTO>> GetAllDispatchersAsync()
    {
        
    }
    public Task<DispatcherReadDTO> GetDetailsByDispatcherNumberAsync(string dispatchNumber)
    {

    }

    public Task<DispatcherReadDTO> CreateAsync(DispatcherCreateDTO createDTO)
    {

    }

    public Task<DispatcherReadDTO> CreateDispatcherAndUser(DispatcherAndUserCrateDTO createDTO)
    {

    }

    public Task<DispatcherReadDTO> UpdateAsync(string dispatchNumber)
    {

    }

    public Task DeleteAsync(string dispatchNumber)
    {

    }
}