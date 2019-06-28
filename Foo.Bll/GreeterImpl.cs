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
            Console.WriteLine($"Server: Calling {nameof(this.SayHello)} from {context?.Peer}, name:{request?.Name}");

            HelloReply reply = new HelloReply
            {
                Request = request,

                Name = request?.Name,
                Message = $"Hello {request?.Name}, 不错嘛，带了{request?.Gifts.Count}种礼物."
            };
            return Task.FromResult(reply);
        }
    }
}
