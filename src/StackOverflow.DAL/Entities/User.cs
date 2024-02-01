
using Microsoft.AspNetCore.Identity;
using StackOverflow.DAL.Entities.Base;
using StackOverflow.DAL.Membership.Entities;

namespace StackOverflow.DAL.Entities
{
    public class User: ApplicationUser, IEntity<Guid>
    {
        public virtual IList<Question>? Questions { get; set; }
        public virtual IList<Answer>? Answers { get; set; }
        public virtual IList<QuestionVote>? QuestionVotes { get; set; }
        public virtual IList<AnswerVote>? AnswerVotes { get; set; }

    }
}
