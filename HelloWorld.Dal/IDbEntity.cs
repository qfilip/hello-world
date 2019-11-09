using LiteDB;

namespace HelloWorld.Dal
{
    public interface IDbEntity
    {
        [BsonId(true)]
        public int Id { get; set; }
    }
}
