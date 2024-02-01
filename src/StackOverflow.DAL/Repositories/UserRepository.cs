using NHibernate;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Repositories.Interfaces;

namespace StackOverflow.DAL.Repositories
{
    public class UserRepository: Repository<User, Guid>, IUserRepository
    {
        public UserRepository(ISession session): base(session)
        {

        }
    }
}
