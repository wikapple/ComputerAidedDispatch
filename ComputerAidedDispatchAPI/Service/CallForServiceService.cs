using AutoMapper;
using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;

namespace ComputerAidedDispatchAPI.Service
{
    public class CallForServiceService : ICallForServiceService
    {

        ICallForServiceRepository _callRepository;
        IMapper _mapper;

        public CallForServiceService(ICallForServiceRepository callRepository, IMapper mapper)
        {
            _callRepository = callRepository;
            _mapper = mapper;
        }

        // Create

        // Read One
        public async Task<CallForServiceReadDTO?> GetAsync(int callId)
        {
            var repositoryResponse = await _callRepository.GetAsync(x => x.Id == callId);

            return repositoryResponse == null ?
                null :
                _mapper.Map<CallForServiceReadDTO>(repositoryResponse);


            
        }


        // Read All

        // Update

        // Delete
    }
}
