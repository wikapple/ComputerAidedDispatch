﻿using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Data;

namespace ComputerAidedDispatchAPI.Controllers;

[Route("api/UsersAuth")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;
    protected APIResponse _response;
    private ComputerAidedDispatchContext _fullDB;
    public UserController(IUserService userService, ComputerAidedDispatchContext fullDB)
    {
        _userService = userService;
        _response = new();
        _fullDB = fullDB;
    }

    [HttpGet]
    [Authorize(Roles = "system,admin,dispatcher")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = _fullDB.Users.ToList();
            _response.Result = users;
            _response.IsSuccess = true;
            _response.StatusCode = System.Net.HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        try
        {

            var loginResponse = await _userService.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username or password is incorrect");
                return BadRequest(_response);
            }
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }

    [HttpPost("register")]
    [Authorize(Roles = "system,admin,dispatcher")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
    {
        try
        {
            bool isUserNameUnique = _userService.IsUniqueUser(model.UserName);
            if (!isUserNameUnique)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Username already exists");
                return BadRequest(_response);
            }

            var user = await _userService.Register(model);
            if (user == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Error while registering");
                return BadRequest(_response);
            }
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            return Problem(ex.ToString());
        }
    }
}
