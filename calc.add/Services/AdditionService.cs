using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace calc.add
{
    public class AdditionService : Addition.AdditionBase
    {
        private readonly ILogger<AdditionService> _logger;
        public AdditionService(ILogger<AdditionService> logger)
        {
            _logger = logger;
        }

        public override Task<AdditionResponse> Add(AdditionRequest request, ServerCallContext context)
        {
            return Task.FromResult(new AdditionResponse
            {
                Result = request.Op1+request.Op2
            });
        }
    }
}
