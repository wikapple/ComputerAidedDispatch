using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;

namespace ComputerAidedDispatchAPI.Service.IService
{
    public interface IUnitService
    {
        public Task<UnitReadDTO?> CreateAsync(UnitCreateDTO createDTO);
        public Task<UnitReadDTO?> CreateUnitAndUserAsync(UnitAndUserCreateDTO createDTO);
        public Task<UnitReadDTO?> GetByUnitNumberAsync(string unitNumber);
        public UnitDetailsReadDTO? GetDetails(string unitNumber);
        public Task<List<UnitDetailsReadDTO>> GetAllDetailsAsync();
        public Task<List<UnitReadDTO>> GetAllAsync();
        public Task<List<UnitReadDTO>> FilterByCallNumberAsync(int callNumber);
        public Task<List<UnitReadDTO>> FilterByStatusAsync(string status);
        public Task<List<UnitReadDTO>> FilterByCallNumberAndStatusAsync(int callNumber, string status);
        public Task<UnitReadDTO?> UpdateAsync(UnitUpdateDTO updateDTO);
        public Task<UnitReadDTO?> UpdateStatusAsync(string unitNumber, string status);
        public Task<UnitReadDTO?> AssignCallAsync(string unitNumber, int? callNumber);
        public Task DeleteAsync(string unitNumber);
    }
}
