using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Line.Notify;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSETKKF_API.Controllers.Line
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class NotifyController : Base
    {
        [HttpPost("NotifyPushMessage")]
        public async Task<dynamic> NotifyPushMessage([FromBody] dynamic data)
        {
            var res = new PushMessage();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
