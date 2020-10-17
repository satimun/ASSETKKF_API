using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Request.Asset;
using Dapper;
using Newtonsoft.Json;

namespace ASSETKKF_ADO.Mssql.Report
{
    public class PivotDataAdo : Base
    {
        private static PivotDataAdo instant;
        public static PivotDataAdo GetInstant()
        {
            if (instant == null) instant = new PivotDataAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private PivotDataAdo()
        {
        }

        public DataTable getProblemByDep(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.Company);
            param.Add("@YEAR", d.year);

            var obs = QuerySP<dynamic>("SP_ProblemByDep",param);

            //IList<dynamic> data = obs.ToList();


            var dt = ToDataTable(obs);                        

            return dt;
        }

        public DataTable getProblemByDepcodeol(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.Company);
            param.Add("@YEAR", d.year);
            param.Add("@DEPMST", d.depmst);

            var obs = QuerySP<dynamic>("SP_ProblemByDepcodeol", param);

            //IList<dynamic> data = obs.ToList();


            var dt = ToDataTable(obs);

            return dt;
        }

    }
}
