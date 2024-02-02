using Autofac;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;

namespace StackOverflow.Web.Models.QuestionModels
{
    public class QuestionDetailsModel
    {
        private IQuestionService _questionService;
        public Question Question { get; set; }

        public QuestionDetailsModel()
        {

        }

        public QuestionDetailsModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _questionService = scope.Resolve<IQuestionService>();
        }

        public async Task LoadQuestion(Guid id)
        {
            Question = await _questionService.GetQuestionById(id);
        }
    }
}
