using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;

namespace ComputerAidedDispatchAPI.Service.IService
{
    public interface IUnitService
    {
        public Task<UnitReadDTO?> CreateAsync(UnitCreateDTO createDTO);
        public Task<UnitReadDTO?> CreateUnitAndUserAsync(UnitAndUserCreateDTO createDTO);
        public Task<UnitReadDTO?> GetByUnitNumberAsync(string unitNumber);
        public Task<UnitDetailsReadDTO?> GetDetailsByUnitNumberAsync(string unitNumber);
        public Task<List<UnitDetailsReadDTO>> GetAllDetailsAsync(int? callNumber = null, string? status = null);
        public Task<List<UnitReadDTO>> GetAllAsync(int? callNumber = null, string? status = null);
        
        public Task<UnitReadDTO?> UpdateAsync(UnitUpdateDTO updateDTO);
        public Task<UnitReadDTO?> UpdateStatusAsync(string unitNumber, string status);
        public Task<UnitReadDTO?> AssignCallAsync(string unitNumber, int? callNumber);
        public Task DeleteAsync(string unitNumber);
    }
}
