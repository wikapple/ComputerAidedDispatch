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
using ComputerAidedDispatchAPI.Models.DTOs.CallCommentDTOs;
using ComputerAidedDispatchAPI.Service;
using Microsoft.AspNetCore.Authorization;

namespace ComputerAidedDispatchAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallCommentsController : ControllerBase
{
    ICallCommentService _commentService;
    APIResponse _response;

    public CallCommentsController(ICallCommentService commentService)
    {
        _commentService = commentService;
        _response = new();
    }

    // GET: api/CallComments
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<CallComment>>> GetCallComments([FromQuery(Name = "callId")]int? callNumber)
    {
        try
        {

            List<CallCommentReadDTO> commentList = await _commentService.GetAllAsync(callNumber);
            _response.Result = commentList;
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);

        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    // GET: api/CallComments/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CallComment>> GetCallComment(int id)
    {
        try
        {
            var comment = await _commentService.GetAsync(id);
            if (comment == null)
            {
                _response.ErrorMessages.Add($"Unable to retrieve CallComment with an Id of {id}");
                _response.IsSuccess = false;
                _response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            else
            {
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.Result = comment;
                return Ok(_response);
            }
        }
        catch(Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    

    // POST: api/CallComments
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<CallComment>> PostCallComment(CreateCallCommentDTO createDTO)
    {
        try
        {


            var response = await _commentService.CreateAsync(createDTO);
            if (response == null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"Failed to create new CallComment for CallId {createDTO.CallNumber}");
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


}
