using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;
using System.Runtime.CompilerServices;

namespace ComputerAidedDispatchAPI.Service;

public class UserService : IUserService
{

	private readonly IUserRepository _userRepository;
	
	public UserService(IUserRepository userRepo)
	{
		_userRepository= userRepo;
		CreateDefaultUsersIfNotExists();

	}

    public bool DoesUserIdExist(string userId)
    {
		return _userRepository.GetUser(userId) != null;
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
		return await _userRepository.Register(registrationRequestDTO);
	}

	private void CreateDefaultUsersIfNotExists()
	{
		if (!_userRepository.DoesDefaultSystemUserExist())
		{
			RegistrationRequestDTO defaultSystemUser = new()
			{
				UserName = "SystemTestUser",
				Name = "SystemUser",
				Password = "HELLOworld!!11",
				Roles = new List<String>{"system", "admin"}
			};

			_userRepository.Register(defaultSystemUser);
		}

	}

}
