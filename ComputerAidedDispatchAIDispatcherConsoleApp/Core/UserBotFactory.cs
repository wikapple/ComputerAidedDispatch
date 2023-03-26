using ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core
{
    public class UserBotFactory : IUserBotFactory
    {
        private readonly IAuthService _authService;
        private readonly IDispatcherService _dispatcherService;
        private readonly IUnitService _unitService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserBotFactory> _log;
        private readonly string _defaultSystemUserToken;
        private int _dispatcherBotsCreated = 0;
        private int _unitBotsCreated = 0;

        public UserBotFactory(IAuthService authService, IDispatcherService dispatcherService, IUnitService unitService, IConfiguration configuration, ILogger<UserBotFactory> log)
        {
            _authService = authService;
            _dispatcherService = dispatcherService; 
            _unitService = unitService;
            _configuration = configuration;
            _log = log;

            _defaultSystemUserToken = GetDefaultSystemToken();
        }

        public async Task<string?> CreateDispatcherBotAndReturnToken()
        {
            
            var response = await CreateDispatcherBotAsync($"Dispatch-{++_dispatcherBotsCreated}", $"DispatcherBot{_dispatcherBotsCreated}", "ZurgZurg!!55");

            if (response != null)
            {
                _log.LogInformation("Created AI Dispatcher");
            }
            else
            {
                _log.LogError("Failed to create AI Dispatcher");
            }

            string? dispatcherToken = await GetToken($"DispatcherBot{_dispatcherBotsCreated}", "ZurgZurg!!55");

            if (dispatcherToken != null && dispatcherToken.Length > 0)
            {
                _log.LogInformation("Dispatcher token received successfully");
            }
            else
            {
                _log.LogError("Failed to received token for dispatcher");
            }
            return dispatcherToken;
        }

        public async Task<string?> CreateUnitBotAndReturnToken()
        {
            var response = await CreateUnitBotAsync($"PD-Bot-{++_unitBotsCreated}", $"PoliceBot{_unitBotsCreated}", "ZurgZurg!!55");

            if (response != null)
            {
                _log.LogInformation($"Created Unit bot {_unitBotsCreated}");
            }
            else
            {
                _log.LogError($"Failed to create Unit bot{_unitBotsCreated}");
            }

            string? dispatcherToken = await GetToken($"PoliceBot{_unitBotsCreated}", "ZurgZurg!!55");

            if (dispatcherToken != null && dispatcherToken.Length > 0)
            {
                _log.LogInformation("Unit token received successfully");
            }
            else
            {
                _log.LogError("Failed to received token for new Unit");
            }
            return dispatcherToken;
        }


        private string GetDefaultSystemToken()
        {

            LoginRequestDTO dto = new()
            {
                UserName = _configuration.GetValue<string>("SystemUserLoginInformation:UserName"),
                Password = _configuration.GetValue<string>("SystemUserLoginInformation:Password"),
            };

            _log.LogInformation($"Attempting login with username: {dto.UserName} and Password: {dto.Password}");

            Task<APIResponse> loginTask = Task.Run(() => _authService.LoginAsync<APIResponse>(dto));
            loginTask.Wait();
            APIResponse loginResponse = loginTask.Result;

            // APIResponse loginResponse = await _authService.LoginAsync<APIResponse>(dto);

            LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(loginResponse.Result));

            _log.LogInformation($"Login success! Retrieved token {model.Token}");
            return model.Token;

        }

        private async Task<APIResponse> CreateDispatcherBotAsync(string dispatcherNumber, string userName, string password)
        {

            DispatcherAndUserCreateDTO dto = new()
            {
                DispatcherNumber = $"DispatchBot-{_dispatcherBotsCreated}",
                RegistrationDTO = new()
                {
                    UserName = userName,
                    Name = userName,
                    Password = password,
                    Roles = new List<String> { "dispatcher" }
                }
            };

            return await _dispatcherService.CreateAsync<APIResponse>(dto, _defaultSystemUserToken);

        }

        private async Task<APIResponse> CreateUnitBotAsync(string unitNumber, string userName, string password)
        {

            UnitAndUserCreateDTO dto = new()
            {
                UnitNumber = $"PD-Bot-{_unitBotsCreated}",
                Status = "Available",
                RegistrationDTO = new()
                {
                    UserName = userName,
                    Name = userName,
                    Password = password,
                    Roles = new List<String> { "unit" }
                }
            };

            return await _unitService.CreateAsync<APIResponse>(dto, _defaultSystemUserToken);

        }

        private async Task<string?> GetToken(string userName, string password)
        {
            LoginRequestDTO loginRequest = new()
            {
                UserName = userName,
                Password = password
            };
            var apiResponse = await _authService.LoginAsync<APIResponse>(loginRequest);

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                LoginResponseDTO responseModel = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(apiResponse.Result)!)!;

                return responseModel.Token;
            }
            else return null;

        }

        
    }
}

