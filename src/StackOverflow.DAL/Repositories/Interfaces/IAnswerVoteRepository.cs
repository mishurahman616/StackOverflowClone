using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Repositories.Interfaces
{
    public interface IAnswerVoteRepository: IRepository<AnswerVote, Guid>
    {
    }
}
