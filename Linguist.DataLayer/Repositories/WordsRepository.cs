using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Linguist.DataLayer.Model;
using Linguist.DataLayer.Context;

namespace Linguist.DataLayer.Repositories
{
    public class WordsRepository : IRepository<Word>
    {
        private readonly LinguistContext _context;

        public WordsRepository(LinguistContext context)
        {
            _context = context;
        }

        public int Add(Word entity)
        {
            _context.Words.Add(entity);
            return _context.Save();
        }

        public int Edit(Word entity)
        {
            _context.Words.AddOrUpdate(entity);
            return _context.Save();
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
            return _context.Save();
        }
    }
}
