using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Operations;
using Ninject.Modules;

namespace ForKodisoft.Models
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            
            Bind<IContentOperations>().To<ContentOperations>();
            Bind<IUserOperations>().To<UserOperations>();
        }
    }
}