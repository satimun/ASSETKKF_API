using ASSETKKF_MODEL.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Controllers
{
    public abstract class Base : ControllerBase
    {
        public dynamic ResponeValid(ResponseAPI res)
        {
            if (res.status == "F")
            {
                return StatusCode(404, new { code = res.code, message = res.message });
            }
            return res.data;
        }
    }
}
