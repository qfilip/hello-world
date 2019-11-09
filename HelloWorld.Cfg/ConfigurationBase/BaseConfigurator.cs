using System;
using System.IO;

namespace HelloWorld.Cfg.ConfigurationBase
{
    public abstract class BaseConfigurator
    {
        protected static string configurationPath;
        protected readonly string databasePath;

        protected delegate void ConfigureChain();
        
        [Serializable]
        protected class ApplicationCfg
        {
            public string ConfigurationPath { get; set; }
            public string DatabasePath { get; set; }
            public string AppDbCollection { get; set; }
            public string TargetString { get; set; }
            public int TargetStringLength { get; set; }
        }

        public BaseConfigurator()
        {
            var directory = Directory.GetCurrentDirectory();
            string cfgFileName = "hw.conf";

            configurationPath = Path.Combine(directory, cfgFileName);
            databasePath = Path.Combine(directory, "ascii.db");

            this.cfgChain = new ConfigureChain(StartConfiguration);
            ConfigureApp();
        }

        protected ConfigureChain cfgChain;

        protected abstract void StartConfiguration();

        public void ConfigureApp()
        {
            while (this.cfgChain != null)
            {
                this.cfgChain.Invoke();
            }
        }

        protected ConfigureChain CfgChainUpdate(ConfigureChain newCommand = null)
        {
            this.cfgChain = null;
            this.cfgChain += newCommand;

            return this.cfgChain;
        }

        public static string GetDatabasePath()
        {
            var ss = new SeriousSerializer();
            var config = ss.BinaryDeserializer<ApplicationCfg>(configurationPath);

            return config.DatabasePath;
        }

        public static string GetAppCollectionName()
        {
            var ss = new SeriousSerializer();
            var config = ss.BinaryDeserializer<ApplicationCfg>(configurationPath);

            return config.AppDbCollection;
        }

        public static string GetTargetString()
        {
            var ss = new SeriousSerializer();
            var config = ss.BinaryDeserializer<ApplicationCfg>(configurationPath);

            return config.TargetString;
        }

        public static int GetTargetStringLength()
        {
            var ss = new SeriousSerializer();
            var config = ss.BinaryDeserializer<ApplicationCfg>(configurationPath);

            return config.TargetStringLength;
        }
    }

}
