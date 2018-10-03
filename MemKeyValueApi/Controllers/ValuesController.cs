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
        /// <summary>
        /// test method to add sample entries to memory
        /// </summary>
        /// <returns></returns>
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Get key values by namespace
        /// </summary>
        /// <param name="_namespace"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get value by namespace and key combination
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
        // GET api/values/5
        public IHttpActionResult Get(string _namespace, string _key)
        {
            object result = null;
            try
            {
                IStoreKeyValue store = StoreKeyValue.Instance;
                result = store.GetKeyValue(_namespace, _key);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Save key value by namespace as group name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Delete key value by namespace
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="_key"></param>
        /// <returns></returns>
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
