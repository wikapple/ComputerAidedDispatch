﻿using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallCommentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;

public interface ICallCommentService
{
    Task<bool> CreateAsync(string comment, int callId, string token);

}
