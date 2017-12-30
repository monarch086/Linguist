using Linguist.DataLayer.UnitOfWork;
using Ninject.Modules;

namespace Linguist.DataLayer.DI
{
    public class DLServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>();
        }
    }
}
