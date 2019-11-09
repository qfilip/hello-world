using LiteDB;

namespace HelloWorld.Abstractions
{
    public interface IDbEntity
    {
        [BsonId(true)]
        public int Id { get; set; }
    }
}
