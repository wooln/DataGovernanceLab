using System;
using Foo.Bll;
using Foo.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foo.Test
{
    [TestClass]
    public class BllTest
    {
        [TestMethod]
        public void GreeterTest()
        {
            GreeterImpl client = new GreeterImpl();

            string user = "徐云金";
            var reply = client.SayHello(new HelloRequest { Name = user }, null).Result;

            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
