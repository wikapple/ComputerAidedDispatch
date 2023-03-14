using ComputerAidedDispatchAPI.Data;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace ComputerAidedDispatchAPI.Controllers;

[Route("api/UsersAuth")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserRepository _userRepo;
    protected APIResponse _response;
    private ComputerAidedDispatchContext _fullDB;
    public UserController(IUserRepository userRepo, ComputerAidedDispatchContext fullDB)
    {
        _userRepo = userRepo;
        _response = new();
        _fullDB = fullDB;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = _fullDB.Users.ToList();
        _response.Result = users;
        _response.IsSuccess = true;
        _response.StatusCode = System.Net.HttpStatusCode.OK;

        return Ok(_response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        var loginResponse = await _userRepo.Login(model);
        if (loginResponse.user == null || string.IsNullOrEmpty(loginResponse.Token))
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

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
    {
        bool isUserNameUnique = _userRepo.IsUniqueUser(model.UserName);
        if (!isUserNameUnique)
        {
            _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Username already exists");
            return BadRequest(_response);
        }
        
        var user = await _userRepo.Register(model);
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
}
