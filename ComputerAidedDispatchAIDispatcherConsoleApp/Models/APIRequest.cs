using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComputerAidedDispatch_UtilityLibrary.SD;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; }
        public string Url { get; set; }
        public object Data { get; set; }
        public string Token { get; set; }
    }
}
