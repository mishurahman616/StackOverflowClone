using StackOverflow.BL.DTOs;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;

namespace StackOverflow.BL.Services
{
    public interface IAnswerService
    {
        Task AddAnswer(Answer answer);
        Task UpdateAnswer(Answer answerToUpdate);
        Task UpdateAnswerByUser(Answer answer, Guid userId);
        Task<Answer> GetAnswerById(Guid id);
        Task DeleteAnswer(Answer answer);
        Task<VoteResponse> UpdateAnswerVote(Guid answerId, Guid userId, VoteType voteType);
        Task<int> GetAnswerVoteCount(Guid answerId);
    }
}
