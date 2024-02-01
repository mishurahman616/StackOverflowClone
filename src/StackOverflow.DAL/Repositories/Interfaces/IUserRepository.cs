using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Repositories.Interfaces
{
    public interface IUserRepository: IRepository<User, Guid>
    {
    }
}
