using Autofac;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.QuestionModels
{
    public class QuestionCreateModel
    {
        private IQuestionService _questionService;
        private IUserService _userService;

        [Required]
        [MinLength(6, ErrorMessage = "{0} should be atleast {1} character long")]
        [MaxLength(100, ErrorMessage = "{0} should have atmost {1} character")]
        public string Title { get; set; }
        [Required]
        [MinLength(100, ErrorMessage = "{0} should be atleast {1} character long")]
        [MaxLength(2000, ErrorMessage = "{0} should have atmost {1} character")]
        public string Body { get; set; }

        public Guid UserId { get; set; }

        public QuestionCreateModel()
        {

        }

        public QuestionCreateModel(IQuestionService questionService,
            IUserService userService
            ) 
        {
            _questionService = questionService;
            _userService = userService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _questionService = scope.Resolve<IQuestionService>();
            _userService = scope.Resolve<IUserService>();
        }

        public async Task CreateQuestion()
        {
            var user = await _userService.GetUserById(UserId);
            var question = new Question();
            question.Title = Title;
            question.Body = Body;
            question.User = user;
            await _questionService.CreateQuestion(question);
        }
    }
}
