using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore
{
    public interface ICallScriptTracker
    {
        public bool IsScriptRunnerComplete { get; }
        public bool IsTimeoutPeriodOver { get; }
        public Task TakeAction();
    }
}
