using HelloWorld.Cfg.ConfigurationBase;
using LiteDB;
using System.Collections.Generic;
using System.IO;

namespace HelloWorld.Cfg
{
    public class Configurator : BaseConfigurator
    {
        public Configurator() : base() { }

        protected override void StartConfiguration()
        {
            ProceedWith(CheckCfgFileExists);
        }

        protected void CheckCfgFileExists()
        {
            bool proceed = File.Exists(configurationPath);
            ProceedWith(proceed, CheckDbExists, CreateCfgFile);
        }

        protected void CreateCfgFile()
        {
            var cfg = new ApplicationCfg
            {
                ConfigurationPath = configurationPath
            };

            var serializer = new SeriousSerializer();
            serializer.BinarySerializer(cfg, configurationPath);

            ProceedWith(CheckDbExists);
        }

        protected void CheckDbExists()
        {
            bool proceed = File.Exists(databasePath);
            ProceedWith(proceed, null, CreateDb);
        }

        protected void CreateDb()
        {
            string appDbCollection = "asciichars";

            var ss = new SeriousSerializer();
            var appCfg = ss.BinaryDeserializer<ApplicationCfg>(configurationPath);

            appCfg.DatabasePath = databasePath;
            appCfg.AppDbCollection = appDbCollection;

            ss.BinarySerializer(appCfg, configurationPath);

            var entities = new List<BaseDbModel>();
            for (int i = 0; i < 127; i++)
            {
                var entity = new BaseDbModel
                {
                    Character = (char)i
                };
                entities.Add(entity);
            }
            using (var db = new LiteDatabase(databasePath))
            {
                var collection = db.GetCollection<BaseDbModel>("asciichars");
                collection.InsertBulk(entities);
            }

            ProceedWith(DefineTargetString);
        }

        protected void DefineTargetString()
        {
            var ss = new SeriousSerializer();
            var appCfg = ss.BinaryDeserializer<ApplicationCfg>(configurationPath);
            
            appCfg.TargetString = "Hello World!";
            appCfg.TargetStringLength =  appCfg.TargetString.Length;
            ss.BinarySerializer(appCfg, configurationPath);
            
            ProceedWith(null);
        }

        protected void ProceedWith(ConfigureChain nextMethod)
        {
            base.cfgChain = base.CfgChainUpdate(nextMethod);
        }
        protected void ProceedWith(bool state, ConfigureChain ifTrue, ConfigureChain ifFalse)
        {
            switch (state)
            {
                case true:
                    base.CfgChainUpdate(ifTrue);
                    break;
                case false:
                    base.CfgChainUpdate(ifFalse);
                    break;
            }
        }
    }
}
