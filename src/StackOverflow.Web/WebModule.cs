using Autofac;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.QuestionModels;

namespace StackOverflow.Web
{
    public class WebModule: Module
    {
        public WebModule() {}

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<LoginModel>().AsSelf().InstancePerDependency();

            //Question 
            builder.RegisterType<QuestionCreateModel>().AsSelf().InstancePerDependency();

            base.Load(builder);
        }
    }
}
