using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatch_UtilityLibrary;
using Microsoft.Extensions.Configuration;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services
{
    public class DispatcherService: BaseService, IDispatcherService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string cadUrl;

        public DispatcherService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            this.cadUrl = configuration.GetConnectionString("BaseApiUrl");
        }

        public Task<T> CreateAsync<T>(DispatcherAndUserCreateDTO dto, string token) 
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $@"{cadUrl}/api/dispatchers/createdispatcheranduser",
                Token = token
            });
        }
    }
}
