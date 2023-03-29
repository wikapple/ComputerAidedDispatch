using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;

public interface IUnitService
{
    Task<T> GetAllAsync<T>();

    Task<List<UnitDetailsReadDTO>>? GetAllAvailableAsync(string? token = null);

    Task<T> CreateAsync<T>(UnitAndUserCreateDTO dto, string token);

    public Task<bool> AssignUnitToCall(string unitNumber, int callId, string token);
    public Task<bool> UpdateUnitStatus(string unitNumber, string status, string token);


}