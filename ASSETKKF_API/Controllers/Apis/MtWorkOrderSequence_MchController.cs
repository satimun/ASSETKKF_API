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
    public class MtWorkOrderSequence_MchController : Base
    {

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequence_MchGetDataApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetDataView")]
        public async Task<dynamic> GetDataView([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequence_MchGetDataViewApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }


    }
}