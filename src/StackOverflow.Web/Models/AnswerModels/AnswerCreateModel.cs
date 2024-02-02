using Autofac;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.AnswerModels
{
    public class AnswerCreateModel
    {
        private IQuestionService _questionService;
        private IUserService _userService;

        [Required]
        [MinLength(10, ErrorMessage = "{0} should be atleast {1} character long")]
        [MaxLength(2000, ErrorMessage = "{0} should have atmost {1} character")]
        public string Body { get; set; }

        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }

        public AnswerCreateModel()
        {

        }

        public AnswerCreateModel(IQuestionService questionService,
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

        public async Task CreateAnswer()
        {
            var user = await _userService.GetUserById(UserId);
            var question = await _questionService.GetQuestionById(QuestionId);
            var answer = new Answer();
            answer.Body = Body;
            answer.User = user;
            answer.Question = question;

            await _questionService.AddAnswer(answer);
        }
    }
}
