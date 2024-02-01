using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;
using System.Linq.Expressions;

namespace StackOverflow.BL.Services
{
    public interface IQuestionService
    {
        Task CreateQuestion(Question question);
        Task UpdateQuestion(Question question);
        Task DeleteQuestion(Question question);
        Task<Question> GetQuestionById(Guid qestionId);
        Task<IList<Question>> GetQuestionsByUserId(Guid userId);
        Task<IList<Question>> GetAllQuestions();
        Task<IList<Question>> GetQuestions(Expression<Func<Question, bool>> filters);
        Task UpdateQuestionVote(Guid questionId, Guid userId, VoteType voteType);


    }
}
