using StackOverflow.DAL.Entities.Base;

namespace StackOverflow.DAL.Entities
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
