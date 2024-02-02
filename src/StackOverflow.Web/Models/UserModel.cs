using StackOverflow.BL.Services;
using StackOverflow.DAL.Entities;

namespace StackOverflow.Web.Models
{
    public class UserModel
    {
        private IUserService _userService;
        public int Id { get; set; }

        public IList<Question> Questions { get; set; }

        public IList<Answer> Answers { get; set; }

        public IList<QuestionVote> QuestionVotes { get; set; }

        public IList<AnswerVote> AnswerVotes { get; set;}

        public UserModel() 
        { 
        }
        public UserModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> GetUserById(Guid id)
        {
           return await _userService.GetUserById(id);
        }
    }
}
