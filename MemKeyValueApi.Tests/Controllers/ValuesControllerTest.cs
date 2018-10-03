using MemKeyValue.Business;
using MemKeyValueApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace MemKeyValueApi.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        /// <summary>
        /// Test Not Applicable
        /// </summary>
        [TestMethod]
        public void Get()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            IEnumerable<string> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        /// <summary>
        /// Test Get Method 
        /// parameter : _namespace
        /// value: test
        /// </summary>
        [TestMethod]
        public void GetById()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            KeyValueModel model = new KeyValueModel()
            {
                _namespace = "test",
                key = "t1",
                value = "001"
            };


            // Act
            controller.Put(model);
            IHttpActionResult actionResult = controller.Get("test");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<object>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("001", contentResult.Content.Select(x => x).First());
        }

        /// <summary>
        /// Test Not Applicable
        /// </summary>
        [TestMethod]
        public void Post()
        {
            // Arrange
            ValuesController controller = new ValuesController();

            // Act
            controller.Post("value");

            // Assert
        }

        /// <summary>
        /// Save method
        /// parameter: KeyValueModel {_namespace,key,value}
        /// </summary>
        [TestMethod]
        public void Put()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            KeyValueModel model = new KeyValueModel()
            {
                _namespace = "test002",
                key = "t2",
                value = "002"
            };

            // Act
            controller.Put(model);
            IHttpActionResult actionResult = controller.Get("test002");
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<object>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual("002", contentResult.Content.Select(x => x).First());
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ValuesController controller = new ValuesController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            KeyValueModel model = new KeyValueModel()
            {
                _namespace = "test003",
                key = "t3",
                value = "003"
            };

            // Act
            controller.Put(model); //add key
            controller.Delete(model._namespace, model.key); //delete key

            IHttpActionResult actionResult = controller.Get("test003","t3"); //lookup key by namespace
            var contentResult = actionResult as ExceptionResult;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Exception);
            Assert.AreEqual("combination of namespace and key not found in memory", contentResult.Exception.Message);
        }
    }
}
