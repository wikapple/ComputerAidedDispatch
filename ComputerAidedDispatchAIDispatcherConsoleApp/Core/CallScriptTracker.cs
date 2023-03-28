using ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core
{
    public class CallScriptTracker
    {
        
        private CallScript _callScript;
        public CallScript CallScript { get { return _callScript; } }

        private DateTime dateTimeLastUpdated;
        private double timeoutPeriod = 0;
        private readonly Random _random;


        // Constructor
        public CallScriptTracker(CallScript callScript)
        {
            _callScript = callScript;
            _random = new Random();
        }


        public bool IsTimeoutPeriodOver
        {
            get
            {
                return DateTime.Now.Subtract(dateTimeLastUpdated).TotalSeconds > timeoutPeriod;
            }
        }

        // Base wait is between 0 and 100 seconds to move to next step:
        private void ActionTaken(double timeoutModifier = 1, double minimumSecondsTimeout = 0)
        {
            dateTimeLastUpdated = DateTime.Now;
            var timeoutCalculated = (_random.NextDouble() * 100 * timeoutModifier);
            timeoutPeriod = timeoutCalculated > minimumSecondsTimeout ? timeoutCalculated : minimumSecondsTimeout;

        }

        // Step 1: Create the call:
        private int? callId = null;


        public bool IsCallCreated
        {
            get { return callId != null; }
        }

        public void SetCallId(int id)
        {
            callId ??= id;
            ActionTaken(timeoutModifier : .2);
        }
        public int? CallId { get { return callId; } }


        // Step 2: Assign units:
        Dictionary<string, string> assignedUnitsStatus= new Dictionary<string, string>();
        public List<string> getUnitsByStatus(string status)
        {
            return assignedUnitsStatus.Keys.Where(key => assignedUnitsStatus.GetValueOrDefault(key) == status).ToList();
        }

        public bool AreEnoughUnitsAssigned()
        {
            return assignedUnitsStatus.Count >= _callScript.NumberUnitsNeeded;
        }
        public int UnitsNeeded
        {
            get{ return _callScript.NumberUnitsNeeded - assignedUnitsStatus.Count; }
        }
        public void AssignUnit(string unitNumber)
        {
            assignedUnitsStatus.Add(unitNumber, "Assigned");

            if(AreEnoughUnitsAssigned())
            {
                ActionTaken(timeoutModifier: .5);
            }
        }

        // Step 3: Show units en route:        
        public bool AllUnitsEnRoute()
        {
            return assignedUnitsStatus.Values.All(status  => (status == "En Route") || (status == "On Scene"));
        }
        public void ShowUnitEnRoute(string unitNumber)
        {
            assignedUnitsStatus[unitNumber] = "En Route";

            if (AllUnitsEnRoute())
            {
                ActionTaken(minimumSecondsTimeout: 20);
            }
        }

        // Step 4:Put units on scene
        public bool AllUnitsOnScene()
        {
            return assignedUnitsStatus.Values.All(status => status == "On Scene");
        }
        public void ShowUnitOnScene(string unitNumber)
        {
            assignedUnitsStatus[unitNumber] = "On Scene";

            if (AllUnitsEnRoute())
            {
                ActionTaken(minimumSecondsTimeout: 20);
            }
            else
            {
                ActionTaken(timeoutModifier: .5);
            }
        }
        // Step 5: add call comments
        private int callCommentsAdded = 0;

        public bool AllCommentsAdded()
        {
            return _callScript.CallCommentsToAdd == null ||
                _callScript.CallCommentsToAdd.Count() <= callCommentsAdded; 
        }
        public string? GetNextCallCommentToAdd()
        {
            return AllCommentsAdded() ? null : _callScript.CallCommentsToAdd[callCommentsAdded]; 
        }
        public void SetCallCommentAdded()
        {
            callCommentsAdded++;

            if(AllCommentsAdded())
            {
                ActionTaken(timeoutModifier: 1.5, minimumSecondsTimeout: 20);
            }
            else
            {
                ActionTaken(timeoutModifier: .75, minimumSecondsTimeout: 10);
            }
        }

        // step 6: close the call
        private bool isCallClosed = false;
        public bool IsCallClosed { get { return isCallClosed; } }
        public void SetCallAsClosed()
        {
            isCallClosed = true;
        }

        
    }
}
