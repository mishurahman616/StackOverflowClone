using StackOverflow.DAL.Entities.Base;
using StackOverflow.DAL.Enums;

namespace StackOverflow.DAL.Entities
{
    public class AnswerVote: IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual VoteType VoteType { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual User User { get; set; }
    }
}
