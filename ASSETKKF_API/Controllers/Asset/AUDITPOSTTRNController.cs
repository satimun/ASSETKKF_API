using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.AUDITCUT;
using ASSETKKF_API.Engine.Asset.File;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class AUDITPOSTTRNController : Base
    {

        public AUDITPOSTTRNController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("AuditAssetPostTRN")]
        public async Task<dynamic> AuditAssetPostTRN([FromBody] dynamic data)
        {
            var res = new AuditPostTRNAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }


        [HttpPost("AuditAssetIMG")]
        public async Task<dynamic> AuditAssetIMG([FromBody] dynamic data)
        {
            var res = new AuditPostTRNUploadAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditPostTRNInfo")]
        public async Task<dynamic> GetAuditPostTRNInfo([FromBody] dynamic data)
        {
            var res = new AuditPostTRNInfoAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetASSETOFFICECODE")]
        public async Task<dynamic> GetASSETOFFICECODE([FromBody] dynamic data)
        {
            var res = new ASSETOFFICECODEAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetASSETASSETNO")]
        public async Task<dynamic> GetASSETASSETNO([FromBody] dynamic data)
        {
            var res = new ASSETASSETNOAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("CheckASSETOFFICECODE")]
        public async Task<dynamic> CheckASSETOFFICECODE([FromBody] dynamic data)
        {
            var res = new ASSETOFFICECODECheckAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("CheckASSETASSETNO")]
        public async Task<dynamic> CheckASSETASSETNO([FromBody] dynamic data)
        {
            var res = new ASSETASSETNOCheckAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

    

    }
}
