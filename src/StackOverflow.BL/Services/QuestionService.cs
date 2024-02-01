using StackOverflow.DAL.Entities;
using StackOverflow.DAL.UnitOfWorks;
using System.Linq.Expressions;

namespace StackOverflow.BL.Services
{
    public class QuestionService: IQuestionService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        public QuestionService(IApplicationUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateQuestion(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }
            else
            {
                await _unitOfWork.BeginTransaction();
                await _unitOfWork.Questions.Create(question);
                await _unitOfWork.Commit();
            }
        }

        public Task DeleteQuestion(Question question)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Question>> GetAllQuestions()
        {
            return _unitOfWork.Questions.GetAll();
        }

        public Task<Question> GetQuestionById(Guid qestionId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Question>> GetQuestions(Expression<Func<Question, bool>> filters)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Question>> GetQuestionsByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuestion(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
