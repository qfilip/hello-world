using HelloWorld.Dal;

namespace HelloWorld.Cqrs.BaseCqrs
{
    public abstract class BaseCommand<TResponse>
    {
        protected TResponse dto;
        public BaseCommand(TResponse dto)
        {
            this.dto = dto;
        }

        public abstract TResponse InvokeCommand<TContext>(TContext context) where TContext : DbContext;
    }
}