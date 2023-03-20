using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;

namespace ComputerAidedDispatchAPI.Service.IService;

public interface IDispatcherService{
    public Task<List<DispatcherReadDTO>> GetAllDispatchersAsync();
    public Task<DispatcherReadDTO> GetByDispatcherNumberAsync(string dispatchNumber);

    public Task<DispatcherReadDTO> CreateAsync(DispatcherCreateDTO createDTO);

    public Task<DispatcherReadDTO> CreateDispatcherAndUser(DispatcherAndUserCreateDTO createDTO);

    public Task DeleteAsync(string dispatchNumber);
}