using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Service.IService;
using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAPI.Service;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ComputerAidedDispatchAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallsForServiceController : ControllerBase
{
    private readonly ICallForServiceService _callService;
    protected APIResponse _response;

    public CallsForServiceController(ICallForServiceService callService)
    {
        _callService = callService;
        _response = new();
    }

    // GET: api/CallsForService
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetCallsForService([FromQuery(Name = "getDetails")] bool getDetails = false)
    {
        if (getDetails)
        {
            List<CallForServiceDetailsReadDTO> callList = await _callService.GetAllDetailsAsync();
            _response.Result = callList;
        }
        else
        {
            List<CallForServiceReadDTO> callList = await _callService.GetAllAsync();
            _response.Result = callList;
        }
        _response.IsSuccess = true;
        _response.StatusCode = System.Net.HttpStatusCode.OK;
        return Ok(_response);
    }

    // GET: api/CallsForService/5
    [HttpGet("{callId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCallForService(int callId, [FromQuery(Name = "getDetails")] bool getDetails = false)
    {
        if (getDetails)
        {
            _response.Result = await _callService.GetDetailsAsync(callId);
        }
        else
        {
            _response.Result = await _callService.GetAsync(callId);
        }

        if (_response.Result == null)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages.Add($"CallForService with an Id of {callId} not found");
            _response.StatusCode = System.Net.HttpStatusCode.NotFound;
            return NotFound(_response);
        }
        else
        {
            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            return Ok(_response);
        }
        
    }

    // PUT: api/CallsForService/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{callId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutCallForService(int callId, CallForServiceUpdateDTO updateDTO)
    {
        if (callId != updateDTO.Id)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Id from callId and CallForServiceUpdateDTO parameters do not match");
            _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }

        var result = await _callService.UpdateAsync(updateDTO);

        if(result == null)
        {
            _response.ErrorMessages.Add($"Failed to update CallForService with Id {callId}, verify that the call for service exists");
            _response.StatusCode = System.Net.HttpStatusCode.NotFound;
            _response.IsSuccess = false;
            return NotFound(_response);
        }
        else
        {
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = result;
            return Ok(_response);
        }
    }

    // POST: api/CallsForService
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateCallForService(CallForServiceCreateDTO createDTO)
    {
        var response = await _callService.CreateAsync(createDTO);
        if (response == null)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages.Add($"Failed to create new CallForService");
            _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            return BadRequest(_response);
        }
        else
        {
            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = response;
            return Ok(_response);
        }
    }

    // DELETE: api/CallsForService/5
    [HttpDelete("{Id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCallForService(int Id)
    {
        var callToDelete = await _callService.GetAsync(Id);

        if (callToDelete == null)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages.Add($"Failed to find CallForService with Id of {Id}");
            _response.StatusCode = System.Net.HttpStatusCode.NotFound;
            return NotFound(_response);
        }
        else
        {
            await _callService.DeleteAsync(Id);
            return NoContent();
        }
    }
}
