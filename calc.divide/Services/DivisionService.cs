using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace calc.divide
{
    public class DivisionService : Division.DivisionBase
    {
        private readonly ILogger<DivisionService> _logger;
        public DivisionService(ILogger<DivisionService> logger)
        {
            _logger = logger;
        }

        public override Task<DivisionResponse> Divide(DivisionRequest request, ServerCallContext context)
        {//purposely not checking division by 0
            return Task.FromResult(new DivisionResponse
            {
                Result = request.Op1 / request.Op2
            });
        }
    }
}
