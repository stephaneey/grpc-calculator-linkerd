using Grpc.Net.Client;
using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using static calc.add.Addition;
using static calc.divide.Division;
using static calc.multiply.Multiplication;
using static calc.percentage.Percentage;
using static calc.substract.Substraction;

namespace MathFanboy
{
    class Program
    {
        static string[] endpoints = null;
        
        static async Task Main(string[] args)
        {

#if DEBUG
            endpoints = new string[5] {
                "http://localhost:5001", 
                "http://localhost:5002", 
                "http://localhost:5003", 
                "http://localhost:5004",
                "http://localhost:5005"};
#else
            endpoints = new string[5] {
                Environment.GetEnvironmentVariable("AdditionEndpoint"),
                Environment.GetEnvironmentVariable("DivisionEndpoint"),
                Environment.GetEnvironmentVariable("MultiplicationEndpoint"),
                Environment.GetEnvironmentVariable("SubstractionEndpoint"),
                Environment.GetEnvironmentVariable("PercentageEndpoint")};
#endif
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            Console.WriteLine("available endpoints");
            foreach(string endpoint in endpoints)
            {
                Console.WriteLine(endpoint);
            }
            Console.WriteLine("Starting gRPC chatting");
            
            using var AdditionChannel = GrpcChannel.ForAddress(endpoints[0]);
            using var DivisionChannel = GrpcChannel.ForAddress(endpoints[1]);
            using var MultiplicationChannel = GrpcChannel.ForAddress(endpoints[2]);
            using var SubstractionChannel = GrpcChannel.ForAddress(endpoints[3]);
            using var PercentageChannel = GrpcChannel.ForAddress(endpoints[4]);

            var AdditionClient = new AdditionClient(AdditionChannel);
            var DivisionClient = new DivisionClient(DivisionChannel);
            var MultiplicationClient = new MultiplicationClient(MultiplicationChannel);
            var SubstractionClient = new SubstractionClient(SubstractionChannel);
            var PercentageClient = new PercentageClient(PercentageChannel);

            while (true)
            {
                try
                {
                    Thread.Sleep(20);                    
                    var position = new Random().Next(0, 5);

                    switch (position)
                    {
                        case 0:
                            Console.WriteLine("Attempting an addition");
                            Console.WriteLine((await AdditionClient.AddAsync(new calc.add.AdditionRequest
                            {
                                Op1 = 10,
                                Op2 = 12
                            })).Result);
                            break;
                        case 1:
                            //purposely causing division by 0 from time to time
                            Console.WriteLine("Attempting a division");
                            Console.WriteLine((await DivisionClient.DivideAsync(new calc.divide.DivisionRequest
                            {
                                Op1 = 10,
                                Op2 = new Random().Next(0, 4)
                            })).Result);
                            break;
                        case 2:
                            Console.WriteLine((await MultiplicationClient.MultiplyAsync(new calc.multiply.MultiplicationRequest
                            {
                                Op1 = 10,
                                Op2 = 12
                            })).Result);
                            break;
                        case 3:
                            Console.WriteLine((await SubstractionClient.SubstractAsync(new calc.substract.SubstractionRequest
                            {
                                Op1 = 10,
                                Op2 = 12
                            })).Result);
                            break;
                        case 4:
                            Console.WriteLine("Attempting a percentage");
                            Console.WriteLine((await PercentageClient.GetPercentageAsync(new calc.percentage.PercentageRequest
                            {
                                Op1 = 10,
                                Op2 = 12
                            })).Result);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }            

        }       

    }
}
