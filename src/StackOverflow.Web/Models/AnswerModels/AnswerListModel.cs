using Autofac;
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
            var answer = await _answerService.GetAnswerById(answerId);
            if(answer != null && answer.User.Id == userId)
            {
                await _answerService.DeleteAnswer(answer);
            }
            else
            {
                throw new Exception("Answer Delete Failed");
            }

        }
    }
}
