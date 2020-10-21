using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.AUDITMANAGER;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Audit
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditManagerController : Base
    {
        

        public AuditManagerController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("AuditMGR")]
        public async Task<dynamic> GetAuditResult([FromBody] dynamic data)
        {
            var res = new AuditManagerApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("AuditMGRAppv")]
        public async Task<dynamic> GetAuditMGRAppv([FromBody] dynamic data)
        {
            var res = new AuditManagerAppvApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }


    }
}
