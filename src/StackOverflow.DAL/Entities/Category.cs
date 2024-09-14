using StackOverflow.DAL.Entities.Base;

namespace StackOverflow.DAL.Entities
{
    public class Category
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
    }
}
