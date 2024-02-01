using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.DAL.Entities;

namespace StackOverflow.DAL.EntitiesMappings
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("ApplicationUsers");

            Id(x => x.Id, map => 
            {
                map.Generator(Generators.GuidComb);
            });

            Property(x => x.UserName);
            Property(x => x.NormalizedUserName);
            Property(x => x.Email);
            Property(x => x.NormalizedEmail);
            Property(x => x.EmailConfirmed);
            Property(x => x.PasswordHash);
            Property(x => x.SecurityStamp);
            Property(x => x.ConcurrencyStamp);
            Property(x => x.PhoneNumber);
            Property(x => x.PhoneNumberConfirmed);
            Property(x => x.TwoFactorEnabled);
            Property(x => x.LockoutEnd);

            //Property(x => x.LockoutEnd, map =>
            //{
            //    map.Column("LockoutEnd");
            //    map.Type<NHibernate.Type.DateTimeOffsetType>();
            //});

            Property(x => x.LockoutEnabled);
            Property(x => x.AccessFailedCount);

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
