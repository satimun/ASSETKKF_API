using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class ConfigSettingController : Base
    {
        private IConfiguration Configuration;

        public ConfigSettingController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new ConfigSettings(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}