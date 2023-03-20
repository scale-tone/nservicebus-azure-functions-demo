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
        public async Task<IActionResult> PlaceOrder (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "a/p/i/place-order")] HttpRequest req, 
            ExecutionContext ctx
        )
        {
            string orderId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new PlaceOrder { OrderId = orderId };

            await this._functionEndpoint.Send(command, ctx);

            return new OkObjectResult(new { orderId });
        }

        /// <summary>
        /// Sends a PlaceOrder command delayed by 10 seconds
        /// </summary>
        [FunctionName(nameof(PlaceDelayedOrder))]
        public async Task<IActionResult> PlaceDelayedOrder (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "a/p/i/place-delayed-order")] HttpRequest req, 
            ExecutionContext ctx
        )
        {
            var sendOptions = new SendOptions();

            sendOptions.DelayDeliveryWith(TimeSpan.FromSeconds(10));

            string orderId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new PlaceOrder { OrderId = orderId };

            await this._functionEndpoint.Send(command, sendOptions, ctx);

            return new OkObjectResult(new { orderId });
        }

        /// <summary>
        /// Sends a PlaceFatalOrder command
        /// </summary>
        [FunctionName(nameof(PlaceFatalOrder))]
        public async Task<IActionResult> PlaceFatalOrder (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "a/p/i/place-fatal-order")] HttpRequest req, 
            ExecutionContext ctx
        )
        {
            string orderId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new PlaceFatalOrder { OrderId = orderId };

            var sendOptions = new SendOptions();
            sendOptions.SetDestination("Sales");

            await this._functionEndpoint.Send(command, sendOptions, ctx);

            return new OkObjectResult(new { orderId });
        }

        /// <summary>
        /// Sends a TestTransactions command
        /// </summary>
        [FunctionName(nameof(TestTransactions))]
        public async Task<IActionResult> TestTransactions (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "a/p/i/test-transactions")] HttpRequest req, 
            ExecutionContext ctx
        )
        {
            string orderId = Guid.NewGuid().ToString().Substring(0, 8);

            var command = new TestTransactions { OrderId = orderId };

            var sendOptions = new SendOptions();
            sendOptions.SetDestination("Sales");

            await this._functionEndpoint.Send(command, sendOptions, ctx);

            return new OkObjectResult(new { orderId });
        }

        /// <summary>
        /// Sends a bunch of PlaceWholesaleOrder commands
        /// </summary>
        [FunctionName(nameof(PlaceWholesaleOrders))]
        public async Task<IActionResult> PlaceWholesaleOrders (
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "a/p/i/place-wholesale-orders")] HttpRequest req, 
            ExecutionContext ctx
        )
        {
            bool useSessions = req.Query["use-sessions"] == "true";

            string customerId = Guid.NewGuid().ToString().Substring(0, 8);

            var sendOptions = new SendOptions();
            sendOptions.SetDestination( useSessions ? "Wholesale" : "Sales");

            // Setting native message's SessionId field
            sendOptions.CustomizeNativeMessage(m => m.SessionId = customerId);

            for (int i = 0; i < 5; i++)
            {
                var command = new PlaceWholesaleOrder { CustomerId = customerId, Id = i };

                await this._functionEndpoint.Send(command, sendOptions, ctx);
            }

            return new OkObjectResult(new { customerId });
        }
    }
}
