using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Repositories.Interfaces
{
    public interface IQuestionVoteRepository: IRepository<QuestionVote, Guid>
    {
    }
}
