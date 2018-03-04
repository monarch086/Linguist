using System;
using System.Collections.Generic;
using System.Linq;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;

namespace Linguist.DataLayer.Repositories.LogRepositories
{
    public class TestResultsRepository : IRepository<TestResult>
    {
        private readonly LinguistContext _context;

        public TestResultsRepository(LinguistContext context)
        {
            _context = context;
        }

        public int Add(TestResult entity)
        {
            _context.TestResults.Add(entity);
            return _context.Save();
        }

        public int Edit(TestResult entity)
        {
            throw new NotSupportedException();
        }

        public IEnumerable<TestResult> GetAll()
        {
            return _context.TestResults.ToList();
        }

        public int Remove(int entityId)
        {
            throw new NotSupportedException();
        }
    }
}
