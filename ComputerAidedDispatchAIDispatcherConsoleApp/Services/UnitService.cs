using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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

        public async Task<List<UnitDetailsReadDTO>>? GetAllAvailableAsync(string? token = null)
        {
            var response = await SendAsync<APIResponse>(new Models.APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = cadUrl + "/api/Units?getDetails=True&status=available"
            });

            if (response != null && response.IsSuccess)
            {
                List<UnitDetailsReadDTO> availableUnits = JsonConvert.DeserializeObject<List<UnitDetailsReadDTO>>(Convert.ToString(response.Result)!)!;
                return availableUnits;
            }
            return null;
        }

        public async Task<bool> AssignUnitToCall(string unitNumber, int callId, string token)
        {
            var response = await SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = cadUrl + @$"/api/Units/{unitNumber}/AssignCallNumber/{callId}",
                Token = token
            });

            return response != null && response.IsSuccess;
        }

        public async Task<bool> UpdateUnitStatus(string unitNumber, string status, string token)
        {
            var response = await SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Url = cadUrl + @$"/api/Units/{unitNumber}/UpdateStatus/{status}",
                Token = token
            });

            return response != null && response.IsSuccess;
        }

    }


}

