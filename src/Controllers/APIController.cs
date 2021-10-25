using System;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MSNexus.Controllers
{
    [Route("api")]
    public class APIController : ControllerBase
    {
        private IConfiguration _config;
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<string, string> _mapList = new ConcurrentDictionary<string, string>();
        private readonly ConcurrentDictionary<string, bool> _banList = new ConcurrentDictionary<string, bool>();

        public APIController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _config = configuration;
            _logger = loggerFactory.CreateLogger<APIController>();

            if (_config.GetValue<bool>("Verify:EnforceMap"))
            {
                string text = "{}";

                if (System.IO.File.Exists(_config["Verify:MapList"]))
                {
                    text = System.IO.File.ReadAllText(_config["Verify:MapList"]);
                    _mapList = JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>(text);
                }
                else
                {
                    _logger.LogError("Unable to find map list file.");
                    _mapList = null;
                }           
            }

            if (_config.GetValue<bool>("Verify:EnforceBans"))
            {
                string text;

                if (System.IO.File.Exists(_config["Verify:BanList"]))
                {
                    text = System.IO.File.ReadAllText(_config["Verify:BanList"]);
                    _banList = JsonConvert.DeserializeObject<ConcurrentDictionary<string, bool>>(text);
                }
                else
                {
                    _logger.LogError("Unable to find ban list file.");
                    _banList = null;
                }
            }
        }

        //result true = map is clear.
        [HttpGet("map/{name}/{hash}")]
        public ActionResult<ResultType> GetMapVerify(string name, string hash)
        {
            if (_mapList != null && _config.GetValue<bool>("Verify:EnforceMap"))
            {
                //map is on list and hash matches
                if (_mapList.ContainsKey(name) && _mapList[name] == hash)
                {
                    return new ResultType { Result = true };
                }

                //map or hash is bad
                return new ResultType { Result = false };
            }
            //if enforce map is false.
            return new ResultType { Result = true };
        }

        //result true == player is banned.
        [HttpGet("ban/{steamid}")]
        public ActionResult<ResultType> GetBanVerify(string steamid)
        {
            if (_banList != null && _config.GetValue<bool>("Verify:EnforceBan"))
            {
                //if player is banned
                if (_banList.ContainsKey(steamid))
                {
                    return new ResultType { Result = true };
                }

                //if player isn't banned
                return new ResultType { Result = false };
            }
            //if enforceban is false.
            return new ResultType { Result = false };
        }
    }

    public class ResultType
    {
        public bool Result { get; set; }
    }
}
