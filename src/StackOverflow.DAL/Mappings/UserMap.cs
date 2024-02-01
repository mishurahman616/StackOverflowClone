using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.EntitiesMappings
{
    public class UserMap : SubclassMapping<User>
    {
        public UserMap()
        {
            DiscriminatorValue(nameof(User));

            Bag(u => u.Questions, map =>
            {
                map.Key(key => key.Column("UserId"));
                map.Inverse(true);
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, rel => rel.OneToMany());

            Bag(u => u.Answers, map =>
            {
                map.Key(key => key.Column("UserId"));
                map.Inverse(true);
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, rel => rel.OneToMany());

            Bag(u => u.QuestionVotes, map =>
            {
                map.Key(key => key.Column("UserId"));
                map.Inverse(true);
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, rel => rel.OneToMany());

            Bag(u => u.AnswerVotes, map =>
            {
                map.Key(key => key.Column("UserId"));
                map.Inverse(true);
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, rel => rel.OneToMany());
        }
    }
}
