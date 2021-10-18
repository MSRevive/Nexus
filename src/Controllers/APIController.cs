using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MSNexus.Controllers
{
    [Route("api")]
    public class APIController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ConcurrentDictionary<string, string> _list = new ConcurrentDictionary<string, string>();

        public APIController(IConfiguration configuration)
        {
            _config = configuration;
            if (_config.GetValue<bool>("Verify:EnforceMap"))
            {
                var text = System.IO.File.ReadAllText(_config["Verify:MapList"]);
                _list = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(text);
            }
        }

        [HttpGet("map/{name}/{hash}")]
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
