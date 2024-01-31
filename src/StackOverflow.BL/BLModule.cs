using Autofac;
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
            base.Load(builder);
        }
    }
}
