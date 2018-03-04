using System.Data.Entity;
using Linguist.DataLayer.Model;

namespace Linguist.DataLayer.Context
{
    public class LinguistContext : DbContext
    {
        public LinguistContext() : base("LinguistConnectionString") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Word> Words { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CatWordRelation> CatWordRelations { get; set; }

        public DbSet<Visitor> Visitors { get; set; }

        public DbSet<TestResult> TestResults { get; set; }

        public DbSet<TrainingResult> TrainingResults { get; set; }

        public int Save()
        {
            return SaveChanges();
        }
    }
}
