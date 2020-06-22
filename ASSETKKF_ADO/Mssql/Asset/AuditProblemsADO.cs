﻿using System;
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
            string cmd = " select sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_NOPROBLEM) as QTY_NOPROBLEM,sum(QTY_PROBLEMS) as QTY_PROBLEMS,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_NOPROBLEM)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_NOPROBLEM   ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_PROBLEMS)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_PROBLEMS   ";
            cmd += ",Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_WAIT)  AS float) / CAST(sum(QTY_TOTAL)   AS float)) * 100) end as PROGRESS_WAIT  ";
            cmd += "from FT_AuditProblems () where 1 = 1";

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
            var res = Query<AuditProblems>(cmd, param).ToList();
            return res;
        }

        public List<AuditProblems> GetDeptSummary(AuditProblemsReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select company,depcodeol,max(stname) as stname,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_NOPROBLEM) as QTY_NOPROBLEM,sum(QTY_PROBLEMS) as QTY_PROBLEMS,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_NOPROBLEM)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_NOPROBLEM   ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_PROBLEMS)  AS float) / CAST(sum(QTY_TOTAL)  AS float)) * 100) end as PROGRESS_PROBLEMS   ";
            cmd += " ,Case when isnull(sum(QTY_TOTAL),0)  = 0  then 0 else ((CAST(sum(QTY_WAIT)  AS float) / CAST(sum(QTY_TOTAL)   AS float)) * 100) end as PROGRESS_WAIT  ";
            cmd += "from FT_AuditProblems () where 1 = 1";

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
            cmd += " group by company,depcodeol";

            var res = Query<AuditProblems>(cmd, param).ToList();
            return res;
        }

    }
}