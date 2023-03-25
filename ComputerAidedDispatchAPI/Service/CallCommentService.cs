using AutoMapper;
using ComputerAidedDispatchAPI.Models;
using ComputerAidedDispatchAPI.Models.DTOs.CallCommentDTOs;
using ComputerAidedDispatchAPI.Repository.IRepository;
using ComputerAidedDispatchAPI.Service.IService;

namespace ComputerAidedDispatchAPI.Service
{
    public class CallCommentService: ICallCommentService
    {

        private readonly ICallCommentRepository _commentRepository;
        private readonly ICadSharedService _sharedService;
        private readonly IMapper _mapper;
        public CallCommentService(ICallCommentRepository callCommentRepository, ICadSharedService sharedService, IMapper mapper)
        {
            _commentRepository= callCommentRepository;
            _sharedService= sharedService;
            _mapper= mapper;
        }

        public async Task<CallCommentReadDTO?> CreateAsync(CreateCallCommentDTO createCallCommentDTO, string userName)
        {
            string? userId = _sharedService.GetUserIdByUserName(userName);
            if ( userId != null &&
                await _sharedService.DoesCallForServiceExist(createCallCommentDTO.CallNumber))
             {
                CallComment newComment = new()
                {
                    Comment = createCallCommentDTO.Comment,
                    TimeCreated = DateTime.Now,
                    CallId = createCallCommentDTO.CallNumber,
                    userId = userId
                };

                var response = await _commentRepository.CreateAsync(newComment);

                return _mapper.Map<CallCommentReadDTO>(response); 
            }
            return null;
        }

        public async Task<List<CallCommentReadDTO>> GetAllAsync(int? callId = null)
        {
            List<CallComment> commentList;
            if (callId == null)
            {
                commentList = await _commentRepository.GetAllAsync();
            }
            else
            {
                commentList = await _commentRepository.GetAllAsync(comment => comment.CallId == callId); 
            }

            return commentList.Select(comment => _mapper.Map<CallCommentReadDTO>(comment)).ToList();
        }

        public async Task<CallCommentReadDTO?> GetAsync(int commentId)
        {
            var comment = await _commentRepository.GetAsync(comment => comment.Id == commentId);

            return _mapper.Map<CallCommentReadDTO>(comment);
        }
    }
}
