using Autofac;
using StackOverflow.BL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            base.Load(builder);
        }
    }
}
