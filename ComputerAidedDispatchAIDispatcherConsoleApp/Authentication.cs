using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UserDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp
{
    public class Authentication : IAuthentication
    {
        private readonly IAuthService _authService;
        private readonly ILogger<Authentication> _log;
        private readonly IConfiguration _configuration;
        private readonly IUnitService _unitService;
        public Authentication(IAuthService authService, ILogger<Authentication> log, IConfiguration configuration, IUnitService unitService)
        {
            _authService = authService;
            _log = log;
            _configuration = configuration;
            _unitService = unitService;
        }


        public async Task<string> GetAuthenticationToken()
        {

            LoginRequestDTO dto = new()
            {
                UserName = _configuration.GetValue<string>("SystemUserLoginInformation:UserName"),
                Password = _configuration.GetValue<string>("SystemUserLoginInformation:Password"),
            };

            _log.LogInformation($"Attempting login with username: {dto.UserName} and Password: {dto.Password}");

            APIResponse loginResponse = await _authService.LoginAsync<APIResponse>(dto);

            LoginResponseDTO model = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(loginResponse.Result));

            _log.LogInformation($"Login success! Retrieved token {model.Token}");
            return model.Token;

        }

        public async Task PrintAllUnits()
        {
            APIResponse response = await _unitService.GetAllAsync<APIResponse>();
            List<UnitReadDTO> list = new();

            if (response != null)
            {
                list = JsonConvert.DeserializeObject<List<UnitReadDTO>>(Convert.ToString(response.Result)!)!;



                foreach (var unit in list)
                {
                    Console.WriteLine(unit.Name);
                }
            }


        }
    }
}
