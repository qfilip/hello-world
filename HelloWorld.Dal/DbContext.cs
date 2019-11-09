using HelloWorld.Cfg.ConfigurationBase;
using LiteDB;

namespace HelloWorld.Dal
{
    public class DbContext
    {
        private readonly string collection;
        private readonly string dbPath;

        public DbContext(string collection)
        {
            this.collection = collection;
            this.dbPath = BaseConfigurator.GetDatabasePath();
        }

        public TEntity Get<TEntity>(int id) where TEntity : IDbEntity
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var collection = db.GetCollection<TEntity>(this.collection);
                var response = collection.FindById(id);
                return response;
            }
        }

        public TEntity Get<TEntity>(Query dbQuery)
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var collection = db.GetCollection<TEntity>(this.collection);
                var response = collection.FindOne(dbQuery);
                return response;
            }
        }

        public void Insert<TEntity>(TEntity entity) where TEntity : IDbEntity
        {
            using (var db = new LiteDatabase(dbPath))
            {
                var collection = db.GetCollection<TEntity>(this.collection);
                collection.Insert(entity);
            }
        }
    }
}
