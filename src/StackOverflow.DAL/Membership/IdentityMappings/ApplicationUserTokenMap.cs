using NHibernate;
using NHibernate.Mapping.ByCode.Conformist;
using StackOverflow.DAL.Membership.Entities;

namespace StackOverflow.DAL.Membership.IdentityMappings
{
    public class ApplicationUserTokenMap : ClassMapping<ApplicationUserToken>
    {
        public ApplicationUserTokenMap()
        {
            Schema("dbo");
            Table("ApplicationUserTokens");

            ComposedId(id =>
            {
                id.Property(x => x.UserId, map =>
                {
                    map.Column("UserId");
                    map.Type(NHibernateUtil.Guid);
                });

                id.Property(x => x.LoginProvider, map =>
                {
                    map.Column("LoginProvider");
                    map.Type(NHibernateUtil.String);
                    map.Length(32);
                });

                id.Property(x => x.Name, map =>
                {
                    map.Column("Name");
                    map.Type(NHibernateUtil.String);
                    map.Length(32);
                });
            });

            Property(x => x.Value, map =>
            {
                map.Column("Value");
                map.Type(NHibernateUtil.String);
                map.Length(256);
                map.NotNullable(true);
            });
        }
    }
}
