using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace calc.substract
{
    public class SubstractionService : Substraction.SubstractionBase
    {
        private readonly ILogger<SubstractionService> _logger;
        public SubstractionService(ILogger<SubstractionService> logger)
        {
            _logger = logger;
        }

        public override Task<SubstractionResponse> Substract(SubstractionRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SubstractionResponse
            {
                Result = request.Op1-request.Op2
            });
        }
    }
}
