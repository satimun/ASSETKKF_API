using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Dashboard;
using ASSETKKF_API.Engine.Asset.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditSummaryController : Base
    {
        [HttpPost("GetSummary")]
        public async Task<dynamic> GetSummary([FromBody] dynamic data)
        {
            var res = new AuditSummaryApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetDeptSummary")]
        public async Task<dynamic> GetDeptSummary([FromBody] dynamic data)
        {
            var res = new AuditDeptSummaryApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetDeptInfoSummary")]
        public async Task<dynamic> GetDeptInfoSummary([FromBody] dynamic data)
        {
            var res = new AuditDepInfoSummaryApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        
        [HttpPost("GetAuditYear")]
        public async Task<dynamic> GetAuditYear([FromBody] dynamic data)
        {
            var res = new GetAuditYearApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditMN")]
        public async Task<dynamic> GetAuditMN([FromBody] dynamic data)
        {
            var res = new GetAuditMNApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
