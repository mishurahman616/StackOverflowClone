using Autofac;
using StackOverflow.Web.Models;

namespace StackOverflow.Web
{
    public class WebModule: Module
    {
        public WebModule() {}

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<LoginModel>().AsSelf().InstancePerDependency();

            base.Load(builder);
        }
    }
}
