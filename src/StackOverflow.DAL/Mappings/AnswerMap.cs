using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL.Mappings
{
    public class AnswerMap: ClassMapping<Answer>
    {
        public AnswerMap()
        {
            Table("Answers");

            Id(x => x.Id, map =>
            {
                map.Generator(Generators.GuidComb);
            });

            Property(x => x.Body, map =>
            {
                map.Length(1000);
            });

            ManyToOne(x => x.User, map =>
            {
                map.Cascade(Cascade.Persist);
                map.Column("UserId");
            });

            ManyToOne(x => x.Question, map =>
            {
                map.Cascade(Cascade.Persist);
                map.Column("QuestionId");
            });

            Bag(x => x.Votes, map =>
            {
                map.Key(x => x.Column("AnswerId"));
                map.Cascade(Cascade.DeleteOrphans);
            }, rel=>rel.OneToMany());
        }
    }
}
