using NHibernate;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Repositories.Interfaces;

namespace StackOverflow.DAL.Repositories
{
    public class AnswerVoteRepository: Repository<AnswerVote, Guid>, IAnswerVoteRepository
    {
        public AnswerVoteRepository(ISession session) : base(session) { }
    }
}
