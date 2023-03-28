using ComputerAidedDispatch_UtilityLibrary;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models;
using ComputerAidedDispatchAIDispatcherConsoleApp.Models.DTOs.CallCommentDTOs;
using ComputerAidedDispatchAIDispatcherConsoleApp.Services.IServices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerAidedDispatchAIDispatcherConsoleApp.Services;

public class CallCommentService : BaseService, ICallCommentService
{
    private readonly IHttpClientFactory _clientFactory;

    private string cadUrl;

    public CallCommentService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
    {
        _clientFactory = clientFactory;
        cadUrl = configuration.GetConnectionString("BaseApiUrl")!;
    }

    public async Task<bool> CreateAsync(string comment, int callId, string token)
    {
        CreateCallCommentDTO dto = new()
        {
            Comment = comment,
            CallNumber = callId
        };

        var response = await SendAsync<APIResponse>(new APIRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = dto,
            Url = $@"{cadUrl}/api/CallsComments",
            Token = token
        });

        return response != null && response.IsSuccess;
    }
}
