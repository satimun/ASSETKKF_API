using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;

namespace ASSETKKF_MODEL.Response.Home
{
    public class TaskAuditRes
    {
        public List<TaskAudit> TaskAuditLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    
}
