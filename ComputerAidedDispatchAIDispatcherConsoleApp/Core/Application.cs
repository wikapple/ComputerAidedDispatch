using ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UserDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core
{
    public class Application : IApplication
    {
        IUserBotFactory _userBotFactory;
        ICadSimulator _cadSimulator;
        public Application(IUserBotFactory userBotFactory, ICadSimulator cadSimulator)
        {
           _userBotFactory = userBotFactory;
            _cadSimulator = cadSimulator;
        }


        

        public async Task Run()
        {
            // Create Dispatcher and get token:
            var dispatcherBotToken = await _userBotFactory.CreateDispatcherBotAndReturnToken();

            // Create Units:
            for(int i = 1; i < 11; i++)
            {
                var newUnit = await _userBotFactory.CreateUnitBotAndReturnToken();
            }

            // Start Creating calls with given Dispatcher and units:
            _cadSimulator.Start(dispatcherBotToken).Wait();

            
            
        }
    }
}
