using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Report;
using Microsoft.AspNetCore.Mvc;


namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class RptAuditAssetController : Base
    {
        [HttpPost("GetData")]
        public async Task<dynamic> GetSummary([FromBody] dynamic data)
        {
            var res = new RptAuditAssetAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
