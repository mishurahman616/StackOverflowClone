using Autofac;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;

namespace StackOverflow.Web.Models.QuestionModels
{
    public class QuestionVoteModel
    {
        private IQuestionService _questionService;

        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }
        public VoteType VoteType { get; set; }

        public QuestionVoteModel()
        {

        }

        public QuestionVoteModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _questionService = scope.Resolve<IQuestionService>();
        }

        public async Task UpdateVote()
        {
           await _questionService.UpdateQuestionVote(QuestionId, UserId, VoteType);
        }
    }
}
