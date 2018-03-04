using System;
using System.Collections.Generic;
using System.Linq;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;

namespace Linguist.DataLayer.Repositories.LogRepositories
{
    public class TrainingResultsRepository : IRepository<TrainingResult>
    {
        private readonly LinguistContext _context;

        public TrainingResultsRepository(LinguistContext context)
        {
            _context = context;
        }

        public int Add(TrainingResult entity)
        {
            _context.TrainingResults.Add(entity);
            return _context.Save();
        }

        public int Edit(TrainingResult entity)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<TrainingResult> GetAll()
        {
            return _context.TrainingResults.ToList();
        }

        public int Remove(int entityId)
        {
            throw new NotSupportedException();
        }
    }
}
