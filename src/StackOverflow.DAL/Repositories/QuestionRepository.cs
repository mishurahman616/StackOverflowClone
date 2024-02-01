using NHibernate;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Repositories.Interfaces;

namespace StackOverflow.DAL.Repositories
{
    public class QuestionRepository: Repository<Question, Guid>, IQuestionRepository
    {
        public QuestionRepository(ISession session) : base(session) { }
    }
}
