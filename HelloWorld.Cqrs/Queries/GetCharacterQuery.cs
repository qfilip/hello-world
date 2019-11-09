using HelloWorld.Cqrs.BaseCqrs;
using HelloWorld.Models;
using LiteDB;

namespace HelloWorld.Cqrs.Queries
{
    public class GetCharacterQuery : BaseQuery<AsciiChar>
    {
        private readonly Query query;
        public GetCharacterQuery(AsciiChar dto) : base(dto)
        {
        }

        public GetCharacterQuery(AsciiChar dto, Query dbQuery) : base(dto)
        {
            this.query = dbQuery;
        }

        public override AsciiChar InvokeQuery<TContext>(TContext context)
        {
            return context.Get<AsciiChar>(this.query);
        }
    }
}
