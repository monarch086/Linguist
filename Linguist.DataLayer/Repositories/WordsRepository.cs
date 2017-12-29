using System.Collections.Generic;
using System.Data.Entity.Migrations;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.UnitOfWork;

namespace Linguist.DataLayer.Repositories
{
    public class WordsRepository : IRepository<Word>
    {
        private readonly LinguistContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public WordsRepository(LinguistContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public int Add(Word entity)
        {
            _context.Words.Add(entity);
            return _unitOfWork.Save();
        }

        public int Edit(Word entity)
        {
            _context.Words.AddOrUpdate(entity);
            return _unitOfWork.Save();
        }

        public IEnumerable<Word> GetAll()
        {
            return _context.Words;
        }

        public int Remove(Word entity)
        {
            _context.Words.Remove(entity);
            return _unitOfWork.Save();
        }
    }
}
