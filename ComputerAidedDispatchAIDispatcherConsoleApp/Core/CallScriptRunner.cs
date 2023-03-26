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
    public class CallScriptRunner : ICallScriptRunner
    {

        private readonly string _dispatcherToken;
        private readonly ICallForServiceService _callService;
        private readonly IUnitService _unitService;
        private readonly ICallCommentService _commentService;


        private int currentStep = 1;
        private int maxSteps;
        private CallScript _callScript;
        private DateTime dateTimeLastUpdated;
        private double timeoutPeriod;
        

        

        public bool IsScriptRunnerComplete 
        {
            get
            {
                return currentStep > maxSteps;
            }
                
        }

        public bool IsTimeoutPeriodOver 
        {
            get
            {
                return DateTime.Now.Subtract(dateTimeLastUpdated).TotalSeconds > timeoutPeriod;
            }
        }

        public CallScriptRunner(CallScript callScript, ICallForServiceService callService, IUnitService unitService, ICallCommentService commentService) 
        {
            _callScript = callScript;
            _callService = callService;
            _unitService = unitService;
            _commentService = commentService;
            

            // Max Steps:
            // (1) Create Call
            // (1 * n) Assign units to call assigning 1 unit == 1 step
            // (1 * n) Units arrive on scene 1 unit arrival == 1 step
            // (1 * c) Add call comments (1 comment add = 1 step) 
            maxSteps = 1 + callScript.NumberUnitsNeeded * 2 + callScript.CallCommentsToAdd.Count();

            timeoutPeriod = 0;
            dateTimeLastUpdated = DateTime.Now;
        }

        public Task TakeAction()
        {
            Console.WriteLine("hi");
            currentStep++;
            return Task.CompletedTask;
        }
    }
}
