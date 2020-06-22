﻿using ASSETKKF_MODEL.Response.Asset;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class AuditSummaryADO : Base
    {
        private static AuditSummaryADO instant;

        public static AuditSummaryADO GetInstant()
        {
            if (instant == null) instant = new AuditSummaryADO();
            return instant;
        }

        private string conectStr { get; set; }

        private AuditSummaryADO()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> GetSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            cmd += "from AuditSummary () where 1 = 1";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    cmd += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
            }
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> GetDeptSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select company,depcodeol,max(stname) as stname,max(DEPMST) as DEPMST,max(DEPNM) as DEPNM,SQNO,audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            cmd += "from AuditSummary () where 1 = 1";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    cmd += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";

            }
            cmd += " group by company,depcodeol,SQNO,audit_no";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> GetDEPCODEOLSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select company,depcodeol,max(stname) as stname,SQNO,audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            cmd += "from AuditSummary () where 1 = 1";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    cmd += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
                
            }
            cmd += " group by company,depcodeol,SQNO,audit_no";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param).ToList();
            return res;
        }

        public List<AuditDeptSummary> GetDEPMSTSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select company,DEPMST ,max(DEPNM) as DEPNM,max(SQNO) as SQNO,max(audit_no) as audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            cmd += "from AuditSummary () where 1 = 1";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    cmd += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";

            }
            cmd += " group by company,DEPMST";
            cmd += " order by company,DEPMST";
            var res = Query<AuditDeptSummary>(cmd, param).ToList();
            return res;
        }

        public List<AuditDeptSummary> GetSummaryByDepMst(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select company,depcodeol,max(stname) as stname,SQNO,audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            cmd += "from AuditSummary () where 1 = 1";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    cmd += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";

            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                cmd += " and depmst = '" + d.depmst + "'";
            }

            cmd += " group by company,depcodeol,SQNO,audit_no";
            cmd += " order by company,depcodeol,SQNO,audit_no";
            var res = Query<AuditDeptSummary>(cmd, param).ToList();
            return res;
        }


    }
}