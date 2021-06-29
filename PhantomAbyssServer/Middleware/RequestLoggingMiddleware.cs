using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace PhantomAbyssServer.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly RecyclableMemoryStreamManager recyclableMemoryStreamManager;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
            recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            request.EnableBuffering();
            StringBuilder builder = new();
            await using var requestStream = recyclableMemoryStreamManager.GetStream();
            await request.Body.CopyToAsync(requestStream);
            string body = await ReadStreamInChunks(requestStream);
            request.Body.Position = 0;

            builder.AppendLine($"Incoming request: {request.Method} {request.Path}{request.QueryString}");
            builder.AppendLine($"Body:\n{body}");

            logger.LogInformation(builder.ToString());
            
            await next(context);
        }

        private static async Task<string> ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = await reader.ReadBlockAsync(readChunk,
                    0,
                    readChunkBufferLength);
                await textWriter.WriteAsync(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}