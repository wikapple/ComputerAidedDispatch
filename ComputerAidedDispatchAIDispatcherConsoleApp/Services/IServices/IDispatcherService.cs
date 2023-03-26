using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.DispatcherDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices
{
    public interface IDispatcherService
    {
        public Task<T> CreateAsync<T>(DispatcherAndUserCreateDTO dto, string token);
    }
}
