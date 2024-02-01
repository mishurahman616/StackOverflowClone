using StackOverflow.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL.Entities
{
    public class AnswerVote
    {
        public virtual Guid Id { get; set; }
        public virtual VoteType VoteType { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual User User { get; set; }
    }
}
