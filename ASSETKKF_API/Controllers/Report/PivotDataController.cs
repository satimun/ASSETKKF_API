using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Report
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class PivotDataController : Base
    {

        public PivotDataController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("PivotData")]
        public async Task<dynamic> PivotData([FromBody] dynamic data)
        {
            var res = new PivotDataApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

    }
}
