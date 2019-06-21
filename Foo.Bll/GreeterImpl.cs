using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Foo.Contracts;
using Grpc.Core;

namespace Foo.Bll
{
    public class GreeterImpl : Greeter.GreeterBase
    {
        // Server side handler of the SayHello RPC
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
        }
    }
}
