using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Apis.Mcis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSETKKF_API.Controllers.Apis
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    [ApiController]
    public class MsCauseAppvController : Base
    {

        [HttpPost("ListActive")]
        public async Task<dynamic> ListActive([FromBody] dynamic data)
        {
            var res = new MsCauseAppvListActiveApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        
    }
}