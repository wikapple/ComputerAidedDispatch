using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Core.ICore
{
    public interface ICadSimulator
    {

        Task Start(string dispatcherToken);
    }
}
