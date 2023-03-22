using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Models.DTOs.UserDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Runtime.CompilerServices;

namespace ComputerAidedDispatchAPI.Service;

public class UserService : IUserService
{

	private readonly IUserRepository _userRepository;
	private readonly ICadSharedService _sharedService;
	private readonly IConfiguration _configuration;

	public UserService(IUserRepository userRepo, ICadSharedService sharedService, IConfiguration configuration)
	{
		_sharedService = sharedService;
		_userRepository = userRepo;
		_configuration = configuration;
		CreateDefaultUsersIfNotExists();

	}

    public bool DoesUserIdExist(string userId)
    {
		return _sharedService.DoesUserIdExist(userId);
    }

    public bool IsUniqueUser(string username)
	{
		return _userRepository.IsUniqueUser(username);
	}

	public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
	{
		return await _userRepository.Login(loginRequestDTO);
	}

	public async Task<UserDTO?> Register(RegistrationRequestDTO registrationRequestDTO)
	{
		return await _sharedService.Register(registrationRequestDTO);
	}

	private void CreateDefaultUsersIfNotExists()
	{
		if (!_userRepository.DoesDefaultSystemUserExist())
		{

            RegistrationRequestDTO defaultSystemUser = new()
            {
                UserName = _configuration.GetValue<string>("ApiSettings:Default-System-user:UserName")!,
				Name = _configuration.GetValue<string>("ApiSettings:Default-System-user:Name")!,
                Password = _configuration.GetValue<string>("ApiSettings:Default-System-user:Password")!,
                Roles = new List<String> { "system", "admin" }
            };

            
			
			/*
			RegistrationRequestDTO defaultSystemUser = new()
			{
				UserName = "SystemTestUser",
				Name = "SystemUser",
				Password = "HELLOworld!!11",
				Roles = new List<String>{"system", "admin"}
			};
			*/

			_userRepository.Register(defaultSystemUser);
		}

	}

}
