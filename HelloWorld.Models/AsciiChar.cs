using HelloWorld.Dal;

namespace HelloWorld.Models
{
    public class AsciiChar : IDbEntity
    {
        public int Id { get; set; }
        public char Character { get; set; }
    }
}
