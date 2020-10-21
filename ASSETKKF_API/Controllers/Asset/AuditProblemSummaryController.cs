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
    public class AuditProblemSummaryController : Base
    {
        public AuditProblemSummaryController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("GetProblemSummary")]
        public async Task<dynamic> GetProblemSummary([FromBody] dynamic data)
        {
            var res = new AuditProblemSummaryAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
