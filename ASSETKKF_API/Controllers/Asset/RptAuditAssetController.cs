﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Report;
using Microsoft.AspNetCore.Mvc;


namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class RptAuditAssetController : Base
    {
        [HttpPost("GetData")]
        public async Task<dynamic> GetSummary([FromBody] dynamic data)
        {
            var res = new RptAuditAssetAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
        
        [HttpPost("GetAuditCUTDT")]
        public async Task<dynamic> GetAuditCUTDT([FromBody] dynamic data)
        {
            var res = new AuditCUTDTApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetOfficeASSET")]
        public async Task<dynamic> GetOfficeASSET([FromBody] dynamic data)
        {
            var res = new GetOFFICEApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetTYPEASSET")]
        public async Task<dynamic> GetTYPEASSET([FromBody] dynamic data)
        {
            var res = new TYPEASSETApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetGROUPASSET")]
        public async Task<dynamic> GetGROUPASSET([FromBody] dynamic data)
        {
            var res = new GROUPASSETApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetMainData")]
        public async Task<dynamic> GetMainSummary([FromBody] dynamic data)
        {
            var res = new RptAuditAssetAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }


    }
}
