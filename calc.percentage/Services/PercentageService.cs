using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using static calc.divide.Division;
using static calc.multiply.Multiplication;

namespace calc.percentage
{
    public class PercentageService : Percentage.PercentageBase
    {
        string MultiplicationEndpoint = null;
        string DivisionEndpoint = null;

    private readonly ILogger<PercentageService> _logger;
        public PercentageService(ILogger<PercentageService> logger)
        {
            _logger = logger;
#if DEBUG
            MultiplicationEndpoint = "http://localhost:5003";
            DivisionEndpoint = "http://localhost:5002";

#else
       MultiplicationEndpoint=Environment.GetEnvironmentVariable("MultiplicationEndpoint");
       DivisionEndpoint=Environment.GetEnvironmentVariable("DivisionEndpoint");                
                
#endif
        }

        public override async Task<PercentageResponse> GetPercentage(PercentageRequest request, ServerCallContext context)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            return new PercentageResponse
            {
                Result = await Divide(await Multiply(request.Op1, request.Op2), 100)
            };
        }
        async Task<int> Divide(int op1, int op2)
        {
            using (var channel = GrpcChannel.ForAddress(DivisionEndpoint))
            {
                var client = new DivisionClient(channel);
                return (await client.DivideAsync(new calc.divide.DivisionRequest
                {
                    Op1 = op1,
                    Op2 = op2
                })).Result;
            }
        }

        async Task<int> Multiply(int op1, int op2)
        {
            using (var channel = GrpcChannel.ForAddress(MultiplicationEndpoint))
            {
                var client = new MultiplicationClient(channel);
                return (await client.MultiplyAsync(new calc.multiply.MultiplicationRequest
                {
                    Op1 = op1,
                    Op2 = op2
                })).Result;
            }
        }
    }
}
