using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Oauth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASSETKKF_API.Controllers.Asset
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class OauthController : Base
    {

        [HttpGet("Access")]
        public async Task<dynamic> Access()
        {
            var res = new OauthAccessTokenGetApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext)));
        }

        [HttpPost("Login")]
        public async Task<dynamic> Login([FromBody] dynamic data)
        {
            var res = new OauthLoginApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("Approve")]
        public async Task<dynamic> Approve([FromBody] dynamic data)
        {
            var res = new OauthApproveApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

        [HttpPost("Logout")]
        public async Task<dynamic> Logout([FromBody] dynamic data)
        {
            var res = new OauthLogoutApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));
        }

    }
}
