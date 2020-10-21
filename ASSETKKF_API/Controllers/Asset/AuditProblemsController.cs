using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Report;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditProblemsController : Base
    {
        public AuditProblemsController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("GetSummary")]
        public async Task<dynamic> GetSummary([FromBody] dynamic data)
        {
            var res = new AuditProblemsAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetDeptSummary")]
        public async Task<dynamic> GetDeptSummary([FromBody] dynamic data)
        {
            var res = new AuditProblemsDeptAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
