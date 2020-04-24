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
    public class MtWorkOrderSequence_EmpController : Base
    {

        [HttpPost("GetData")]
        public async Task<dynamic> GetData([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequence_EmpGetDataApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("GetDataView")]
        public async Task<dynamic> GetDataView([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequence_EmpGetDataViewApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }

        [HttpPost("CloseOrder")]
        public async Task<dynamic> CloseOrder([FromBody] dynamic data)
        {
            var res = new MtWorkOrderSequenceCloseOrderApi();
            return await Task.Run(() => ResponeValid(res.Execute(HttpContext, data)));

        }


    }
}