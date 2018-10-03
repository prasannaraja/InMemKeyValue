using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MemKeyValue.Business
{
    [Serializable]
    public class KeyValueModel: ICloneable
    {
        public string _namespace { get; set; }
        public string key { get; set; }
        public object value { get; set; }

        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }
    }
}
