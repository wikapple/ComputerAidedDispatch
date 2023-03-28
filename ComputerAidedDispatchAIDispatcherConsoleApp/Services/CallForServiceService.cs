using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;
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
    public class CallForServiceService : BaseService, ICallForServiceService
    {
        private readonly IHttpClientFactory _clientFactory;

        private string cadUrl;

        public CallForServiceService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory= clientFactory;
            cadUrl = configuration.GetConnectionString("BaseApiUrl")!;
        }

        // Creates a call for service, returns the CallId:
        public async Task<int?> CreateAsync(CallForServiceCreateDTO dto, string token)
        {
            var response =  await SendAsync<APIResponse>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $@"{cadUrl}/api/CallsForService",
                Token = token
            });

            if (response != null && response.IsSuccess)
            {
                CallForServiceReadDTO returnedDto = JsonConvert.DeserializeObject<CallForServiceReadDTO>(Convert.ToString(response.Result)!)!;
                if (returnedDto != null)
                {
                    return returnedDto!.Id;
                }

            }
            return null;

        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $@"{cadUrl}/api/CallsForService",
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $@"{cadUrl}/api/CallsForService",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $@"{cadUrl}/api/CallsForService/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CallForServiceUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $@"{cadUrl}/api/CallsForService/{dto.Id}",
                Token = token
            });
        }
    }
}
