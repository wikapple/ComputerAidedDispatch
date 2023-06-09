﻿using ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore;
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
            // Check if each callTracker's timeout period is over, if so, take next step in the call script:
            foreach (var callTracker in _currentCallTrackers.Where(ct => ct.IsTimeoutPeriodOver))
            {               
                    await TakeCallAction(callTracker);
            }

            // Remove any call scripts from the list that are finished:
            _currentCallTrackers.RemoveAll(callTracker => callTracker.IsCallClosed);
            

            // Decide whether to add more calls or not to _currentCallRunners
            if(IsNewScriptBeingAdded(_currentCallTrackers.Count, 12, loopIterationsWithoutAddingCall))
            {
                loopIterationsWithoutAddingCall = 0;

                _currentCallTrackers.Add(new CallScriptTracker(CallScriptGenerator()));

            }
            else
            {
                loopIterationsWithoutAddingCall++;
            }

            // Sleep 5 seconds:
            Thread.Sleep(5000);

        }
        return;

    }

    // Easy test version of CallScript
    private static CallScript CallScriptGenerator()
    {
        return RandomCallScriptGenerator.GetRandomCallScript();
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
            List<UnitDetailsReadDTO>? availableUnits = await _unitService.GetAllAvailableAsync();

            availableUnits = availableUnits.OrderBy(unit => unit.UpdatedDate).ToList();

            if (availableUnits != null && availableUnits.Count > 0)
            {
                List<UnitDetailsReadDTO> unitsToAssign;
                if (availableUnits.Count > callTracker.UnitsNeeded)
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
        else if (callTracker.AllUnitsEnRoute() == false)
        {
            List<string> unitNumbersAssigned = callTracker.getUnitsByStatus("Assigned");

            foreach (var unitNumber in unitNumbersAssigned)
            {
                bool statusChangeSuccess = await _unitService.UpdateUnitStatus(unitNumber, "En Route", _dispatcherToken!);

                if (statusChangeSuccess)
                {
                    callTracker.ShowUnitEnRoute(unitNumber);
                }
            }
        }
        // and units on scene?
        else if (callTracker.AllUnitsOnScene() == false)
        {
            List<string> unitNumbersEnRoute = callTracker.getUnitsByStatus("En Route");

            string randomUnitNumber = unitNumbersEnRoute[_random.Next(unitNumbersEnRoute.Count)];


            bool statusChangeSuccess = await _unitService.UpdateUnitStatus(randomUnitNumber, "On Scene", _dispatcherToken!);

            if (statusChangeSuccess)
            {
                callTracker.ShowUnitOnScene(randomUnitNumber);
            }

        }
        // are all call comments added?
        else if (callTracker.AllCommentsAdded() == false)
        {
            string commentToAdd = callTracker.GetNextCallCommentToAdd()!;
            bool commentAddedSuccessfully = await _commentService.CreateAsync(commentToAdd, (int)callTracker.CallId!, _dispatcherToken!);
            if (commentAddedSuccessfully)
            {
                callTracker.SetCallCommentAdded();
            }
        }
        // has call been deleted from api?
        else if (callTracker.IsCallClosed == false)
        {
            bool callDeletedSuccessfully = await _callService.DeleteAsync((int)callTracker.CallId!, _dispatcherToken!);

            if (callDeletedSuccessfully)
            {
                callTracker.SetCallAsClosed();
            }
        }
        
    }

    // Use of Random to decide whether a new call will be created:
    private bool IsNewScriptBeingAdded(int listCount, int listMax, int iterationsFalse)
    {
        var randomDouble = _random.NextDouble();
        var chanceComputation = listCount < listMax ?
             (randomDouble + (iterationsFalse * .05)) / (listCount * .5 + .8) :
                0.0;
        _log.LogInformation($"random double generated: {chanceComputation}");
        return chanceComputation > .9;
    }
}
