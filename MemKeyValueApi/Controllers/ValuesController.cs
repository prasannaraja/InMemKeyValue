using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MemKeyValue.Business;

namespace MemKeyValueApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    public class ValuesController : ApiController
    {

        // GET api/values
        public IEnumerable<string> Get()
        {
            IStoreKeyValue store = StoreKeyValue.Instance;

            store.AddKeyValue("person", "id", "12003");
            store.AddKeyValue("person", "name", "prasanna");
            store.AddKeyValue("person", "email", "prasanna@email.com");
            store.AddKeyValue("city", "name", "dubai");
            store.AddKeyValue("country", "name", "uae");
            store.AddKeyValue("test", "k1", "value01");
            store.AddKeyValue("test", "k2", "value02");
            store.AddKeyValue("test", "k3", "value03");

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IHttpActionResult Get(string _namespace)
        {
            IEnumerable<object> result = null;
            try
            {
                IStoreKeyValue store = StoreKeyValue.Instance;
                result = store.GetKeyValues(_namespace);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            if (result == null || result.ToList().Count < 1)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET api/values/5
        public IHttpActionResult Get(string _namespace, string _key)
        {
            object result = null;
            try
            {
                IStoreKeyValue store = StoreKeyValue.Instance;
                result = store.GetKeyValue(_namespace, _key);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT api/values/5
        public IHttpActionResult Put([FromBody]KeyValueModel value)
        {
            bool result = false;

            try
            {
                IStoreKeyValue store = StoreKeyValue.Instance;

                KeyValueModel copyOfKvp = value.Clone() as KeyValueModel;

                result = store.AddKeyValue(copyOfKvp._namespace, copyOfKvp.key, copyOfKvp.value);

            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }

        // DELETE api/values/5
        public IHttpActionResult Delete(string _namespace, string _key)
        {
            bool result = false;

            try
            {
                IStoreKeyValue store = StoreKeyValue.Instance;
                result = store.DeleteKeyValue(_namespace, _key);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return Ok();
        }

        // POST api/values
        public IHttpActionResult Post([FromBody]string value)
        {
            return Ok();
        }
    }
}
