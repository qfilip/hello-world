using HelloWorld.Dal;

namespace HelloWorld.Cqrs.BaseCqrs
{
    public class EventBroker<TContext> where TContext : DbContext
    {
        private readonly TContext context;
        public EventBroker(TContext context)
        {
            this.context = context;
        }

        public TResponse Command<TCommand, TResponse>(TCommand command) where TCommand : BaseCommand<TResponse>
        {
            return command.InvokeCommand(this.context);
        }

        public TResponse Query<TQuery, TResponse>(TQuery query) where TQuery : BaseQuery<TResponse>
        {
            return query.InvokeQuery(this.context);
        }
    }
}
