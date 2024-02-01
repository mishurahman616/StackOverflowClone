using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Repositories.Interfaces
{
    public interface IQuestionRepository: IRepository<Question, Guid>
    {
    }
}
