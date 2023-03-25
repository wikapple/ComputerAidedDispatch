using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services;

public class AuthService : BaseService, IAuthService
{

    private readonly IHttpClientFactory _clientFactory;
    private string cadUrl;

    public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        cadUrl = configuration.GetConnectionString("BaseApiUrl")!;
    }

    public Task<T> LoginAsync<T>(LoginRequestDTO obj)
    {
        return SendAsync<T>(new Models.APIRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = obj,
            Url = $@"{cadUrl}/api/UsersAuth/login"
        });
    }

    public Task<T> RegisterAsync<T>(RegistrationRequestDTO obj)
    {
        return SendAsync<T>(new Models.APIRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = obj,
            Url = $@"{cadUrl}/api/UsersAuth/register"
        });
    }
}
