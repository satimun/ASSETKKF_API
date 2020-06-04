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
            cmd += ", Case when sum(QTY_TOTAL) is null or sum(QTY_TOTAL) = 0  then 0 else ((sum(QTY_CHECKED) / sum(QTY_TOTAL)) * 100) end as PROGRESS ";
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
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> GetDeptSummary(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " select company,depcodeol,stname,SQNO,audit_no,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT ";
            cmd += ", Case when sum(QTY_TOTAL) is null or sum(QTY_TOTAL) = 0  then 0 else ((sum(QTY_CHECKED) / sum(QTY_TOTAL)) * 100) end as progress ";
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
            cmd += " group by company,depcodeol,stname,SQNO,audit_no";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary> Search(ASSETKKF_MODEL.Request.Asset.AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            ////param.Add("@DEPTCODEEOL", d.DeptCode);
            var comp = "";
            if (!String.IsNullOrEmpty(d.Company))
            {
                //var arrComp = d.Company.Trim().Split(",");
                //for (int i = 0; i < arrComp.Length; i++)
                //{
                //    comp += " '" + arrComp[i] + "' ";
                //    if (i < arrComp.Length - 1)
                //    {
                //        comp += ",";
                //    }
                //}
                
                comp = "'" + d.Company.Replace(",", "','") + "'";
            }

            string cmd = "select max(QTY_TOTAL) as QTY_TOTAL,max(QTY_CHECKED) as QTY_CHECKED,max(QTY_WAIT) as QTY_WAIT";
            cmd += ",Case when max(QTY_TOTAL) is null or max(QTY_TOTAL) = 0  then 0 else ";
            cmd += "((max(QTY_CHECKED) / max(QTY_TOTAL)) * 100) end as PROGRESS_PERCENT";
            cmd += " from(";
            cmd += " select  count(D.ASSETNO) as QTY_TOTAL,null as QTY_CHECKED,null as QTY_WAIT from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M where D.SQNO = M.SQNO and D.FLAG not in ('X','C') ";
            if (!String.IsNullOrEmpty(d.Company))
            {
                cmd += " and M.COMPANY in (" + comp + ") ";
                //param.Add("@COMPANY1", comp);
                //cmd += " and (M.COMPANY in (@COMPANY1)) ";

                //cmd += " and (";
                //var arrComp = d.Company.Trim().Split(",");
                //for (int i = 0; i < arrComp.Length; i++)
                //{
                //    cmd += " M.COMPANY = '" + arrComp[i] + "' ";
                //    if (i < arrComp.Length - 1)
                //    {
                //        cmd += " or";
                //    }
                //}
                //cmd += " )";
            }
            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    cmd += " D.DEPCODEOL = '" + d.DeptCode + "'";
                    //cmd += " D.DEPCODEOL = @DEPTCODEEOL";
                }
                if (d.DeptLST != null && d.DeptLST.Length > 0)
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or D.DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
            }
            cmd += " UNION ";
            cmd += " select null as QTY_TOTAL,count(D.ASSETNO) as QTY_CHECKED,null as QTY_WAIT from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M where D.SQNO = M.SQNO and D.FLAG in ('P') and D.PCODE is not null and D.PCODE  <> ''  ";
            //if (!String.IsNullOrEmpty(d.Company))
            //{
            //    cmd += " and M.COMPANY in (" + comp + ") ";
            //    //cmd += " and M.COMPANY in (@COMPANY) ";
            //}
            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    cmd += " D.DEPCODEOL = '" + d.DeptCode + "'";
                    //cmd += " D.DEPCODEOL = @DEPTCODEEOL";
                }
                if (!String.IsNullOrEmpty(d.DeptLST))
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or D.DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
            }
            cmd += " UNION ";
            cmd += " select null as QTY_TOTAL,null as QTY_CHECKED,count(D.ASSETNO) as QTY_WAIT from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M where D.SQNO = M.SQNO and D.FLAG in ('P','S') and (D.PCODE is  null or D.PCODE  = '')  ";
            //if (!String.IsNullOrEmpty(d.Company))
            //{
            //    cmd += " and M.COMPANY in (" + comp + ") ";
            //    //cmd += " and M.COMPANY in (@COMPANY) ";
            //}
            if (!d.Menu3)
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    cmd += " D.DEPCODEOL = '" + d.DeptCode + "'";
                    //cmd += " D.DEPCODEOL = @DEPTCODEEOL";
                }
                if (!String.IsNullOrEmpty(d.DeptLST))
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or D.DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
            }
            cmd += " ) as X";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>(cmd, param).ToList();
            return res;
        }
    }
}
