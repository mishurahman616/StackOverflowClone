using NHibernate;
using StackOverflow.DAL.Repositories.Interfaces;

namespace StackOverflow.DAL.UnitOfWorks
{
    public class ApplicationUnitOfWork : UnitOfWorkBase, IApplicationUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public IQuestionRepository Questions { get; private set; }
        public IAnswerRepository Answers { get; private set; }
        public IQuestionVoteRepository QuestionVotes { get; private set; }
        public IAnswerVoteRepository AnswerVotes { get; private set; }

        private ISession _session;

        public ApplicationUnitOfWork(
            ISession session,
            IUserRepository userRepository,
            IQuestionRepository questionRepository,
            IAnswerRepository answerRepository, IQuestionVoteRepository questionVoteRepository,
            IAnswerVoteRepository answerVoteRepository): base(session)
        {
            _session = session;
            Users = userRepository;
            Questions = questionRepository;
            Answers = answerRepository;
            QuestionVotes = questionVoteRepository;
            AnswerVotes = answerVoteRepository;
        }
}
}
