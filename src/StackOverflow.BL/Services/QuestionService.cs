using StackOverflow.BL.DTOs;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;
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

        public async Task DeleteQuestion(Question question)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Questions.Delete(question);
            await _unitOfWork.Commit();
        }

        public async Task<IList<Question>> GetAllQuestions()
        {
            return await _unitOfWork.Questions.GetAll();
        }

        public async Task<Question> GetQuestionById(Guid qestionId)
        {
            return await _unitOfWork.Questions.GetById(qestionId);
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

        public async Task<VoteUpdateStatus> UpdateQuestionVote(Guid questionId, Guid userId, VoteType voteType)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            var question = await _unitOfWork.Questions.GetById(questionId);

            if (user == null || question == null)
            {
                return VoteUpdateStatus.NoChange;
            }
                

            var existingVote = await _unitOfWork.QuestionVotes.GetSingle(x => x.Question.Id == questionId && x.User.Id == userId);
            var isNewVote = existingVote == null;

            await _unitOfWork.BeginTransaction();

            if (isNewVote || existingVote.VoteType != voteType)
            {
                if (!isNewVote)
                    await _unitOfWork.QuestionVotes.Delete(existingVote);

                var newVote = new QuestionVote
                {
                    User = user,
                    Question = question,
                    VoteType = voteType
                };

                await _unitOfWork.QuestionVotes.Create(newVote);
                await _unitOfWork.Commit();
                return isNewVote? VoteUpdateStatus.NewVoteInserted : VoteUpdateStatus.VoteUpdated;
            }
            else if (existingVote.VoteType == voteType)
            {
                await _unitOfWork.QuestionVotes.Delete(existingVote);
                await _unitOfWork.Commit();
                return VoteUpdateStatus.VoteRemoved;
            }
            return VoteUpdateStatus.NoChange;
        }
    }
}
