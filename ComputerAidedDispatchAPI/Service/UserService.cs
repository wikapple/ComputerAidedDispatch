using ComputerAidedDispatchAPI.Models.DTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;
using System.Runtime.CompilerServices;

namespace ComputerAidedDispatchAPI.Service;

public class UserService : IUserService
{

	private readonly IUserRepository _userRepository;
	private readonly IUnitRepository _unitRepository;
	private readonly IDispatcherRepository _dispatcherRepository;
	public UserService(IUserRepository userRepo, IUnitRepository unitRepo, IDispatcherRepository dispatcherRepo)
	{
		_userRepository= userRepo;
		_unitRepository= unitRepo;
		_dispatcherRepository= dispatcherRepo;
		AddDefaultUsersIfNotExists();

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

	private async Task CreateDefaultUsersIfNotExists()
	{
		if (!_userRepository.DoesDefaultAIUserExist())
		{

		}

		if (!_userRepository.DoesDefaultTestUserExist())
		{

		}
	}

}
