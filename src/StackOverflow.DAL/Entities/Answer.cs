
using StackOverflow.DAL.Entities.Base;

namespace StackOverflow.DAL.Entities
{
    public class Answer : IEntity<Guid>
    {
        public virtual Guid Id { get; set ; }
        public virtual string Body { get; set ; }
        public virtual Question Question { get; set ; }
        public virtual User User { get; set ; }
        public virtual IList<AnswerVote>? Votes { get; set ; }

    }
}
