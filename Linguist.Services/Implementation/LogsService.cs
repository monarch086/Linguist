using System;
using System.Web;
using Linguist.DataLayer.Repositories;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;

namespace Linguist.Services.Implementation
{
    public class LogsService : ILogsService
    {
        private readonly IRepository<Visitor> _visitorsRepository;

        private readonly IAccountsService _accountsService;

        public LogsService(IRepository<Visitor> visitorsRepository, IAccountsService accountsService)
        {
            _visitorsRepository = visitorsRepository;
            _accountsService = accountsService;
        }

        public void AddVisitor(HttpContext context)
        {
            var request = context.Request;

            var login = _accountsService.GetUserName(context);

            Visitor visitor = new Visitor
            {
                Login = login,
                Ip = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                Url = request.RawUrl,
                Date = DateTime.UtcNow,
                Browser = request.Browser.Browser,
                IsMobileDevice = request.Browser.IsMobileDevice
            };

            _visitorsRepository.Add(visitor);
        }
    }
}
