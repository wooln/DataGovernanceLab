using System;
using Foo.Bll;
using Foo.Contracts;
using Library.ServiceTool;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Foo.UnitTest
{
    [TestClass]
    public class BllTest
    {
        [TestMethod]
        public void GreeterTest()
        {
            GreeterImpl client = new GreeterImpl();

            Console.Write("Input your name: ");
            String user = Console.ReadLine();

            var reply = client.SayHello(new HelloRequest { Name = user }, null).Result;
            Console.WriteLine("Greeting: " + reply.Message);
        }
    }
}
