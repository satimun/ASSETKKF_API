﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class STPOSITASSETController : Base
    {
        [HttpPost("GetDeptLst")]
        public async Task<dynamic> GetDeptLst([FromBody] dynamic data)
        {
            var res = new STPOSITASSETAPI();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
