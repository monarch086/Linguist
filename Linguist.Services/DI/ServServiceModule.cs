using Linguist.DataLayer.Model;
using Linguist.DataLayer.Repositories;
using Linguist.DataLayer.Repositories.LogRepositories;
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

            Bind<IRepository<TestResult>>().To<TestResultsRepository>();
            Bind<IRepository<TrainingResult>>().To<TrainingResultsRepository>();
            Bind<IRepository<Visitor>>().To<VisitorsRepository>();
        }
    }
}
