using Autofac;
using StackOverflow.Web.Models;
using StackOverflow.Web.Models.AnswerModels;
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

            builder.RegisterType<UserModel>().AsSelf().InstancePerDependency();

            //Question 
            builder.RegisterType<QuestionCreateModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<QuestionUpdateModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<QuestionListModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<QuestionVoteModel>().AsSelf().InstancePerDependency();
            
            builder.RegisterType<QuestionDetailsModel>().AsSelf().InstancePerDependency();

            //Answer
            builder.RegisterType<AnswerCreateModel>().AsSelf().InstancePerDependency();

            builder.RegisterType<AnswerUpdateModel>().AsSelf().InstancePerDependency();
            
            builder.RegisterType<AnswerVoteModel>().AsSelf().InstancePerDependency();
   
            builder.RegisterType<AnswerListModel>().AsSelf().InstancePerDependency();

            base.Load(builder);
        }
    }
}
