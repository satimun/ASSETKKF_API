using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_API.Engine.Asset.Track;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Controllers.Track
{
    [Produces("application/json")]
    [Route("v1/[controller]")]

    [ApiController]
    public class TrackOfflineController : Base
    {
        private IConfiguration Configuration;

        public TrackOfflineController(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        [HttpPost("TrackOffline")]
        public async Task<dynamic> TrackOffline([FromBody] dynamic data)
        {
            var res = new TrackOfflineApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
    }
}
