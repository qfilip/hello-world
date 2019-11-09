using HelloWorld.Abstractions;
using HelloWorld.Cfg.ConfigurationBase;

namespace HelloWorld
{
    public sealed class ApplicationBuilder : IAppStart
    {
        private IEntry entry;
        private ApplicationBuilder(IEntry entry) => this.entry = entry;

        public static IAppStart AppStarter<TSource>() where TSource : IEntry, new()
        {
            var source = new TSource();
            return new ApplicationBuilder(source);
        }

        public IEntry Configure<TSource>() where TSource : BaseConfigurator, new()
        {
            var cfgSource = new TSource();
            cfgSource.ConfigureApp();

            return this.entry;
        }

        void IAppStart.Run()
        {
            this.entry.Run();
        }
    }
}
