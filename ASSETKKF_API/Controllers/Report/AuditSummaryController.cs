using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Report
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditSummaryController : Base
    {
        public AuditSummaryController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("AuditSummary")]
        public async Task<dynamic> AuditSummary([FromBody] dynamic data)
        {
            var res = new AuditSummaryApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
