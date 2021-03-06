﻿using System;
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

        public AuditCutController(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        [HttpPost("GetAuditNoLst")]
        public async Task<dynamic> GetAuditNoLst([FromBody] dynamic data)
        {
            var res = new AuditCutAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditDepLst")]
        public async Task<dynamic> GetAuditDepLst([FromBody] dynamic data)
        {
            var res = new AuditCutDepAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditDepcodeol")]
        public async Task<dynamic> GetAuditDepcodeol([FromBody] dynamic data)
        {
            var res = new AuditDepcodeolAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditInfo")]
        public async Task<dynamic> GetAuditInfo([FromBody] dynamic data)
        {
            var res = new AuditCutInfoAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditCutPost")]
        public async Task<dynamic> GetAuditCutPost([FromBody] dynamic data)
        {
            var res = new AuditCutPostAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("CheckAuditAssetNo")]
        public async Task<dynamic> CheckAuditAssetNo([FromBody] dynamic data)
        {
            var res = new AuditPostCheckAPI(Configuration);
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
            var res = new AuditUploadApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetAuditAsset")]
        public async Task<dynamic> GetAuditAsset([FromBody] dynamic data)
        {
            var res = new AuditAddsetAPI(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("UpdatePostMST")]
        public async Task<dynamic> UpdatePostMST([FromBody] dynamic data)
        {
            var res = new UpdatePostMSTApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetCentralOfficer")]
        public async Task<dynamic> GetCentralOfficer([FromBody] dynamic data)
        {
            var res = new CentralOfficerApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

    }
}
