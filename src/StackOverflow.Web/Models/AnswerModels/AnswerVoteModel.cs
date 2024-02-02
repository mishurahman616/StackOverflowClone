using Autofac;
using StackOverflow.BL.DTOs;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Enums;

namespace StackOverflow.Web.Models.AnswerModels
{
    public class AnswerVoteModel
    {
        private IAnswerService _answerService;

        public Guid AnswerId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public AnswerVoteModel()
        {

        }

        public AnswerVoteModel(IAnswerService questionService)
        {
            _answerService = questionService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _answerService = scope.Resolve<IAnswerService>();
        }

        public async Task<VoteUpdateStatus> UpdateVote()
        {
            return await _answerService.UpdateAnswerVote(AnswerId, UserId, VoteType);
        }
    }
}
