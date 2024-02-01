using StackOverflow.DAL.Entities;

namespace StackOverflow.BL.Services
{
    public interface IQuestionService
    {
        Task CreateQuestion(Question question);
    }
}
