using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore
{
    public interface IUserBotFactory
    {
        public Task<string?> CreateDispatcherBotAndReturnToken();

        public Task<string?> CreateUnitBotAndReturnToken();
    }
}
