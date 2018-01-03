using Linguist.DataLayer.Model;
using Linguist.DataLayer.Repositories;
using Ninject.Modules;

namespace Linguist.Services.DI
{
    public class ServServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<User>>().To<UsersRepository>();
            Bind<IRepository<Word>>().To<WordsRepository>();
            Bind<IRepository<Category>>().To<CategoriesRepository>();
            Bind<IRepository<CatWordRelation>>().To<CatWordRelationsRepository>();
        }
    }
}
