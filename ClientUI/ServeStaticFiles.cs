using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ClientUI
{
    public static class ServeStaticFiles
    {
        // Serves static files for client UI
        [FunctionName(nameof(ServeStaticFiles))]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "{p1?}/{p2?}/{p3?}")] HttpRequest req,
            string p1,
            string p2,
            string p3,
            ExecutionContext context
        )
        {
            string wwwroot = Path.Join(context.FunctionAppDirectory, "wwwroot");

            // Returning index.html by default
            string fullLocalPath = Path.Join(wwwroot, "index.html");
            string contentTypeValue = "text/html; charset=UTF-8";

            // Two bugs away. Making sure none of these segments ever contain any path separators and/or relative paths
            string relativePath = Path.Join(Path.GetFileName(p1), Path.GetFileName(p2), Path.GetFileName(p3));

            var contentType = FileMap.FirstOrDefault((kv => relativePath.StartsWith(kv[0])));

            if (contentType != null)
            {
                fullLocalPath = Path.Join(wwwroot, relativePath);

                if (!File.Exists(fullLocalPath))
                {
                    return new NotFoundResult();
                }

                contentTypeValue = contentType[1];
            }

            return new FileStreamResult(File.OpenRead(fullLocalPath), contentTypeValue);
        }

        private static readonly string[][] FileMap = new string[][]
        {
            new [] {Path.Join("static", "css"), "text/css; charset=utf-8"},
            new [] {Path.Join("static", "js"), "application/javascript; charset=UTF-8"},
            new [] {"manifest.json", "application/json; charset=UTF-8"},
            new [] {"favicon.svg", "image/svg+xml; charset=UTF-8"},
            new [] {"logo.svg", "image/svg+xml; charset=UTF-8"},
            new [] {"logo192.png", "image/png"},
            new [] {"logo512.png", "image/png"},
        };
    }
}
