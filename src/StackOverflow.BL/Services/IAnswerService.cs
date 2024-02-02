using StackOverflow.BL.DTOs;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;

namespace StackOverflow.BL.Services
{
    public interface IAnswerService
    {
        Task AddAnswer(Answer answer);
        Task<VoteUpdateStatus> UpdateAnswerVote(Guid answerId, Guid userId, VoteType voteType);
    }
}
