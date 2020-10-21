using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AsFixedAssetController : Base
    {
        public AsFixedAssetController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new AsFixedAssetApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetTask")]
        public async Task<dynamic> GetTask([FromBody] dynamic data)
        {
            var res = new TaskAuditApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("CancelAudit")]
        public async Task<dynamic> CancelAudit([FromBody] dynamic data)
        {
            var res = new AuditCancelApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetTracking")]
        public async Task<dynamic> GetTracking([FromBody] dynamic data)
        {
            var res = new TaskAuditApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
