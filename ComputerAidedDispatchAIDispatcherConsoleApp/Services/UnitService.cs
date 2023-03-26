using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services
{
    internal class UnitService : BaseService, IUnitService
    {

        private readonly IHttpClientFactory _clientFactory;

        private string cadUrl;

        public UnitService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            this.cadUrl = configuration.GetConnectionString("BaseApiUrl");
           
        }

        public Task<T> CreateAsync<T>(UnitAndUserCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $@"{cadUrl}/api/units/createunitanduser",
                Token = token
            });
        }

        public async Task<T> GetAllAsync<T>()
        {
            return await SendAsync<T>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = cadUrl + "/api/Units",

            });
        }
    }
}
