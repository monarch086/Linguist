using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Linguist.Services.Implementation;
using Linguist.Services.Interfaces;
using Ninject;

namespace Linguist.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            //Здесь будут размещены привязки
            _kernel.Bind<IUsersService>().To<UsersService>();
            _kernel.Bind<IAccountsService>().To<AccountsService>();
            _kernel.Bind<ICategoriesService>().To<CategoriesService>();
            _kernel.Bind<IWordsService>().To<WordsService>();
            _kernel.Bind<IResultsService>().To<ResultsService>();
            _kernel.Bind<ILogsService>().To<LogsService>();
            _kernel.Bind<IMailService>().To<MailService>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
    }
}