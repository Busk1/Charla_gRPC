using System;
using System.Net.Http;
using System.Threading.Tasks;
using GrpcClient;
using Grpc.Net.Client;
using Grpc.Core;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            //var reply = await client.SayHelloAsync( new HelloRequest { Name = "GreeterClient" });
            //Console.WriteLine("Greeting: " + reply.Message);
            var streaming = client.SayHelloStreaming(new HelloRequest { Name = "GreeterClient" });
            await foreach (var item in streaming.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine("Greeting: " + item.Message);
                var prueba = "algo";
                Console.WriteLine($"algo {item.Message}");
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }
}
