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
            
            while (true)
            {
                try
                {
                    Thread.Sleep(200);
                    var position = new Random().Next(0, 5);
                    
                    switch (position)
                    {
                        case 0:
                           Console.WriteLine("Attempting an addition");
                           Console.WriteLine(await Add(5, 3, endpoints[position]));
                            break;
                        case 1:
                            //purposely causing division by 0 from time to time
                            Console.WriteLine("Attempting a division");
                            Console.WriteLine(await Divide(5, new Random().Next(0, 4), endpoints[position]));
                            break;
                        case 2:
                            Console.WriteLine("Attempting a multiplication");
                            Console.WriteLine(await Multiply(5, 3, endpoints[position]));
                            break;
                        case 3:
                            Console.WriteLine("Attempting a substraction");
                            Console.WriteLine(await Substract(5, 3, endpoints[position]));
                            break;
                        case 4:
                            Console.WriteLine("Attempting a percentage");
                            Console.WriteLine(await Percentage(110, 20, endpoints[position]));
                            break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            

        }
        static async Task<int> Add(int op1,int op2,string endpoint)
        {
            using (var channel = GrpcChannel.ForAddress(endpoint))
            {
                var client = new AdditionClient(channel);
                return (await client.AddAsync(new calc.add.AdditionRequest
                {
                    Op1 = op1,
                    Op2 = op2
                })).Result;
            }
        }
        static async Task<int> Substract(int op1, int op2, string endpoint)
        {
            using (var channel = GrpcChannel.ForAddress(endpoint))
            {
                var client = new SubstractionClient(channel);
                return (await client.SubstractAsync(new calc.substract.SubstractionRequest
                {
                    Op1 = op1,
                    Op2 = op2
                })).Result;
            }
        }

        static async Task<int> Divide(int op1, int op2, string endpoint)
        {
            using (var channel = GrpcChannel.ForAddress(endpoint))
            {
                var client = new DivisionClient(channel);
                return (await client.DivideAsync(new calc.divide.DivisionRequest
                {
                    Op1 = op1,
                    Op2 = op2
                })).Result;
            }
        }

        static async Task<int> Multiply(int op1, int op2, string endpoint)
        {
            using (var channel = GrpcChannel.ForAddress(endpoint))
            {
                var client = new MultiplicationClient(channel);
                return (await client.MultiplyAsync(new calc.multiply.MultiplicationRequest
                {
                    Op1 = op1,
                    Op2 = op2
                })).Result;
            }
        }

        static async Task<int> Percentage(int op1, int op2, string endpoint)
        {
            using (var channel = GrpcChannel.ForAddress(endpoint))
            {
                var client = new PercentageClient(channel);
                return (await client.GetPercentageAsync(new calc.percentage.PercentageRequest
                {
                     Op1=op1,
                     Op2=op2
                })).Result;
            }
        }

    }
}
