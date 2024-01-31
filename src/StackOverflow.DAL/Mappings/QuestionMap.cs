using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.Mappings
{
    public class QuestionMap: ClassMapping<Question>
    {
        public QuestionMap() 
        {
            Table("Questions");
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.GuidComb);
            });

            Property(x => x.Title, map =>
            {
                map.Length(100);
            });

            Property(x => x.Body, map =>
            {
                map.Length(1000);
            });
        }
    }
}
