using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallForServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Models;

public class CallScript
{
    public CallForServiceCreateDTO callModel { get; set; }
    public int NumberUnitsNeeded { get; set; }
    public List<string> CallCommentsToAdd { get; set; }
}
