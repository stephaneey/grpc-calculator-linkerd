using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace calc.multiply
{
    public class MultiplicationService : Multiplication.MultiplicationBase
    {
        private readonly ILogger<MultiplicationService> _logger;
        public MultiplicationService(ILogger<MultiplicationService> logger)
        {
            _logger = logger;
        }

        public override Task<MultiplicationResponse> Multiply(MultiplicationRequest request, ServerCallContext context)
        {
            return Task.FromResult(new MultiplicationResponse
            {
                Result = request.Op1*request.Op2
            });
        }
    }
}
