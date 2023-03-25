using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp
{
    public interface IAuthentication
    {
        public Task<string> GetAuthenticationToken();
        public Task PrintAllUnits();
    }
}
