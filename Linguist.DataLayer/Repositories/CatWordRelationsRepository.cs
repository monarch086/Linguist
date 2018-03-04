using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Linguist.DataLayer.Context;
using Linguist.DataLayer.Model;

namespace Linguist.DataLayer.Repositories
{
    public class CatWordRelationsRepository : IRepository<CatWordRelation>
    {
        private readonly LinguistContext _context;

        public CatWordRelationsRepository(LinguistContext context)
        {
            _context = context;
        }

        public int Add(CatWordRelation entity)
        {
            _context.CatWordRelations.Add(entity);
            return _context.Save();
        }

        public int Edit(CatWordRelation entity)
        {
            _context.CatWordRelations.AddOrUpdate(entity);
            return _context.Save();
        }

        public IEnumerable<CatWordRelation> GetAll()
        {
            return _context.CatWordRelations.ToList();
        }

        public int Remove(int entityId)
        {
            var relation = _context.CatWordRelations.FirstOrDefault(r => r.CatWordRelationId == entityId);
            if (relation != null)
                _context.CatWordRelations.Remove(relation);
            return _context.Save();
        }
    }
}
