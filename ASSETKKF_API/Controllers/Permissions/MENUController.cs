using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Permissions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Permissions
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class MENUController : Base
    {

        public MENUController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("Action")]
        public async Task<dynamic> Action([FromBody] dynamic data)
        {
            var res = new MENUApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }


    }
}
