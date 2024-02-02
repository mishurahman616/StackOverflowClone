﻿using StackOverflow.BL.DTOs;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;
using StackOverflow.DAL.UnitOfWorks;

namespace StackOverflow.BL.Services
{
    public class AnswerService: IAnswerService
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public AnswerService(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAnswer(Answer answer)
        {
            await _unitOfWork.BeginTransaction();
            await _unitOfWork.Answers.Create(answer);
            await _unitOfWork.Commit();
        }

        public async Task<VoteUpdateStatus> UpdateAnswerVote(Guid answerId, Guid userId, VoteType voteType)
        {
            var user = await _unitOfWork.Users.GetById(userId);
            var answer = await _unitOfWork.Answers.GetById(answerId);

            if (user == null || answer == null)
            {
                return VoteUpdateStatus.NoChange;
            }

            var existingVote = await _unitOfWork.AnswerVotes.GetSingle(x => x.Answer.Id == answerId && x.User.Id == userId);
            var isNewVote = existingVote == null;

            await _unitOfWork.BeginTransaction();

            if (isNewVote || existingVote.VoteType != voteType)
            {
                if (!isNewVote)
                    await _unitOfWork.AnswerVotes.Delete(existingVote);

                var newVote = new AnswerVote
                {
                    User = user,
                    Answer = answer,
                    VoteType = voteType
                };

                await _unitOfWork.AnswerVotes.Create(newVote);
                await _unitOfWork.Commit();
                return isNewVote ? VoteUpdateStatus.NewVoteInserted : VoteUpdateStatus.VoteUpdated;
            }
            else if (existingVote.VoteType == voteType)
            {
                await _unitOfWork.AnswerVotes.Delete(existingVote);
                await _unitOfWork.Commit();
                return VoteUpdateStatus.VoteRemoved;
            }
            return VoteUpdateStatus.NoChange;
        }
    }
}