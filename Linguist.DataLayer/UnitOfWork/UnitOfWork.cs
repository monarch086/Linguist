using Linguist.DataLayer.Context;

namespace Linguist.DataLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LinguistContext _context;

        public UnitOfWork(LinguistContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
