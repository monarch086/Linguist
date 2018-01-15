using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.UnitOfWork;

namespace Linguist.DataLayer.Repositories
{
    public class WordsRepository : IRepository<Word>
    {
        private readonly LinguistContext _context;

        private readonly IUnitOfWork _unitOfWork;

        public WordsRepository(LinguistContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork.UnitOfWork(context);
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
            return _context.Words.ToList();
        }

        public int Remove(int entityId)
        {
            var word = _context.Words.FirstOrDefault(w => w.WordId == entityId);
            if (word != null)
                _context.Words.Remove(word);
            return _unitOfWork.Save();
        }
    }
}
