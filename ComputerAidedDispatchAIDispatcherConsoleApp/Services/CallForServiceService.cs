using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Configuration;
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
            cadUrl = configuration.GetConnectionString("BaseApiUrl");
        }

        public Task<T> CreateAsync<T>(CallForServiceCreateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = $@"{cadUrl}/api/CallComments",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $@"{cadUrl}/api/CallComments",
                Token = token
            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $@"{cadUrl}/api/CallComments",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $@"{cadUrl}/api/CallComments/{id}",
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CallForServiceUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = $@"{cadUrl}/api/CallComments/{dto.Id}",
                Token = token
            });
        }
    }
}
