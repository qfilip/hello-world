using HelloWorld.Cfg.ConfigurationBase;

namespace HelloWorld.Abstractions
{
    public interface IAppStart
    {
        IEntry Configure<TSource>() where TSource : BaseConfigurator, new();
        void Run();
    }
}
