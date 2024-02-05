using Autofac;
using StackOverflow.BL.Exceptions;
using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace StackOverflow.Web.Models.AnswerModels
{
    public class AnswerUpdateModel
    {
        private IAnswerService _answerService;

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "{0} should be atleast {1} character long")]
        [MaxLength(2000, ErrorMessage = "{0} should have atmost {1} character")]
        public string Body { get; set; }

        public Guid QuestionId { get; set; }
        public Guid UserId { get; set; }

        public AnswerUpdateModel()
        {

        }

        public AnswerUpdateModel(
            IAnswerService answerService
            )
        {
            _answerService = answerService;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _answerService = scope.Resolve<IAnswerService>();
        }
        public async Task LoadAnswer(Guid id)
        {
            var answer = await _answerService.GetAnswerById(id) ?? throw new NotFoundException("Answer not found");
            Id = answer.Id;
            Body = answer.Body;
            QuestionId = answer.Question.Id;
        }

        public async Task LoadAnswer(Guid id, Guid userId)
        {
            var answer = await _answerService.GetAnswerById(id) ?? throw new NotFoundException("Answer not found");

            if (answer.User.Id == userId)
            {
                Id = answer.Id;
                Body = answer.Body;
            }
            else
            {
                throw new PermissionMissingException("You do not have permission to edit this answer");
            }
        }

        public async Task UpdateAnswer(Guid userId)
        {
            var answer = new Answer();
            answer.Id = Id;
            answer.Body = Body;
            await _answerService.UpdateAnswerByUser(answer, userId);
        }

        public async Task UpdateAnswerByAdmin()
        {
            var answer = new Answer();
            answer.Id = Id;
            answer.Body = Body;
            await _answerService.UpdateAnswer(answer);
        }
    }
}
