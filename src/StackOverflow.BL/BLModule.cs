using Autofac;
using StackOverflow.BL.Services;

namespace StackOverflow.BL
{
    public class BLModule: Module
    {
        public BLModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionService>().As<IQuestionService>().InstancePerDependency();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();

            builder.RegisterType<AnswerService>().As<IAnswerService>().InstancePerDependency();

            builder.RegisterType<SeederService>().As<ISeederService>().InstancePerDependency();

            base.Load(builder);
        }
    }
}
