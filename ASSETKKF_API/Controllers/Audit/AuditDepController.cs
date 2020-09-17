using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.AUDITDEP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Audit
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditDepController : Base
    {
        private IConfiguration Configuration;

        public AuditDepController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("AuditDep")]
        public async Task<dynamic> GetAuditResult([FromBody] dynamic data)
        {
            var res = new AuditDepApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("DepEditAudit")]
        public async Task<dynamic> GetAuditDuplicate([FromBody] dynamic data)
        {
            var res = new DepEditAuditApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

    }
}
