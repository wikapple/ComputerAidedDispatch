using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;

namespace ComputerAidedDispatchAPI.Service.IService;

public interface IDispatcherService{
    public Task<List<DispatcherReadDTO>> GetAllDispatchersAsync();
    public Task<DispatcherReadDTO> GetDetailsByDispatcherNumberAsync(string dispatchNumber);

    public Task<DispatcherReadDTO> CreateAsync(DispatcherCreateDTO createDTO);

    public Task<DispatcherReadDTO> CreateDispatcherAndUser(DispatcherAndUserCrateDTO createDTO);

    public Task<DispatcherReadDTO> UpdateAsync(string dispatchNumber);

    public Task DeleteAsync(string dispatchNumber);
}