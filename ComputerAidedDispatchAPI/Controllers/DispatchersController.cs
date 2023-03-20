using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.DispatcherDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using ComputerAidedDispatchAPI.Service;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetDispatchers()
    {
        var dispatcherList = await _dispatcherService.GetAllDispatchersAsync();

        _response.IsSuccess = true;
        _response.StatusCode = System.Net.HttpStatusCode.OK;
        _response.Result = dispatcherList;

        return Ok(_response);
    }

    [HttpGet("{dispatcherNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDispatcher(string dispatcherNumber)
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateDispatcher([FromBody] DispatcherCreateDTO createDTO)
    {
        var creationResponse = _dispatcherService.CreateAsync(createDTO).Result;

        if(creationResponse == null)
        {
            _response.IsSuccess =false;
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

    [HttpPost("CreateDispatcherAndUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateDispatcherAndUser([FromBody] DispatcherAndUserCreateDTO createDTO)
    {
        var creationResponse = _dispatcherService.CreateDispatcherAndUser(createDTO).Result;

        if(creationResponse == null)
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

    [HttpDelete("{dispatcherNumber}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDispatcher(string dispatcherNumber)
    {
        var dispatcherToDelete = _dispatcherService.GetByDispatcherNumberAsync(dispatcherNumber);

        if(dispatcherToDelete == null)
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
}
