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
    public class AuditProblemsController : Base
    {
        [HttpPost("GetSummary")]
        public async Task<dynamic> GetSummary([FromBody] dynamic data)
        {
            var res = new AuditProblemsAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetDeptSummary")]
        public async Task<dynamic> GetDeptSummary([FromBody] dynamic data)
        {
            var res = new AuditProblemsDeptAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
