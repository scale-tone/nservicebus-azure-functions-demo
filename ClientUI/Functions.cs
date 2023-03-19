using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using NServiceBus;
using System;
using Messages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClientUI
{
    public class Functions
    {
        readonly IFunctionEndpoint _functionEndpoint;

        public Functions(IFunctionEndpoint functionEndpoint)
        {
            this._functionEndpoint = functionEndpoint;
        }

        /// <summary>
        /// Sends a PlaceOrder command
        /// </summary>
        [FunctionName(nameof(PlaceOrder))]
        public async Task<IActionResult> Run (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = $"api/place-order")] HttpRequest req, 
            ExecutionContext ctx
        )
        {
            string orderId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new PlaceOrder { OrderId = orderId };

            await this._functionEndpoint.Send(command, ctx);

            return new OkObjectResult(new { orderId });
        }
    }
}
