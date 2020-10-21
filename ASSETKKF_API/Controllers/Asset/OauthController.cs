using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Oauth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class OauthController : Base
    {
        public OauthController(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        [HttpGet("Access")]
        public async Task<dynamic> Access([FromQuery] string DBMode = null)
        {
            var res = new OauthAccessTokenGetApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, DBMode)));
        }

        [HttpPost("Login")]
        public async Task<dynamic> Login([FromBody] dynamic data)
        {
            var res = new OauthLoginApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("Approve")]
        public async Task<dynamic> Approve([FromBody] dynamic data)
        {
            var res = new OauthApproveApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("Logout")]
        public async Task<dynamic> Logout([FromBody] dynamic data)
        {
            var res = new OauthLogoutApi(Configuration);
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

    }
}
