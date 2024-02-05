using Autofac;
using StackOverflow.BL.Exceptions;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;
using System.Linq.Expressions;

namespace StackOverflow.Web.Models.QuestionModels
{
    public class QuestionListModel
    {
        private IQuestionService _questionService;

        public string? SearchText { get; set; }
        public int? TotalQuestion {  get; set; }
        public int? TotalToDisplay { get; set; }
        public int? TotalPage { get; set; }
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public IList<Question>? Questions { get; set; }

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


        public async Task<(IList<Question> questions, int total, int totalToDislplay, int totalPages)> GetPaginated(string search=null, int pageIndex=0, int pageSize=10)
        {
            Expression<Func<Question, bool>> query = null;

            if (search!=null)
            {
                query = x => x.Title.Contains(search) || x.Body.Contains(search);
            }
            
            return await _questionService.GetPaginated(query, pageIndex, pageSize);
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
