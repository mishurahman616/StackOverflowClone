using Autofac;
using StackOverflow.BL.Exceptions;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;

namespace StackOverflow.Web.Models.QuestionModels
{
    public class QuestionListModel
    {
        private IQuestionService _questionService;
        public List<Question> Questions { get; set; }

        public QuestionListModel()
        {

        }

        public QuestionListModel(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _questionService = scope.Resolve<IQuestionService>();
        }

        public async Task LoadQuestions()
        {
            Questions = (List<Question>)await _questionService.GetAllQuestions();
        }

        public async Task DeleteQuestionByUser(Guid userId, Guid questionId)
        {
            var question = await _questionService.GetQuestionById(questionId) ?? throw new NotFoundException("Question Not Found");

            if(question.User.Id == userId) 
            {
                await _questionService.DeleteQuestion(question);
            }
            else
            {
                throw new PermissionMissingException("You do not have permission to delete this quesiton");
            }
            
        }
    }
}
