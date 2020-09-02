using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.AUDITPOST;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Audit
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditDuplicateController : Base
    {
        private IConfiguration Configuration;

        public AuditDuplicateController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("AuditDuplicate")]
        public async Task<dynamic> Action([FromBody] dynamic data)
        {
            var res = new AuditDuplicateApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
