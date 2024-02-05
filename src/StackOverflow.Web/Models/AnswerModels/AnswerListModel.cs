using Autofac;
using StackOverflow.BL.Exceptions;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;

namespace StackOverflow.Web.Models.AnswerModels
{
    public class AnswerListModel
    {
        private IAnswerService _answerService;
        public List<Answer> Answers { get; set; }

        public AnswerListModel()
        {

        }

        public AnswerListModel(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _answerService = scope.Resolve<IAnswerService>();
        }

        public async Task DeleteAnswerByUser(Guid userId, Guid answerId)
        {
            var answer = await _answerService.GetAnswerById(answerId) ?? throw new NotFoundException("Answer Not Found");

            if(answer.User.Id == userId)
            {
                await _answerService.DeleteAnswer(answer);
            }
            else
            {
                throw new PermissionMissingException("You do not have permission to delete this answer");
            }

        }
        public async Task<Guid> DeleteAnswerByAdmin(Guid answerId)
        {
            var answer = await _answerService.GetAnswerById(answerId) ?? throw new NotFoundException("Answer Not Found");
            await _answerService.DeleteAnswer(answer);
            return answer.Question.Id;
        }
    }
}
