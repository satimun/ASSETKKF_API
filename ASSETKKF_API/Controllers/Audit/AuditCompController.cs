using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.AUDITCOMP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Audit
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditCompController : Base
    {
        

        public AuditCompController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("AuditComp")]
        public async Task<dynamic> GetAuditResult([FromBody] dynamic data)
        {
            var res = new AuditCompApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("CompEditAudit")]
        public async Task<dynamic> GetAuditDuplicate([FromBody] dynamic data)
        {
            var res = new CompEditAuditApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
