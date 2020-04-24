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
    public class MtWorkOrderSequenceLController : Base
    {
        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequenceLGetDataApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        /*
        [HttpPost("Search")]
        public async Task<dynamic> Search([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequenceLSearchApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("Save")]
        public async Task<dynamic> Save([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequenceLSaveApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }
        [HttpDelete("Delete")]
        public async Task<dynamic> Delete([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequenceLDeleteApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }*/
    }
}