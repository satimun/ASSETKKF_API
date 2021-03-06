﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Line.Notify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Line
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class NotifyController : Base
    {
        public NotifyController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("NotifyPushMessage")]
        public async Task<dynamic> NotifyPushMessage([FromBody] dynamic data)
        {
            var res = new PushMessage(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
