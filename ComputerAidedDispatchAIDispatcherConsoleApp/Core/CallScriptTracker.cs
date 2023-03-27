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
        public int CallId { get { return CallId; } }


        // Step 2: Assign units:
        private int unitsAssigned = 0;
        public bool AreEnoughUnitsAssigned()
        {
            return unitsAssigned >= _callScript.NumberUnitsNeeded;
        }
        public int UnitsNeeded()
        {
            return _callScript.NumberUnitsNeeded - unitsAssigned;
        }
        public void AssignUnit(int numberUnitsAssigned = 1)
        {
            unitsAssigned += numberUnitsAssigned;

            if(AreEnoughUnitsAssigned())
            {
                ActionTaken(timeoutModifier: .5);
            }
        }

        // Step 3: Show units en route:
        private int unitsEnRoute = 0;
        
        public bool AllUnitsEnRoute()
        {
            return unitsEnRoute >= _callScript.NumberUnitsNeeded;
        }
        public void increaseUnitsEnRoute(int unitsEnRoute = 1)
        {
            unitsEnRoute += unitsEnRoute;

            if (AllUnitsEnRoute())
            {
                ActionTaken(timeoutModifier: 2, minimumSecondsTimeout: 20);
            }
        }

        // Step 4:Put units on scene
        private int unitsOnScene = 0;

        public bool AllUnitsOnScene()
        {
            return unitsOnScene >= _callScript.NumberUnitsNeeded;
        }

        public void SetUnitOnScene(int unitsArriving = 1)
        {
            unitsOnScene += unitsArriving;

            if (AllUnitsOnScene())
            {
                ActionTaken(timeoutModifier: 2, minimumSecondsTimeout: 20);
            }
            else
            {
                ActionTaken();
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
                ActionTaken(timeoutModifier: 2, minimumSecondsTimeout: 20);
            }
            else
            {
                ActionTaken(minimumSecondsTimeout: 10);
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
