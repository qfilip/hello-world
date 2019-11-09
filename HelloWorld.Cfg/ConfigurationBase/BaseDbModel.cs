using LiteDB;

namespace HelloWorld.Cfg.ConfigurationBase
{
    internal class BaseDbModel
    {
        [BsonId(true)]
        public int Id { get; set; }
        public char Character { get; set; }
    }
}
