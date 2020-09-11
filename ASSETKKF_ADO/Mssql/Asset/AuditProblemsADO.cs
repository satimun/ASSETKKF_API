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
    public class AuditProblemsADO : Base
    {
        private static AuditProblemsADO instant;

        public static AuditProblemsADO GetInstant()
        {
            if (instant == null) instant = new AuditProblemsADO();
            return instant;
        }

        private string conectStr { get; set; }

        private AuditProblemsADO()
        {
        }

        public List<AuditProblems> GetSummary(AuditProblemsReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@TYPECODE", d.TYPECODE);
            param.Add("@GASTCODE", d.GASTCODE);
            param.Add("@OFFICECODE", d.OFFICECODE);
            param.Add("@PCODE", d.PCODE);

            string cmd = " select sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_NOPROBLEM) as QTY_NOPROBLEM,sum(QTY_PROBLEMS) as QTY_PROBLEMS,sum(QTY_WAIT) as QTY_WAIT,sum(QTY_TRN) as QTY_TRN ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_NOPROBLEM)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_NOPROBLEM   ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_PROBLEMS)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_PROBLEMS   ";
            cmd += ",Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_WAIT)  AS float) / CAST(sum(QTY_TOTAL)   AS float)) * 100) end as PROGRESS_WAIT  ";
            cmd += "from FT_AuditProblems ("+ QuoteStr(d.TYPECODE) + "," + QuoteStr(d.GASTCODE) + "," + QuoteStr(d.OFFICECODE) + "," + QuoteStr(d.PCODE) + ") where 1 = 1";

            

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

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

            if (d.cutdt != null)
            {
                param.Add("@CUTDT", d.cutdt);
                cmd += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (d.inpdt != null)
            {
                param.Add("@INPDT", d.inpdt);
                cmd += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.inpdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.inpdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                //param.Add("@AUDITNO", d.audit_no);
                //cmd += " and audit_no = @AUDITNO";

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
            var res = Query<AuditProblems>(cmd, param).ToList();
            return res;
        }

        public List<AuditProblems> GetDeptSummary(AuditProblemsReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@TYPECODE", d.TYPECODE);
            param.Add("@GASTCODE", d.GASTCODE);
            param.Add("@OFFICECODE", d.OFFICECODE);
            param.Add("@PCODE", d.PCODE);

            string cmd = " select company,depcodeol,max(stname) as stname,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_NOPROBLEM) as QTY_NOPROBLEM,sum(QTY_PROBLEMS) as QTY_PROBLEMS,sum(QTY_WAIT) as QTY_WAIT,sum(QTY_TRN) as QTY_TRN ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_NOPROBLEM)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_NOPROBLEM   ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_PROBLEMS)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_PROBLEMS   ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_WAIT)  AS float) / CAST(sum(QTY_TOTAL)   AS float)) * 100) end as PROGRESS_WAIT  ";
            cmd += "from FT_AuditProblems (" + QuoteStr(d.TYPECODE) + "," + QuoteStr(d.GASTCODE) + "," + QuoteStr(d.OFFICECODE) + "," + QuoteStr(d.PCODE) + ") where 1 = 1";

           

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

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

            /*if (d.cutdt != null)
            {
                param.Add("@CUTDT", d.cutdt);
                cmd += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (d.inpdt != null)
            {
                param.Add("@INPDT", d.inpdt);
                cmd += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.inpdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.inpdt) + "))";
            }*/

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                //param.Add("@AUDITNO", d.audit_no);
                //cmd += " and audit_no = @AUDITNO";

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
            cmd += " group by company,depcodeol";

            var res = Query<AuditProblems>(cmd, param).ToList();
            return res;
        }

    }
}
