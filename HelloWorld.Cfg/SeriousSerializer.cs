using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HelloWorld.Cfg
{
    public class SeriousSerializer
    {
        public void BinarySerializer<TSource>(TSource obj, string path)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, obj);
            }
        }

        public TTarget BinaryDeserializer<TTarget>(string path)
        {
            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                TTarget obj = (TTarget)formatter.Deserialize(stream);
                return obj;
            }
        }
    }
}
