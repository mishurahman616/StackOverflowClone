using NHibernate;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Repositories.Interfaces;

namespace StackOverflow.DAL.Repositories
{
    public class AnswerRepository: Repository<Answer, Guid>, IAnswerRepository
    {
        public AnswerRepository(ISession session) : base(session) { }
    }
}
