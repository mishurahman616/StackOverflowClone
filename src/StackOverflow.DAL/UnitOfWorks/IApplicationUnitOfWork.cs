using StackOverflow.DAL.Repositories.Interfaces;

namespace StackOverflow.DAL.UnitOfWorks
{
    public interface IApplicationUnitOfWork : IUnitOfWorkBase
    {
        public IUserRepository Users { get; }
        public IQuestionRepository Questions { get; }
        public IAnswerRepository Answers { get; }
        public IQuestionVoteRepository QuestionVotes { get; }
        public IAnswerVoteRepository AnswerVotes { get; }
    }
}
