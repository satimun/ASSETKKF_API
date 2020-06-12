using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.File
{
    public class AuditUploadRes
    {
        public string fileName { get; set; }
        public string path { get; set; }
        public string fullpath { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
