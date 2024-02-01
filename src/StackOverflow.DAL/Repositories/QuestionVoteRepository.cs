using NHibernate;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Repositories.Interfaces;

namespace StackOverflow.DAL.Repositories
{
    public class QuestionVoteRepository: Repository<QuestionVote, Guid>, IQuestionVoteRepository
    {
        public QuestionVoteRepository(ISession session): base(session) { }
    }
}
