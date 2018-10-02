using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MemKeyValue.Business
{
    public class StoreKeyValue : IStoreKeyValue
    {

        #region Singleton Pattern

        private StoreKeyValue() { }

        private static volatile StoreKeyValue _instance;
        private static object syncRoot = new object();

        public static StoreKeyValue Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new StoreKeyValue();
                    }
                }

                return _instance;
            }
        }

        #endregion

        Dictionary<string, object> _cache = new Dictionary<string, object>();

        public bool AddKeyValue(string _namespace, string _key, string _value)
        {
            try
            {
                string tempKey = string.Format("{0}:{1}", _namespace, _key);

                if (_cache.ContainsKey(tempKey))
                {
                    throw new Exception("combination of namespace and key name already exist in memory");
                }

                _cache.Add(tempKey, _value);
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteKeyValue(string _namespace, string _key)
        {
            try
            {
                string tempKey = string.Format("{0}:{1}", _namespace, _key);

                if (!_cache.ContainsKey(tempKey))
                {
                    throw new Exception("combination of namespace and key not found in memory, key cannot be deleted");
                }

                return _cache.Remove(tempKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetKeyValue(string _namespace, string _key)
        {
            try
            {
                string tempKey = string.Format("{0}:{1}", _namespace, _key);

                if (!_cache.ContainsKey(tempKey))
                {
                    throw new Exception("combination of namespace and key not found in memory");
                }

                return _cache[tempKey];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<object> GetKeyValues(string _namespace)
        {
            try
            {
                IEnumerable<object> _values = _cache.Where(s => s.Key.Contains(_namespace))
                                    .Select(key => key.Value);

                if (_values.ToList().Count < 1)
                {
                    throw new Exception("no key found for the namespace.");
                }

                return _values;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
