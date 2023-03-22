using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Service;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ComputerAidedDispatchAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DispatchersController : ControllerBase
{
    private readonly IDispatcherService _dispatcherService;
    protected APIResponse _response;

    public DispatchersController(IDispatcherService dispatcherService)
    {
        _dispatcherService = dispatcherService;
        _response = new();
    }


    // Get: api/Dispatchers
    [HttpGet]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetDispatchers()
    {
        try
        {
            var dispatcherList = await _dispatcherService.GetAllDispatchersAsync();

            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.Result = dispatcherList;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    
    [HttpGet("{dispatcherNumber}")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDispatcher(string dispatcherNumber)
    {
        try
        {
            var dispatcher = _dispatcherService.GetByDispatcherNumberAsync(dispatcherNumber).Result;

            if (dispatcher == null)
            {
                _response.ErrorMessages.Add($"Failed to find dispatcher with the dispatcherNumber of {dispatcherNumber}");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = dispatcher;
                return Ok(_response);
            }
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    
    [HttpPost]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateDispatcher([FromBody] DispatcherCreateDTO createDTO)
    {
        try
        {
            var creationResponse = _dispatcherService.CreateAsync(createDTO).Result;

            if (creationResponse == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"Failed to create new Dispatcher with dispatcherNumber:" +
                    $" {createDTO.DispatcherNumber} and userId: {createDTO.UserId}");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = creationResponse;
                return Ok(creationResponse);
            }
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    
    [HttpPost("CreateDispatcherAndUser")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateDispatcherAndUser([FromBody] DispatcherAndUserCreateDTO createDTO)
    {
        try
        {
            var creationResponse = _dispatcherService.CreateDispatcherAndUser(createDTO).Result;

            if (creationResponse == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Failed to create new Dispatcher and User with the given createDTO");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = creationResponse;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                return Ok(_response);
            }
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    
    [HttpDelete("{dispatcherNumber}")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDispatcher(string dispatcherNumber)
    {
        try
        {

            var dispatcherToDelete = _dispatcherService.GetByDispatcherNumberAsync(dispatcherNumber);

            if (dispatcherToDelete == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"Unable to find dispatcher with DispatcherNumber: {dispatcherNumber}");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            else
            {
                await _dispatcherService.DeleteAsync(dispatcherNumber);
                return NoContent();
            }
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }
}
