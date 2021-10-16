using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MSNexus.Middleware
{
    public class IpWhitelist
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private Dictionary<string, bool> _list = new Dictionary<string, bool>();

        public IpWhitelist(RequestDelegate next, ILoggerFactory loggerFactory, Dictionary<string, bool> list)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<IpWhitelist>();
            _list = list;
        }

        public async Task Invoke(HttpContext context)
        {
            var remoteIP = context.Connection.RemoteIpAddress;

            if (!_list.ContainsKey(remoteIP.ToString()))
            {
                _logger.LogInformation($"Forbidden request from {remoteIP}");
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            //passed
            await _next.Invoke(context);
        }
    }
}
