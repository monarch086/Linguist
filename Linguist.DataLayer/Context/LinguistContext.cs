using System.Data.Entity;
using Linguist.DataLayer.Model;

namespace Linguist.DataLayer.Context
{
    public class LinguistContext : DbContext
    {
        LinguistContext() : base("LinguistConnectionString") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Word> Words { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}
