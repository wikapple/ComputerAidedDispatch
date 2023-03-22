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
using ComputerAidedDispatchAPI.Models.DTOs.UnitDTOs;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ComputerAidedDispatchAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UnitsController : ControllerBase
{
    private readonly IUnitService _unitService;
    protected APIResponse _response;

    public UnitsController( IUnitService unitService)
    {
        _unitService = unitService;
        _response = new();
    }

    // GET: api/Units
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUnits([FromQuery(Name = "callNumber")]int? callNumber, [FromQuery(Name = "status")]string? status, [FromQuery(Name = "getDetails")] bool getDetails = false)
    {
        try
        {
            if (getDetails)
            {
                List<UnitDetailsReadDTO> unitList = await _unitService.GetAllDetailsAsync(callNumber, status);
                _response.Result = unitList;
            }
            else
            {
                List<UnitReadDTO> unitList = await _unitService.GetAllAsync(callNumber, status);
                _response.Result = unitList;
            }
            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    // GET: api/Units/5
    [HttpGet("{unitNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUnit(string unitNumber, [FromQuery(Name = "getDetails")] bool getDetails = false)
    {
        try
        {

            if (getDetails)
            {
                _response.Result = await _unitService.GetDetailsByUnitNumberAsync(unitNumber);
            }
            else
            {
                _response.Result = await _unitService.GetByUnitNumberAsync(unitNumber);
            }
            if (_response.Result == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"Unit with an Id of {unitNumber} not found");
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
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }



    // PUT: api/Units/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{unitNumber}")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutUnit(string unitNumber, UnitUpdateDTO unitUpdateDTO)
    {
        try
        {
            if (unitNumber != unitUpdateDTO.UnitNumber)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Unit numbers from unitNumber and unitUpdateDTO parameters do not match");
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            var result = await _unitService.UpdateAsync(unitUpdateDTO);

            if (result == null)
            {
                _response.ErrorMessages.Add($"Unit not found with the unit number - {unitNumber}");
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
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    // PUT: api/Units/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{unitNumber}/UpdateStatus/{status}")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(string unitNumber, string status)
    {
        try
        {

            var result = await _unitService.UpdateStatusAsync(unitNumber, status);

            if (result == null)
            {
                _response.ErrorMessages.Add($"Unit not found with the unit number - {unitNumber}");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = result;
                return Ok(_response);
            }
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    // PUT: api/Units/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{unitNumber}/AssignCallNumber/{callNumber:int}")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignCallNumber(string unitNumber, int? callNumber)
    {
        try
        {
            var result = await _unitService.AssignCallAsync(unitNumber, callNumber);

            if (result == null)
            {
                _response.ErrorMessages.Add($"Unit not found with the unit number - {unitNumber}");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.Result = result;
                return Ok(_response);
            }
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    // POST: api/Units
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateUnit([FromBody] UnitCreateDTO createDTO)
    {
        try
        {

            var response = await _unitService.CreateAsync(createDTO);
            if (response == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"Failed to create new Unit with unit number: {createDTO.UnitNumber} and userId: {createDTO.UserId}");
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
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    [HttpPost("CreateUnitAndUser")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateUnitAndUser([FromBody] UnitAndUserCreateDTO createDTO)
    {
        try
        {

            var response = await _unitService.CreateUnitAndUserAsync(createDTO);
            if (response == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Failed to create new Unit and User with createDTO");
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
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    // DELETE: api/Units/5
    [HttpDelete("{unitNumber}")]
    [Authorize(Roles = "system,admin,dispatcher")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUnit(string unitNumber)
    {
        try
        {

            var unit = await _unitService.GetByUnitNumberAsync(unitNumber);

            if (unit == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"Unable to find unit with UnitNumber: {unitNumber}");
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            else
            {
                await _unitService.DeleteAsync(unitNumber);
                return NoContent();
            }
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

}
