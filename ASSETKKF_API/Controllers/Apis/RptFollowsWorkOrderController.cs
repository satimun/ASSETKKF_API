using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Apis.Oauth;
using ASSETKKF_API.Engine.Apis.Report;
using ASSETKKF_MODEL.Data.Mssql.Mcis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ASSETKKF_API.Controllers.Apis
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class RptFollowsWorkOrderController : Base
    {
        [HttpPost("Report")]
        public async Task<dynamic> Report([FromBody]  dynamic data)
        {
            var res = new RptFollowsWorkOrderApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        } 

        [HttpPost("ReportHead")]
        public async Task<dynamic> ReportHead([FromBody]  dynamic data)
        {
            var res = new RptFollowsWorkOrderHeadApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("ReportDetail")]
        public async Task<dynamic> ReportDetail([FromBody]  dynamic data)
        {            
            var res = new RptFollowsWorkOrderDetailApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("ReportLast")]
        public async Task<dynamic> ReportLast([FromBody]  dynamic data)
        {            
            var res = new RptFollowsWorkOrderLastApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

    }
}