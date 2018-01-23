using System;
using System.Collections.Generic;
using System.Linq;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.UnitOfWork;

namespace Linguist.DataLayer.Repositories.LogRepositories
{
    public class VisitorsRepository : IRepository<Visitor>
    {
        private readonly LinguistContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public VisitorsRepository(LinguistContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork.UnitOfWork(context);
        }

        public int Add(Visitor entity)
        {
            _context.Visitors.Add(entity);
            return _unitOfWork.Save();
        }

        public int Edit(Visitor entity)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<Visitor> GetAll()
        {
            return _context.Visitors.ToList();
        }

        public int Remove(int entityId)
        {
            throw new NotSupportedException();
        }
    }
}
