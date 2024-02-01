
using StackOverflow.DAL.Entities.Base;
using StackOverflow.DAL.Enums;

namespace StackOverflow.DAL.Entities
{
    public class QuestionVote: IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual VoteType VoteType { get; set; }
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }          
    }
}
