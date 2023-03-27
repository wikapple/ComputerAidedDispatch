using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices
{
    public interface ICallForServiceService
    {
        Task<T> GetAllAsync<T>(string token);
        Task<T> GetAsync<T>(int id, string token);
        Task<int?> CreateAsync(CallForServiceCreateDTO dto, string token);
        Task<T> UpdateAsync<T>(CallForServiceUpdateDTO dto, string token);
        Task<T> DeleteAsync<T>(int id, string token);
    }
}
