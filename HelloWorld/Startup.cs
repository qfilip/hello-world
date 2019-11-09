using HelloWorld.Abstractions;
using HelloWorld.Cfg.ConfigurationBase;
using HelloWorld.Cqrs.BaseCqrs;
using HelloWorld.Cqrs.Queries;
using HelloWorld.Dal;
using HelloWorld.Models;
using LiteDB;
using System;
using System.Collections.Generic;

namespace HelloWorld
{
    public class Startup : IEntry
    {
        public void Run()
        {
            PrintHelloWorld();
        }

        private void PrintHelloWorld()
        {
            var collectionName = BaseConfigurator.GetAppCollectionName();
            var context = new DbContext(collectionName);
            var broker = new EventBroker<DbContext>(context);

            int length = BaseConfigurator.GetTargetStringLength();
            var asciiChars = new List<AsciiChar>();

            for (int i = 0; i < length; i++)
            {
                char character = BaseConfigurator.GetTargetString()[i];
                var dbQuery = Query.EQ("Character", character.ToString());

                var query = new GetCharacterQuery(new AsciiChar(), dbQuery);
                var target = broker.Query<GetCharacterQuery, AsciiChar>(query);
                asciiChars.Add(target);
            }

            foreach(var item in asciiChars)
            {
                Console.Write(item.Character);
            }
        }
    }
}
