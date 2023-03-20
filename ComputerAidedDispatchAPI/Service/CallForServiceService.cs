using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.CallCommentDTOs;
using ComputerAidedDispatchAPI.Models.DTOs.CallForServiceDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ComputerAidedDispatchAPI.Service
{
    public class CallForServiceService : ICallForServiceService
    {

        private readonly ICallForServiceRepository _callRepository;
        private readonly IMapper _mapper;
        private readonly ICadSharedService _sharedService;
        public CallForServiceService(ICallForServiceRepository callRepository, IMapper mapper, ICadSharedService sharedService)
        {
            _callRepository = callRepository;
            _sharedService = sharedService;
            _mapper = mapper;
        }

        public async Task<List<CallForServiceReadDTO>> GetAllAsync(string? status = null)
        {
            if (status != null)
            {
                var callList = await _callRepository.GetAllAsync(x => x.Status == status, includeProperties: "Units");
                return callList.Select(c => _mapper.Map<CallForServiceReadDTO>(c)).ToList();    
            }
            else
            {
                var callList = await _callRepository.GetAllAsync(includeProperties: "Units");
                return callList.Select(c => _mapper.Map<CallForServiceReadDTO>(c)).ToList();
            }
        }

        public async Task<List<CallForServiceDetailsReadDTO>> GetAllDetailsAsync(string? status = null)
        {
            if (status != null)
            {
                var callList = await _callRepository.GetAllAsync(x => x.Status == status, includeProperties: "Units,CallComments");
                return callList.Select(c => _mapper.Map<CallForServiceDetailsReadDTO>(c)).ToList();
            }
            else
            {
                var callList = await _callRepository.GetAllAsync(includeProperties: "Units,CallComments");
                return callList.Select(c => _mapper.Map<CallForServiceDetailsReadDTO>(c)).ToList();
            }
        }

        // Read One
        public async Task<CallForServiceReadDTO?> GetAsync(int callId)
        {
            var repositoryResponse = await _callRepository.GetAsync(x => x.Id == callId, includeProperties: "Units");

            return repositoryResponse == null ?
                null :
                _mapper.Map<CallForServiceReadDTO>(repositoryResponse);

        }
        public async Task<CallForServiceDetailsReadDTO?> GetDetailsAsync(int callId)
        {
            var repositoryResponse = await _callRepository.GetAsync(x => x.Id == callId, includeProperties: "Units,CallComments");

            return repositoryResponse == null ?
                null :
                _mapper.Map<CallForServiceDetailsReadDTO>(repositoryResponse);
        }

        public async Task<CallForServiceReadDTO?> CreateAsync(CallForServiceCreateDTO createDTO)
        {
            CallForService newCallForService = new()
            {
                DateTimeCreated = DateTime.Now,
                Status = "Created",
                CallType = createDTO.CallType,
                Address = createDTO.Address,
                Caller_info = createDTO.Caller_info,
                Description = createDTO.Description
            };

            var callCreated = _callRepository.CreateAsync(newCallForService).Result;

            if (callCreated != null)
            {
                if (createDTO.AssignedUnitIds.Any())
                {
                    AssignUnits(callCreated.Id, createDTO.AssignedUnitIds);
                }

                var call = await _callRepository.GetAsync(cfs => cfs.Id == callCreated.Id);
                return _mapper.Map<CallForServiceReadDTO>(call);
            }
            else
            {
                return null;
            }
        }

        public async Task<CallForServiceReadDTO?> UpdateAsync(CallForServiceUpdateDTO updateDTO)
        {
            var CallToUpdate = await _callRepository.GetAsync(x => x.Id == updateDTO.Id);

            if (CallToUpdate != null)
            {
                CallToUpdate.CallType = updateDTO.CallType;
                CallToUpdate.Address = updateDTO.Address;
                CallToUpdate.Caller_info = updateDTO.Caller_info;
                CallToUpdate.Description = updateDTO.Description;

                var resultDispatcher = await _callRepository.UpdateAsync(CallToUpdate);

                return _mapper.Map<CallForServiceReadDTO?>(resultDispatcher);
            }
            else
            {
                return null;
            }
        }


        public async Task<CallForServiceReadDTO?> UpdateStatus(int callId, string status)
        {
            var call = await _callRepository.GetAsync(cfs => cfs.Id == callId);

            if(call != null)
            {
                call.Status = status;
                var resultDispatcher = await _callRepository.UpdateAsync(call);
                return _mapper.Map<CallForServiceReadDTO>(resultDispatcher);
            }
            else
            {
                return null;
            }
        }

        public async void AssignUnits(int callId, List<string> unitIds)
        {
            foreach (var unitId in unitIds)
            {
                if (_sharedService.GetByUnitNumberAsync(unitId) != null)
                    await _sharedService.AssignCallAsync(unitId, callId);
            }

          
              
        }
        public async void RemoveUnits(int callId, List<string> unitIds)
        {
            foreach (var unitId in unitIds)
            {
                var unit = await _sharedService.GetByUnitNumberAsync(unitId);
                if (unit != null && unit.CallNumber == callId)
                {
                    await _sharedService.AssignCallAsync(unitId, null);
                    await _sharedService.UpdateStatusAsync(unitId, "Available");
                }
            }
        }
        public Task PostComment(int callId, CreateCallCommentDTO createCommentDTO)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int callId)
        {
            var callToDelete = await _callRepository.GetAsync(cfs => cfs.Id == callId);
            if(callToDelete != null)
            {
                await _callRepository.RemoveAsync(callToDelete);
            }
        }

       
    }
}
