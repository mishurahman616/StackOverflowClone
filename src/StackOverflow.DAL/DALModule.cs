using Autofac;
using StackOverflow.DAL.Repositories;
using StackOverflow.DAL.Repositories.Interfaces;
using StackOverflow.DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflow.DAL
{
    public class DALModule: Module
    {
        public DALModule() { }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRepository>().As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AnswerRepository>().As<IAnswerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QuestionVoteRepository>().As<IQuestionVoteRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AnswerVoteRepository>().As<IAnswerVoteRepository>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
