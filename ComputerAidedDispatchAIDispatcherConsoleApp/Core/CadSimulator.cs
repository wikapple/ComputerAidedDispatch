using ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core;

public class CadSimulator : ICadSimulator
{

    private readonly ILogger<CadSimulator> _log;
    private string _dispatcherToken;
    private List<ICallScriptRunner> _currentCallRunners;
    private DateTime timeSinceLastCallWasAdded;

    public bool ContinueSimulator { get; set; }


    public CadSimulator(ILogger<CadSimulator> log)
    {
        
        _log = log;
        ContinueSimulator = true;
        _currentCallRunners = new();
        timeSinceLastCallWasAdded = DateTime.Now;
    }

    public async Task Start(string dispatcherToken)
    {
        _dispatcherToken = dispatcherToken;

        while (ContinueSimulator)
        {
            // Take action for each call runner if necessary
            foreach(var callRunner in _currentCallRunners)
            {
                // If callRunner's timeout period is over, take next action
                if (callRunner.IsTimeoutPeriodOver)
                {
                    callRunner.TakeAction();
                }
                // If callRunner is complete, remove it
                if(callRunner.IsScriptRunnerComplete)
                {
                    _currentCallRunners.Remove(callRunner);
                }
            }
            // Decide whether to add more calls or not to _currentCallRunners

            // Sleep:
            Thread.Sleep(5000);

        }


        return;
        
    }
}
