using Autofac;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.QuestionModels
{
    public class QuestionUpdateModel
    {
        private IQuestionService _questionService;

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "{0} should be atleast {1} character long")]
        [MaxLength(100, ErrorMessage = "{0} should have atmost {1} character")]
        public string Title { get; set; }
        [Required]
        [MinLength(220, ErrorMessage = "{0} should be atleast {1} character long")]
        [MaxLength(1000, ErrorMessage = "{0} should have atmost {1} character")]
        public string Body { get; set; }

        public QuestionUpdateModel()
        {

        }

        public QuestionUpdateModel(IQuestionService questionService,
            IUserService userService
            ) 
        {
            _questionService = questionService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _questionService = scope.Resolve<IQuestionService>();
        }

        public async Task LoadQuestion(Guid id, Guid userId)
        {
            var quesiton = await _questionService.GetQuestionById(id);
            if( quesiton != null && quesiton.User.Id == userId) 
            {
                Id = quesiton.Id;
                Title = quesiton.Title;
                Body = quesiton.Body;
            }
        }
        public async Task UpdateQuestion(Guid userId)
        {
            var question = new Question();
            question.Id = Id;
            question.Title = Title;
            question.Body = Body;
            await _questionService.UpdateQuestionByUser(question, userId);
        }
    }
}
