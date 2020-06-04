using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response.Report;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class AuditProblemSummaryADO : Base
    {
        private static AuditProblemSummaryADO instant;

        public static AuditProblemSummaryADO GetInstant()
        {
            if (instant == null) instant = new AuditProblemSummaryADO();
            return instant;
        }

        private string conectStr { get; set; }

        private AuditProblemSummaryADO()
        {
        }

        public List<AuditProblemSummary> GetProblemSummary(AuditProblemSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select pname,sum(QTY) as QTY ";
            cmd += "from FT_AuditProblemSummary () where 1 = 1";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    cmd += " DEPCODEOL = '" + d.DeptCode + "'";
                }
                if (d.DeptLST != null && d.DeptLST.Length > 0)
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
            }
            cmd += " group by pname";
            var res = Query<AuditProblemSummary>(cmd, param).ToList();
            return res;
        }
    }
}
