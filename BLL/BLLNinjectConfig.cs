using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using DAL;
using BLL.FeedRead;

namespace BLL
{
    public class BLLNinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
            Bind<IFeedReader>().To<RSSRead>();
        }
    }
}
