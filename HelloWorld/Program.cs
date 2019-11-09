using HelloWorld.Cfg;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationBuilder
                .AppStarter<Startup>()
                .Configure<Configurator>()
                .Run();
        }
    } 
}
