using StackOverflow.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL.Entities
{
    public class Question: IEntity<Guid>
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }
    }
}
