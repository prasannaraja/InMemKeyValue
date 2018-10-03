using System;
using System.Collections.Generic;
using System.Text;

namespace MemKeyValue.Business
{
    public interface IStoreKeyValue
    {
        bool AddKeyValue(string _namespace, string _key,object _value);
        object GetKeyValue(string _namespace, string _key);
        IEnumerable<object> GetKeyValues(string _namespace);
        bool DeleteKeyValue(string _namespace, string _key);
    }
}
