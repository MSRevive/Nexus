using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MSNexus.Middleware
{
    public class ApiKey
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly string _apiKey;

        public ApiKey(RequestDelegate next, ILoggerFactory loggerFactory, string apiKey)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ApiKey>();
            _apiKey = apiKey;
        }

        public async Task Invoke(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress;

            if (!context.Request.Headers.TryGetValue(_apiKey, out var providedApiKey))
            {
                _logger.LogInformation($"Invalid API key from {ip}");
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            if (!_apiKey.Equals(providedApiKey))
            {
                _logger.LogInformation($"Invalid API key from {ip} using {_apiKey}");
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            //passed
            await _next.Invoke(context);
        }
    }
}
