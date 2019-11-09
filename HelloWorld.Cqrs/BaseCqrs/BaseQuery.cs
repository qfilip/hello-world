using HelloWorld.Dal;

namespace HelloWorld.Cqrs.BaseCqrs
{
    public abstract class BaseQuery<TResponse>
    {
        protected TResponse dto;
        public BaseQuery(TResponse dto)
        {
            this.dto = dto;
        }

        public abstract TResponse InvokeQuery<TContext>(TContext context) where TContext : DbContext;
    }
}
