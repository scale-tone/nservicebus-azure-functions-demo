using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace ClientUI.API
{
    public static class HttpTest
    {
        [FunctionName("HttpTest")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            return new OkObjectResult(new { someField = "some value" });
        }
    }
}
