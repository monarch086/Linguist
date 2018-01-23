using Linguist.DataLayer.Repositories;
using Linguist.DataLayer.Model;
using Linguist.Services.Interfaces;

namespace Linguist.Services.Implementation
{
    public class LogsService : ILogsService
    {
        private readonly IRepository<Visitor> _visitorsRepository;

        public LogsService(IRepository<Visitor> visitorsRepository)
        {
            _visitorsRepository = visitorsRepository;
        }

        public void AddVisitor(Visitor visitor)
        {
            _visitorsRepository.Add(visitor);
        }
    }
}
