using StackOverflow.DAL.Entities.Base;

namespace StackOverflow.DAL.Entities
{
    public class Question: IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; } = string.Empty;
        public virtual string Body { get; set; } = string.Empty;
        public virtual IList<Answer>? Answers { get; set; } = new List<Answer>();
        public virtual IList<QuestionVote>? Votes { get; set; } = new List<QuestionVote>();
        public virtual User User { get; set; }
    }
}
