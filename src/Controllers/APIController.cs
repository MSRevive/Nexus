using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MSNexus.Controllers
{
    [Route("api")]
    public class APIController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<string, string> _list = new ConcurrentDictionary<string, string>();

        public APIController(ILogger<APIController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _config = configuration;
            if (_config.GetValue<bool>("Verify:EnforceMap"))
            {
                var text = System.IO.File.ReadAllText(_config["Verify:MapList"]);
                _list = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(text);
            }
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("mapcheck/{name}/{hash}")]
        public ActionResult<ResultType> GetMapVerify(string name, string hash)
        {
            if (_config.GetValue<bool>("Verify:EnforceMap"))
            {
                if (_list.ContainsKey(name) && _list[name] != hash)
                {
                    return new ResultType { Result = false };
                }
            }
            return new ResultType { Result = true };
        }
    }

    public class ResultType
    {
        public bool Result { get; set; }
    }
}
