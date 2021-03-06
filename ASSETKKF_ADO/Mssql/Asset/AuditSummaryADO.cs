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

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> GetSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            int isdep = d.isdept ? 1 : 0;
            string cmd = " select sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT,sum(QTY_PROBLEM) as QTY_PROBLEM,sum(QTY_NOPROBLEM) as QTY_NOPROBLEM ,sum(QTY_TRN) as QTY_TRN ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            //cmd += "   ,(select COUNT(AssETNO) from FT_ASAUDITPOSTTRN_COMPANY(" + QuoteStr(d.Company) + ")";
            //cmd += "  where yr = " + d.year + " and mn = " + d.mn + " ) as QTY_TRN";
            //cmd += " from AuditSummary (" + d.year + "," + d.mn + ") as C where 1 = 1 and audit_no is not null";
            cmd += " from AuditSummary_company (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn) + "," + QuoteStr(d.yrmn) + "," + isdep + ") as C where 1 = 1 and audit_no is not null";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if ((!d.Menu3 && !d.Menu4))
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODE))
                {
                    cmd += " DEPCODEOL = '" + d.DEPCODE + "'";
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

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                cmd += " and YRMN = '" + d.yrmn + "'";
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                cmd += " and depmst = '" + d.depmst + "'";
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                cmd += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                cmd += " and OFFICECODE = '" + d.OFFICECODE + "'";
                //cmd += " and DEPCODEOL = (SELECT distinct DEPCODEEOL FROM [dbo].[FT_UserAsset] ( '" + d.OFFICECODE + "') where COMPANY = c.COMPANY )";
            }

            if (d.isdept)
            {
                cmd += " and isnull(Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(Audit_NO,'') like 'AU%' ";
            }

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param, conStr).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> GetDeptSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            int isdep = d.isdept ? 1 : 0;
            string cmd = " select company,depcodeol,max(stname) as stname,max(DEPMST) as DEPMST,max(DEPNM) as DEPNM,SQNO,audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            //cmd += "from AuditSummary (" + d.year + "," + d.mn + ") where 1 = 1  and audit_no is not null";
            cmd += "from from AuditSummary_company (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn) + "," + QuoteStr(d.yrmn) + "," + isdep + ") as C where 1 = 1 and audit_no is not null";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if ((!d.Menu3 && !d.Menu4))
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

            if (d.isdept)
            {
                cmd += " and isnull(Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(Audit_NO,'') like 'AU%' ";
            }

            cmd += " group by company,depcodeol,SQNO,audit_no";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param, conStr).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> GetDEPCODEOLSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            int isdep = d.isdept ? 1 : 0;

            string cmd = " select company,depcodeol,max(stname) as stname,SQNO,audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            //cmd += "from AuditSummary (" + d.year + "," + d.mn + ") where 1 = 1  and audit_no is not null";
            cmd += "from from AuditSummary_company (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn) + "," + QuoteStr(d.yrmn) + "," + isdep + ") as C where 1 = 1 and audit_no is not null";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if ((!d.Menu3 && !d.Menu4))
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

            if (d.isdept)
            {
                cmd += " and isnull(Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(Audit_NO,'') like 'AU%' ";
            }

            cmd += " group by company,depcodeol,SQNO,audit_no";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param, conStr).ToList();
            return res;
        }

        public List<AuditDeptSummary> GetDEPMSTSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            int isdep = d.isdept ? 1 : 0;

            string cmd = " select company,YRMN,DEPMST ,max(DEPNM) as DEPNM,max(SQNO) as SQNO,max(audit_no) as audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT,sum(QTY_TRN) as QTY_TRN ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            //cmd += "from AuditSummary  (" + d.year + "," + d.mn + ") where 1 = 1  and audit_no is not null";
            cmd += " from AuditSummary_company (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn) + "," + QuoteStr(d.yrmn) + "," + isdep + ") as C where 1 = 1 and audit_no is not null";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if ((!d.Menu3 && !d.Menu4))
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

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                cmd += " YRMN = '" + d.yrmn + "'";
            }

            if (d.isdept)
            {
                cmd += " and isnull(Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(Audit_NO,'') like 'AU%' ";
            }

            cmd += " group by company,YRMN,DEPMST";
            cmd += " order by company,YRMN,DEPMST";
            var res = Query<AuditDeptSummary>(cmd, param, conStr).ToList();
            return res;
        }

        public List<AuditDeptSummary> GetSummaryByDepMst(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            int isdep = d.isdept ? 1 : 0;

            string cmd = " select company,yrmn,max(DEPMST) as DEPMST,depcodeol,max(stname) as stname,SQNO,audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end progress ";
            cmd += ",max(yr) as yr,max(mn) as mn,max(yrmn) as yrmn";
            //cmd += " from AuditSummary (" + d.year + "," + d.mn + ") where 1 = 1  and audit_no is not null";
            cmd += " from AuditSummary_company (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn) + "," + QuoteStr(d.yrmn) + "," + isdep + ") as C where 1 = 1 and audit_no is not null";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if ((!d.Menu3 && !d.Menu4))
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

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                cmd += " and YRMN = '" + d.yrmn + "'";
            }

            if (d.isdept)
            {
                cmd += " and isnull(Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(Audit_NO,'') like 'AU%' ";
            }

            cmd += " group by company,depcodeol,SQNO,audit_no";
            cmd += " order by company,depcodeol,SQNO,audit_no";
            var res = Query<AuditDeptSummary>(cmd, param, conStr).ToList();
            return res;
        }

        public List<Multiselect> GetAuditYear(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT YR as id,YR as description FROM [dbo].FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.Company) + ") as M ";
            cmd += " where M.FLAG not in ('X','C')  and audit_no is not null";
            cmd += " and COMPANY in (" + QuoteStr(d.Company) + ") ";

            if (d.isdept)
            {
                cmd += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }

            //if (!String.IsNullOrEmpty(d.Company))
            //{
            //    var comp = "";
            //    comp = "'" + d.Company.Replace(",", "','") + "'";
            //    cmd += " and COMPANY in (" + comp + ") ";
            //}

            if (!String.IsNullOrEmpty(d.depmst))
            {
                cmd += " and depmst = '" + d.depmst + "'";
            }

                cmd += " group by YR ";
            cmd += " order by YR desc ";

            var res = Query<Multiselect>(cmd, param, conStr).ToList();
            return res;
        }

        public List<Multiselect> GetAuditMN(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT MN as id,MN  as description FROM [dbo].FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.Company) + ") as M ";
            cmd += " where M.FLAG not in ('X','C')  and audit_no is not null";
            cmd += " and COMPANY in (" + QuoteStr( d.Company) + ") ";

            if (d.isdept)
            {
                cmd += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }

            //if (!String.IsNullOrEmpty(d.Company))
            //{
            //    var comp = "";
            //    comp = "'" + d.Company.Replace(",", "','") + "'";
            //    cmd += " and COMPANY in (" + comp + ") ";
            //}

            if (!String.IsNullOrEmpty(d.depmst))
            {
                cmd += " and depmst = '" + d.depmst + "'";
            }

            if (!String.IsNullOrEmpty(d.year))
            {
                cmd += " and YR = '" + d.year + "'";
            }

            cmd += " group by MN ";
            cmd += " order by MN desc ";

            var res = Query<Multiselect>(cmd, param, conStr).ToList();
            return res;
        }



    }
}
