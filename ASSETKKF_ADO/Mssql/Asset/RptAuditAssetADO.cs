using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class RptAuditAssetADO : Base
    {
        private static RptAuditAssetADO instant;

        public static RptAuditAssetADO GetInstant()
        {
            if (instant == null) instant = new RptAuditAssetADO();
            return instant;
        }

        private string conectStr { get; set; }

        private RptAuditAssetADO()
        {
        }

        public List<ASSETKKF_MODEL.Response.Report.RptAuditAsset> GetAuditAssetLists(ASSETKKF_MODEL.Request.Report.RptAuditAssetReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "  select P.*    , U.OFNAME as INPNAME     ";
            sql += "  , (select max(audit_no) from FT_ASAUDITCUTDATEMST() where SQNO = P.SQNO and COMPANY = P.COMPANY) as audit_no ";
            sql += " from  [FT_ASAUDITPOSTMST] () as P left outer join [FT_UserAsset] ('') as U on U.OFFICECODE = P.INPID and U.COMPANY = P.COMPANY  ";
            sql += " left outer join [FT_ASAUDITPOSTMST_PHONE] () as D on D.SQNO = P.SQNO and D.COMPANY = P.COMPANY and D.ASSETNO = P.ASSETNO";
            sql += " where 1 = 1";

            /*sql += " and YR = (SELECT YR from(  ";
            sql += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITPOSTMST()  ";
            sql += " where YR = (SELECT max(YR) FROM FT_ASAUDITPOSTMST() )  ";
            sql += " group by YR  ";
            sql += "   ) as a)  ";
            sql += " and MN = (SELECT MN from(  ";
            sql += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITPOSTMST()  ";
            sql += " where YR = (SELECT max(YR) FROM FT_ASAUDITPOSTMST() )  ";
            sql += " group by YR  ";
            sql += "  ) as b)  ";
            sql += " and YRMN = (SELECT YRMN from(  ";
            sql += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITPOSTMST()  ";
            sql += " where YR = (SELECT max(YR) FROM FT_ASAUDITPOSTMST() )  ";
            sql += " group by YR  ";
            sql += "   ) as c)   ";*/


            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                sql += " and P.COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and P.DEPMST =" + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and P.YR =" + QuoteStr(d.YEAR);
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and P.MN =" + QuoteStr(d.MN);
            }

            if (d.cutdt != null)
            {
                param.Add("@CUTDT", d.cutdt);
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if(d.inpdt != null)
            {
                param.Add("@INPDT", d.inpdt);
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.inpdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.inpdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                //param.Add("@AUDITNO", d.audit_no);
                //sql += " and audit_no = @AUDITNO";

                param.Add("@AUDITNO", d.audit_no);
                param.Add("@auditno_lk", $"%{d.audit_no}%");
                sql += " AND (P.audit_no LIKE @auditno_lk OR P.audit_no = @AUDITNO )";
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                param.Add("@SQNO", d.sqno);
                sql += " and P.sqno = " + QuoteStr(d.sqno);

            }

            if (!string.IsNullOrEmpty(d.sqno_copm))
            {
                sql += " and P.COMPANY = " + QuoteStr(d.sqno_copm);
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                param.Add("@DEPCODEOL", d.DEPCODEOL);
                sql += " and P.DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            }

            if (!String.IsNullOrEmpty(d.LEADERCODE))
            {
                param.Add("@LEADERCODE", d.LEADERCODE);
                sql += " and P.LEADERCODE = " + QuoteStr(d.LEADERCODE);
            }

            if (!String.IsNullOrEmpty(d.PCODE))
            {
                param.Add("@PCODE", d.PCODE);
                sql += " and P.PCODE = " + QuoteStr(d.PCODE);
            }


            if (!d.Menu3 && ((!String.IsNullOrEmpty(d.DeptCode)) || d.DeptLST != null))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    sql += " P.DEPCODEOL like (case when isnull('" + d.DeptCode + "','') <> '' then   SUBSTRING('" + d.DeptCode + "',1,1) else '' end + '%')";
                }
                if ((d.DeptLST != null && d.DeptLST.Length > 0) && (d.DeptLST != "null"))
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or P.DEPCODEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                sql += " )";
            }

            sql += " order by P.SQNO desc";




            var obj = Query<ASAUDITPOSTMST>(sql, param).ToList();


            List<ASSETKKF_MODEL.Response.Report.RptAuditAsset> res = new List<ASSETKKF_MODEL.Response.Report.RptAuditAsset>();

            if (obj != null && obj.Count > 0)
            {
                obj.ForEach(x => {
                    res.Add(new ASSETKKF_MODEL.Response.Report.RptAuditAsset
                    {
                        ASSETNO = x.ASSETNO,
                        ASSETNAME = x.ASSETNAME,
                        TYPECODE = x.TYPECODE,
                        TYPENAME = x.TYPENAME,
                        GASTCODE = x.GASTCODE,
                        GASTNAME = x.GASTNAME,
                        OFFICECODE = x.OFFICECODE,
                        OFNAME = x.OFNAME,
                        DEPCODEOL = x.DEPCODEOL,
                        STNAME = x.STNAME,
                        AUDIT_RESULT = !String.IsNullOrEmpty(x.PCODE)?( x.PCODE + " : " + x.PNAME):"",
                        AUDIT_NOTE = x.MEMO1,
                        AUDIT_NO = x.AUDIT_NO,
                        SQNO = x.SQNO,
                        AUDIT_AT = x.MN + "/" + x.YR + " - " + x.YRMN.ToString(),
                        ACCDT = x.ACCDT,
                        COMPDT = x.COMPDT,
                        LEADERCODE = x.LEADERCODE,
                        LEADERNAME = x.LEADERNAME,
                        INPID = x.INPID,
                        INPNAME = x.INPNAME,
                        INPDT = x.INPDT,
                        DEPMST = x.DEPMST,
                        IMGPATH = x.IMGPATH,
                        POSITCODE = x.POSITCODE,
                        POSITNAME = x.POSITNAME

                    }); 
                });
            }


            return res;
        }

        public List<ASSETKKF_MODEL.Response.Report.RptAuditAsset> GetAuditAssetTRNLists(ASSETKKF_MODEL.Request.Report.RptAuditAssetReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select P.*    , U.OFNAME as INPNAME    ";
            sql += "  , (select max(audit_no) from FT_ASAUDITCUTDATEMST() where SQNO = P.SQNO and COMPANY = P.COMPANY) as audit_no";
            sql += " from  [FT_ASAUDITPOSTTRN] ()  as P left outer join [FT_UserAsset] ('') as U on U.OFFICECODE = P.INPID and U.COMPANY = P.COMPANY";
            sql += " left outer join [FT_ASAUDITPOSTTRN_PHONE] () as D on D.SQNO = P.SQNO and D.COMPANY = P.COMPANY and D.ASSETNO = P.ASSETNO";
            sql += " where 1 = 1";

            /*sql += " and YR = (SELECT YR from(  ";
            sql += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITPOSTTRN()  ";
            sql += " where YR = (SELECT max(YR) FROM FT_ASAUDITPOSTTRN() )  ";
            sql += " group by YR  ";
            sql += "   ) as a)  ";
            sql += " and MN = (SELECT MN from(  ";
            sql += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITPOSTTRN()  ";
            sql += " where YR = (SELECT max(YR) FROM FT_ASAUDITPOSTTRN() )  ";
            sql += " group by YR  ";
            sql += "  ) as b)  ";
            sql += " and YRMN = (SELECT YRMN from(  ";
            sql += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITPOSTTRN()  ";
            sql += " where YR = (SELECT max(YR) FROM FT_ASAUDITPOSTTRN() )  ";
            sql += " group by YR  ";
            sql += "   ) as c)   ";*/


            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                sql += " and P.COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and P.DEPMST =" + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and P.YR =" + QuoteStr(d.YEAR);
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and P.MN =" + QuoteStr(d.MN);
            }

            if (d.cutdt != null)
            {
                param.Add("@CUTDT", d.cutdt);
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (d.inpdt != null)
            {
                param.Add("@INPDT", d.inpdt);
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, P.inpdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.inpdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                //param.Add("@AUDITNO", d.audit_no);
                //sql += " and audit_no = @AUDITNO";

                param.Add("@AUDITNO", d.audit_no);
                param.Add("@auditno_lk", $"%{d.audit_no}%");
                sql += " AND (P.audit_no LIKE @auditno_lk OR P.audit_no = @AUDITNO )";
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                param.Add("@SQNO", d.sqno);
                sql += " and P.sqno = " + QuoteStr(d.sqno);

            }

            if (!string.IsNullOrEmpty(d.sqno_copm))
            {
                sql += " and P.COMPANY = " + QuoteStr(d.sqno_copm);
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                param.Add("@DEPCODEOL", d.DEPCODEOL);
                sql += " and P.DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            }

            if (!String.IsNullOrEmpty(d.LEADERCODE))
            {
                param.Add("@LEADERCODE", d.LEADERCODE);
                sql += " and P.LEADERCODE = " + QuoteStr(d.LEADERCODE);
            }

            if (!String.IsNullOrEmpty(d.PCODE))
            {
                param.Add("@PCODE", d.PCODE);
                sql += " and P.PCODE = " + QuoteStr(d.PCODE);
            }

            if (!d.Menu3 && ((!String.IsNullOrEmpty(d.DeptCode)) || d.DeptLST != null))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    sql += " P.DEPCODEOL like (case when isnull('" + d.DeptCode + "','') <> '' then   SUBSTRING('" + d.DeptCode + "',1,1) else '' end + '%')";
                }
                if ((d.DeptLST != null && d.DeptLST.Length > 0) && (d.DeptLST != "null"))
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or P.DEPCODEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                sql += " )";
            }

            sql += " order by P.SQNO desc";


            var obj = Query<ASAUDITPOSTTRN>(sql, param).ToList();


            List<ASSETKKF_MODEL.Response.Report.RptAuditAsset> res = new List<ASSETKKF_MODEL.Response.Report.RptAuditAsset>();

            if (obj != null && obj.Count > 0)
            {
                obj.ForEach(x => {
                    res.Add(new ASSETKKF_MODEL.Response.Report.RptAuditAsset
                    {
                        ASSETNO = x.ASSETNO,
                        ASSETNAME = x.ASSETNAME,
                        TYPECODE = x.TYPECODE,
                        TYPENAME = x.TYPENAME,
                        GASTCODE = x.GASTCODE,
                        GASTNAME = x.GASTNAME,
                        OFFICECODE = x.OFFICECODE,
                        OFNAME = x.OFNAME,
                        DEPCODEOL = x.DEPCODEOL,
                        STNAME = x.STNAME,
                        AUDIT_RESULT = !String.IsNullOrEmpty(x.PCODE) ? (x.PCODE + " : " + x.PNAME) : "",
                        AUDIT_NOTE = x.MEMO1,
                        AUDIT_NO = x.AUDIT_NO,
                        SQNO = x.SQNO,
                        AUDIT_AT = x.MN + "/" + x.YR + " - " + x.YRMN.ToString(),
                        ACCDT = x.ACCDT,
                        COMPDT = x.COMPDT,
                        LEADERCODE = x.LEADERCODE,
                        LEADERNAME = x.LEADERNAME,
                        INPID = x.INPID,
                        INPNAME = x.INPNAME,
                        INPDT = x.INPDT,
                        DEPMST = x.DEPMST,
                        IMGPATH = x.IMGPATH,
                        POSITCODE = x.POSITCODE,
                        POSITNAME = x.POSITNAME

                    });
                });
            }


            return res;
        }

        public List<Multiselect> GetAuditCUTDT(ASSETKKF_MODEL.Request.Report.RptAuditAssetReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT CUTDT as id,convert(varchar, max(CUTDT),103) as description FROM [dbo].[FT_ASAUDITCUTDATEMST] () ";
            cmd += " where FLAG not in ('X') ";

            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                cmd += " and DEPMST = '" + d.DEPMST + "'";
            }


            cmd += " group by CUTDT ";
            cmd += " order by CUTDT desc ";

            var res = Query<Multiselect>(cmd, param).ToList();
            return res;
        }


    }
}
