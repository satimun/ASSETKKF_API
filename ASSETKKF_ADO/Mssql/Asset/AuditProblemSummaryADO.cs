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

        public static AuditProblemSummaryADO GetInstant(string conStr = null)
        {
            if (instant == null) instant = new AuditProblemSummaryADO(conStr);
            return instant;
        }

        private string conectStr { get; set; }

        private AuditProblemSummaryADO(string conStr = null)
        {
            conectStr = conStr;
        }

        public List<AuditProblemSummary> GetProblemSummary(AuditProblemSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@TYPECODE", d.TYPECODE);
            param.Add("@GASTCODE", d.GASTCODE);
            param.Add("@OFFICECODE", d.OFFICECODE);
            param.Add("@PCODE", d.PCODE);

            string cmd = " select pname,sum(QTY) as QTY ";
            cmd += "from FT_AuditProblemSummary (" + QuoteStr(d.TYPECODE) + "," + QuoteStr(d.GASTCODE) + "," + QuoteStr(d.OFFICECODE) + "," + QuoteStr(d.PCODE) + "," + QuoteStr(d.Company) + ") where 1 = 1";
            

            //if (!String.IsNullOrEmpty(d.Company))
            //{
            //    var comp = "";
            //    comp = "'" + d.Company.Replace(",", "','") + "'";
            //    cmd += " and COMPANY in (" + comp + ") ";
            //}

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                cmd += " and DEPMST =" + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                cmd += " and YR =" + QuoteStr(d.YEAR);
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                cmd += " and MN =" + QuoteStr(d.MN);
            }

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                //param.Add("@AUDITNO", d.audit_no);
                //sql += " and audit_no = @AUDITNO";

                param.Add("@AUDITNO", d.audit_no);
                param.Add("@auditno_lk", $"%{d.audit_no}%");
                cmd += " AND (audit_no LIKE @auditno_lk OR audit_no = " + QuoteStr(d.audit_no) + " )";
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                param.Add("@SQNO", d.sqno);
                cmd += " and sqno = " + QuoteStr(d.sqno);

            }

            if (!string.IsNullOrEmpty(d.sqno_copm))
            {
                cmd += " and COMPANY = " + QuoteStr(d.sqno_copm);
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                param.Add("@DEPCODEOL", d.DEPCODEOL);
                cmd += " and DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            }

            if ((!d.Menu3 && !d.Menu4))
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
            var res = Query<AuditProblemSummary>(cmd, param, conectStr).ToList();
            return res;
        }
    }
}
