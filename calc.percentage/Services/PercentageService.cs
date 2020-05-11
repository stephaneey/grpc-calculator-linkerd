using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using calc.divide;
using calc.multiply;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using static calc.divide.Division;
using static calc.multiply.Multiplication;

namespace calc.percentage
{
    public class PercentageService : Percentage.PercentageBase
    {       
        
        DivisionClient _DivisionClient;
        MultiplicationClient _MultiplicationClient;
        
        private readonly ILogger<PercentageService> _logger;
        public PercentageService(ILogger<PercentageService> logger, grpcClientConfig config)
        {
            _logger = logger;
            _MultiplicationClient = config.mcli;
            _DivisionClient = config.dcli;
        }

        public override async Task<PercentageResponse> GetPercentage(PercentageRequest request, ServerCallContext context)
        {
            
            var m =(await _MultiplicationClient.MultiplyAsync(
                        new MultiplicationRequest
                        {
                            Op1 = request.Op1,
                            Op2 = request.Op2
                        })).Result;

            return new PercentageResponse
            {
                Result = (await _DivisionClient.DivideAsync(new DivisionRequest
                {
                    Op1 = (await _MultiplicationClient.MultiplyAsync(
                        new MultiplicationRequest
                        {
                            Op1 = request.Op1,
                            Op2 = request.Op2
                        })).Result,
                    Op2 = 100
                })).Result
            };
        }
       
    }
}
