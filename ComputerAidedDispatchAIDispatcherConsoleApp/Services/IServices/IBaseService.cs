using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices
{
    public interface IBaseService
    {
        APIResponse ResponseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest responseModel);
    }
}
