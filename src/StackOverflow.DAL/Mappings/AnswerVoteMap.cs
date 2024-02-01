using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using StackOverflow.DAL.Entities;
using StackOverflow.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL.Mappings
{
    public class AnswerVoteMap: ClassMapping<AnswerVote>
    {
        public AnswerVoteMap()
        {
            Table("AnswerVote");

            Id(x => x.Id, map =>
            {
                map.Generator(Generators.GuidComb);
            });

            Property(x => x.VoteType, map =>
            {
                map.Type<EnumType<VoteType>>();
                map.Column("VoteType");
                map.NotNullable(true);
            });

            ManyToOne(x => x.User, map =>
            {
                map.Column("UserId");
            });

            ManyToOne(x => x.Answer, map =>
            {
                map.Column("AnswerId");
            });
        }
    }
}
