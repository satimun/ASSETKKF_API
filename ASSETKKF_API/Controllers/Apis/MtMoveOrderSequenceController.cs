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
    public class MtMoveOrderSequenceController : Base
    {

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new MtMoveOrderSequenceGetDataApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetNextWorkStationGrp")]
        public async Task<dynamic> GetNextWorkStationGrp([FromBody] dynamic data)
        {
            var res = new MtMoveOrderSequenceGetNextWorkStationGrpApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("InsertMove")]
        public async Task<dynamic> InsertMove([FromBody] dynamic data)
        {
            var res = new MtMoveOrderSequenceInsertMoveApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }


    }
}