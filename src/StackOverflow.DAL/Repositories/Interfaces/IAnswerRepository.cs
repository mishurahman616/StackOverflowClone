using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Repositories.Interfaces
{
    public interface IAnswerRepository: IRepository<Answer, Guid>
    {
    }
}
