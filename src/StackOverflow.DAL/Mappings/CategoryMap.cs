using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Mappings
{
    public class CategoryMap: ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("Categories");

            Id(cat => cat.Id, map =>
            {
                map.Generator(Generators.GuidComb);
            });

            Property(cat => cat.Name, map =>
            {
                map.Length(20);
                map.Unique(true);
            });

            Property(cat => cat.Description, map =>
            {
                map.Length(1000);
            });

        }
    }
}
