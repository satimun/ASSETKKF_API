using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.AUDITCUT;
using ASSETKKF_API.Engine.Asset.File;
using ASSETKKF_API.Engine.Asset.Home;
using ASSETKKF_MODEL.Request.Asset;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AuditCutController : Base
    {
        private IConfiguration Configuration;

        public AuditCutController(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        [HttpPost("GetAuditNoLst")]
        public async Task<dynamic> GetAuditNoLst([FromBody] dynamic data)
        {
            var res = new AuditCutAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditInfo")]
        public async Task<dynamic> GetAuditInfo([FromBody] dynamic data)
        {
            var res = new AuditCutInfoAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditCutPost")]
        public async Task<dynamic> GetAuditCutPost([FromBody] dynamic data)
        {
            var res = new AuditCutPostAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("CheckAuditAssetNo")]
        public async Task<dynamic> CheckAuditAssetNo([FromBody] dynamic data)
        {
            var res = new AuditPostCheckAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        
        [HttpPost("AuditAssetPost")]
        public async Task<dynamic> AuditAssetPost([FromBody] dynamic data)
        {
            var res = new AuditPostMSTAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        
        [HttpPost("AuditAssetIMG")]
        public async Task<dynamic> AuditAssetIMG([FromBody] dynamic data)
        {
            var res = new AuditUploadApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
