// See https://aka.ms/new-console-template for more information
using Grpc.Net.Client;
using GrpcServiceTutorial;

Console.WriteLine("Hello, World!");
var input = new HelloRequest { Name = "Angela" };
var channel = GrpcChannel.ForAddress("https://localhost:5125");

var client = new Greeter.GreeterClient(channel);

var reply = await client.SayHelloAsync(input);

Console.WriteLine(reply.Message);
Console.ReadLine();
