using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices
{
    public interface IUnitService
    {
        Task<T> GetAllAsync<T>();
    }
}
