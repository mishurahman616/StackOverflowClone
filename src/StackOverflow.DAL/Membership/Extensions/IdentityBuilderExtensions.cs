using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using StackOverflow.DAL.Membership.Store;


namespace StackOverflow.DAL.Membership.Extensions
{
    public static class IdentityBuilderExtensions
    {
        public static IdentityBuilder AddHibernateStores(this IdentityBuilder builder)
        {
            var userType = builder.UserType;
            var roleType = builder.RoleType;
   
            if (roleType != null)
            {
                var userStoreServiceType = typeof(IUserStore<>).MakeGenericType(userType);
                var userStoreImplementationType = typeof(CustomUserStore<,>).MakeGenericType(userType, roleType);
                builder.Services.AddScoped(userStoreServiceType, userStoreImplementationType);

                var roleStoreServiceType = typeof(IRoleStore<>).MakeGenericType(roleType);
                var roleStoreImplementationType = typeof(CustomRoleStore<>).MakeGenericType(roleType);
                builder.Services.AddScoped(roleStoreServiceType, roleStoreImplementationType);
            }
            else
            {
                var userStoreServiceType = typeof(IUserStore<>).MakeGenericType(userType);
                var userStoreImplementationType = typeof(UserOnlyStore<>).MakeGenericType(userType);
                builder.Services.AddScoped(userStoreServiceType, userStoreImplementationType);
            }

            return builder;
        }
    }
}
