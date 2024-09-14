using StackOverflow.BL.DTOs;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;
using System.Linq.Expressions;

namespace StackOverflow.BL.Services
{
    public interface IQuestionService
    {
        Task CreateQuestion(Question question);
        Task UpdateQuestion(Question question);
        Task UpdateQuestionByUser(Question questionToUpdate, Guid userId);
        Task DeleteQuestion(Question question);
        Task<Question> GetQuestionById(Guid qestionId);
        Task<IList<Question>> GetQuestionsByUserId(Guid userId);
        Task<IList<Question>> GetAllQuestions();
        Task<IList<Question>> GetQuestions(Expression<Func<Question, bool>> filters);
        Task<VoteResponse> UpdateQuestionVote(Guid questionId, Guid userId, VoteType voteType);
        Task<int> GetQuestionVoteCount(Guid questionId);
        Task<(IList<Question>questions, int total, int totalToDislplay, int totalPages)> GetPaginated(Expression<Func<Question, bool>>? predicate = null, int pageIndex = 1, int pageSize = 10);

    }
}
