using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using MemKeyValue.Business;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Myclass mc = new Myclass();
            mc.test();

            Console.ReadLine();
        }
    }

    class Myclass
    {
        public void test()
        {
            Dictionary<string, object> a = new Dictionary<string, object>();
            a.Add("p:one", "p1");
            a.Add("p:two", "p2");
            a.Add("q:one", "q1");
            a.Add("q:two", "q2");

            object o = a["p:one"];
            var k = a.Where(s => s.Key.Contains("p")).Select(d => d.Value);
            a.Remove("q:one");

            IStoreKeyValue store = StoreKeyValue.Instance;
            store.AddKeyValue("person", "id", "12003");
            store.AddKeyValue("person", "name", "prasanna");
            store.AddKeyValue("person", "email", "prasanna@email.com");
            store.AddKeyValue("city", "name", "dubai");
            store.AddKeyValue("country", "name", "uae");
            store.AddKeyValue("test", "k1", "value01");
            store.AddKeyValue("test", "k2", "value02");
            store.AddKeyValue("test", "k3", "value03");

            var name = store.GetKeyValue("city", "name");

            store.DeleteKeyValue("test", "k1");

            var h = store.GetKeyValues("test");


        }
    }
}
