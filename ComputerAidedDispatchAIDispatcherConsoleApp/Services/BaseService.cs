using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse ResponseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.ResponseModel = new();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("CAD");
             
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                message.Method = apiRequest.ApiType switch
                {
                    SD.ApiType.POST => HttpMethod.Post,
                    SD.ApiType.PUT => HttpMethod.Put,
                    SD.ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get,
                };
                HttpResponseMessage apiResponse = null;

                if (!string.IsNullOrEmpty(apiRequest.Token))
                {

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }

              apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                try
                {
                    APIResponse ApiResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if ((ApiResponse != null) && (apiResponse.StatusCode == System.Net.HttpStatusCode.BadRequest
                        || apiResponse.StatusCode == System.Net.HttpStatusCode.NotFound))
                    {
                        ApiResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        ApiResponse.IsSuccess = false;
                        var res = JsonConvert.SerializeObject(ApiResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                }
                catch (Exception ex)
                {
                    var exceptionResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exceptionResponse;
                }

                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;

            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }

        }
    }
}
