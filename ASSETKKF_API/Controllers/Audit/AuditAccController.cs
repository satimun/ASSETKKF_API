using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.AUDITACC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Audit
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditAccController : Base
    {
        

        public AuditAccController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("AuditAcc")]
        public async Task<dynamic> GetAuditResult([FromBody] dynamic data)
        {
            var res = new AuditAccApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("AuditACCSend")]
        public async Task<dynamic> GetAuditAcc([FromBody] dynamic data)
        {
            var res = new AuditAccSendApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
