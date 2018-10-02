using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MemKeyValue.Business;

namespace MemKeyValueApi.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("api/values/{_namespace}")]
        public IHttpActionResult Get(string _namespace)
        {
            IEnumerable<object> result = null;
            try
            {
                IStoreKeyValue store = StoreKeyValue.Instance;
                result = store.GetKeyValues(_namespace);
            }
            catch(Exception)
            {
                return InternalServerError();
            }
            if (result == null || result.ToList().Count < 1)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }
    }
}
