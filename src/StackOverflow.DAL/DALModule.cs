using Autofac;
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
            base.Load(builder);
        }
    }
}
