using ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core;

public class CadSimulator : ICadSimulator
{

    private readonly ILogger<CadSimulator> _log;
    private readonly IUnitService _unitService;
    private readonly ICallForServiceService _callService;
    private readonly ICallCommentService _commentService;
    private string? _dispatcherToken;
    List<CallScriptTracker> _currentCallTrackers;
    private readonly Random _random;

    public bool ContinueSimulator { get; set; }


    public CadSimulator(ILogger<CadSimulator> log, IUnitService unitService, ICallForServiceService callForServiceService, ICallCommentService callCommentService)
    {

        _log = log;
        _unitService = unitService;
        _callService = callForServiceService;
        _commentService = callCommentService;

        ContinueSimulator = true;
        _random = new Random();
    }

    public async Task Start(string dispatcherToken)
    {
        _dispatcherToken = dispatcherToken;
        _currentCallTrackers = new();
        int loopIterationsWithoutAddingCall = 0;
        while (ContinueSimulator)
        {
            // Take action for each call runner if necessary
            foreach (var callRunner in _currentCallTrackers)
            {
                // If callRunner's timeout period is over, take next action
                if (callRunner.IsTimeoutPeriodOver)
                {
                    await TakeCallAction(callRunner);
                }
                // If callRunner is complete, remove it
                if (callRunner.IsCallClosed)
                {
                    _currentCallTrackers.Remove(callRunner);
                }
            }

            // Decide whether to add more calls or not to _currentCallRunners
            if(IsNewScriptBeingAdded(_currentCallTrackers.Count, 12, loopIterationsWithoutAddingCall))
            {
                _log.LogInformation("loop itertions set to 0");
                loopIterationsWithoutAddingCall = 0;

                _currentCallTrackers.Add(new CallScriptTracker(CallScriptGenerator()));

                _log.LogInformation($"Current call count: {_currentCallTrackers.Count()}");

            }
            else
            {
                loopIterationsWithoutAddingCall++;
                _log.LogInformation($"Loops without adding call: {loopIterationsWithoutAddingCall}");
            }

            // Sleep:
            Thread.Sleep(5000);

        }
        return;

    }

    // Easy test version of CallScript
    private static CallScript CallScriptGenerator()
    {
        return new CallScript()
        {
            callModel = new()
            {
                CallType = "Accident",
                Address = "10 Main Street",
                Caller_info = "Ted",
                Description = "Red pickup vs white unicorn",

            },
            NumberUnitsNeeded = 2,
            CallCommentsToAdd = new List<string> { "blah", "blah, blah", "blah, blah, blah" }
        };
    }

    // Decide what action to take:
    private async Task TakeCallAction(CallScriptTracker callTracker)
    {
        // is call created?
        if (callTracker.IsCallCreated == false)
        {
            int? returnedCallId = await _callService.CreateAsync(callTracker.CallScript.callModel, _dispatcherToken!);
            if (returnedCallId != null)
            {
                callTracker.SetCallId((int)returnedCallId);
            }
                
        }
        // are all units assigned?
        else if (callTracker.AreEnoughUnitsAssigned() == false)
        {
            // get a list of available units
            List<UnitReadDTO>? availableUnits = await _unitService.GetAllAvailableAsync();

            if(availableUnits != null && availableUnits.Count > 0)
            {
                List<UnitReadDTO> unitsToAssign;
                if(availableUnits.Count > callTracker.UnitsNeeded)
                {
                    unitsToAssign = availableUnits.Take(callTracker.UnitsNeeded).ToList();
                }
                else
                {
                    unitsToAssign = availableUnits;
                }
                    
                foreach (var unit in unitsToAssign)
                {
                    bool unitAssignmentSuccessful = 
                        await _unitService.AssignUnitToCall(unit.UnitNumber, (int)callTracker.CallId!, _dispatcherToken!);

                    if (unitAssignmentSuccessful)
                    {
                        callTracker.AssignUnit(unit.UnitNumber);
                    }
                }     
            }
           
        }
        // are all units en route?

        // and units on scene?

        // are all call comments added?

        // has call been deleted from api?

    }

    // Use of Random to decide whether a new call will be created:
    private bool IsNewScriptBeingAdded(int listCount, int listMax, int iterationsFalse)
    {
        var randomDouble = _random.NextDouble();
        var chanceComputation = listCount < listMax ?
             (randomDouble + (iterationsFalse * .2)) / (listCount * .2 + .8) :
                0.0;
        _log.LogInformation($"random double generated: {chanceComputation}");
        return chanceComputation > .9;
    }
}
