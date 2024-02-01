using StackOverflow.DAL.Entities;
using StackOverflow.DAL.UnitOfWorks;

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
    }
}
