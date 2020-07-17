using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response.Asset;
using ASSETKKF_MODEL.Response.Report;
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
            sql = "  select   * from (  ";
            sql += " select * from (select P.* ,MEMO1 as AUDIT_NOTE ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME  ";
            sql += " , (select max(audit_no) from FT_ASAUDITCUTDATEMST() where SQNO = P.SQNO and COMPANY = P.COMPANY) as audit_no  ";
            sql += "  ,(case when isnull(P.PCODE,'') = '' then '' else P.PCODE + ' : ' + P.PNAME end ) as AUDIT_RESULT  ";
            sql += " ,(CAST(P.MN AS varchar) + ' / ' + CAST(P.YR AS varchar) + ' - ' + CAST(P.YRMN AS varchar) ) as AUDIT_AT  ";
            sql += " from  [FT_ASAUDITPOSTMST] () as P ";
           // sql += " left outer join [FT_UserAsset] ('') as U on U.OFFICECODE = P.INPID and U.COMPANY = P.COMPANY   ";
            sql += " left outer join [FT_ASAUDITPOSTMST_PHONE] () as D on D.SQNO = P.SQNO and D.COMPANY = P.COMPANY and D.ASSETNO = P.ASSETNO ";
            sql += " where 1 = 1";


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

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and P.TYPECODE = " + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and P.GASTCODE = " + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and P.OFFICECODE = " + QuoteStr(d.OFFICECODE);
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


            sql += "  ) as x";
            sql += " union";
            sql += " select * from ((select C.* ,c.MEMO1 as AUDIT_NOTE ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.INPID) as INPNAME    , M.AUDIT_NO";
            sql += " ,(case when isnull(c.PCODE,'') = '' then '' else c.PCODE + ' : ' + c.PNAME end ) as AUDIT_RESULT";
            sql += " ,(CAST(c.MN AS varchar) + ' / ' + CAST(c.YR AS varchar) + ' - ' + CAST(c.YRMN AS varchar) ) as AUDIT_AT";
            sql += " from FT_ASAUDITCUTDATE() AS C";
            sql += " left outer join FT_ASAUDITCUTDATEMST() M on M.SQNO = C.SQNO and M.COMPANY = C.COMPANY";
            //sql += " left outer join [FT_UserAsset] ('') as U on U.OFFICECODE = c.INPID and U.COMPANY = c.COMPANY";
            sql += " where 1 = 1";

            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                sql += " and C.COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and C.DEPMST =" + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and C.YR =" + QuoteStr(d.YEAR);
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and C.MN =" + QuoteStr(d.MN);
            }

            if (d.cutdt != null)
            {
                param.Add("@CUTDT", d.cutdt);
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, C.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.audit_no))
            {

                param.Add("@AUDITNO", d.audit_no);
                param.Add("@auditno_lk", $"%{d.audit_no}%");
                sql += " AND (C.audit_no LIKE @auditno_lk OR C.audit_no = @AUDITNO )";
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                param.Add("@SQNO", d.sqno);
                sql += " and C.sqno = " + QuoteStr(d.sqno);

            }

            if (!string.IsNullOrEmpty(d.sqno_copm))
            {
                sql += " and C.COMPANY = " + QuoteStr(d.sqno_copm);
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                param.Add("@DEPCODEOL", d.DEPCODEOL);
                sql += " and C.DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            }

            if (!String.IsNullOrEmpty(d.PCODE))
            {
                param.Add("@PCODE", d.PCODE);
                sql += " and C.PCODE = " + QuoteStr(d.PCODE);
            }


            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and C.TYPECODE = " + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and C.GASTCODE = " + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and C.OFFICECODE = " + QuoteStr(d.OFFICECODE);
            }


            if (!d.Menu3 && ((!String.IsNullOrEmpty(d.DeptCode)) || d.DeptLST != null))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    sql += " C.DEPCODEOL like (case when isnull('" + d.DeptCode + "','') <> '' then   SUBSTRING('" + d.DeptCode + "',1,1) else '' end + '%')";
                }
                if ((d.DeptLST != null && d.DeptLST.Length > 0) && (d.DeptLST != "null"))
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or C.DEPCODEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                sql += " )";
            }

            sql += " and ASSETNO not in (select assetno from [FT_ASAUDITPOSTMST]() P where 1 =1";

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

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and P.TYPECODE = " + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and P.GASTCODE = " + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and P.OFFICECODE = " + QuoteStr(d.OFFICECODE);
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


            sql += " ) )";
            sql += " ) as b";
            sql += " ) as C";
            // sql += " order by AUDIT_RESULT desc, ASSETNO ";

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var obj = Query<RptAuditAsset>(sql, param).ToList();


            List<ASSETKKF_MODEL.Response.Report.RptAuditAsset> res = new List<ASSETKKF_MODEL.Response.Report.RptAuditAsset>();
            res = obj;

            //if (obj != null && obj.Count > 0)
            //{
            //    obj.ForEach(x => {
            //        res.Add(new ASSETKKF_MODEL.Response.Report.RptAuditAsset
            //        {
            //            ASSETNO = x.ASSETNO,
            //            ASSETNAME = x.ASSETNAME,
            //            TYPECODE = x.TYPECODE,
            //            TYPENAME = x.TYPENAME,
            //            GASTCODE = x.GASTCODE,
            //            GASTNAME = x.GASTNAME,
            //            OFFICECODE = x.OFFICECODE,
            //            OFNAME = x.OFNAME,
            //            DEPCODEOL = x.DEPCODEOL,
            //            STNAME = x.STNAME,
            //            AUDIT_RESULT = !String.IsNullOrEmpty(x.PCODE)?( x.PCODE + " : " + x.PNAME):"",
            //            AUDIT_NOTE = x.MEMO1,
            //            AUDIT_NO = x.AUDIT_NO,
            //            SQNO = x.SQNO,
            //            AUDIT_AT = x.MN + "/" + x.YR + " - " + x.YRMN.ToString(),
            //            ACCDT = x.ACCDT,
            //            COMPDT = x.COMPDT,
            //            LEADERCODE = x.LEADERCODE,
            //            LEADERNAME = x.LEADERNAME,
            //            INPID = x.INPID,
            //            INPNAME = x.INPNAME,
            //            INPDT = x.INPDT,
            //            DEPMST = x.DEPMST,
            //            IMGPATH = x.IMGPATH,
            //            POSITCODE = x.POSITCODE,
            //            POSITNAME = x.POSITNAME

            //        }); 
            //    });
            //}


            return res;
        }

        public List<ASSETKKF_MODEL.Response.Report.RptAuditAssetTRN> GetAuditAssetTRNLists(ASSETKKF_MODEL.Request.Report.RptAuditAssetReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select P.*     ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME,MEMO1 as AUDIT_NOTE    ";
            sql += "  , (select max(audit_no) from FT_ASAUDITCUTDATEMST() where SQNO = P.SQNO and COMPANY = P.COMPANY) as audit_no";
            sql += "  ,(case when isnull(P.PCODE,'') = '' then '' else P.PCODE + ' : ' + P.PNAME end ) as AUDIT_RESULT  ";
            sql += " ,(CAST(P.MN AS varchar) + ' / ' + CAST(P.YR AS varchar) + ' - ' + CAST(P.YRMN AS varchar) ) as AUDIT_AT  ";
            sql += " from  [FT_ASAUDITPOSTTRN] ()  as P ";
            //sql += " left outer join [FT_UserAsset] ('') as U on U.OFFICECODE = P.INPID and U.COMPANY = P.COMPANY";
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

            // sql += " order by P.SQNO desc";

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }


            var obj = Query<RptAuditAssetTRN>(sql, param).ToList();


            List<ASSETKKF_MODEL.Response.Report.RptAuditAssetTRN> res = new List<ASSETKKF_MODEL.Response.Report.RptAuditAssetTRN>();
            res = obj;

            //if (obj != null && obj.Count > 0)
            //{
            //    obj.ForEach(x => {
            //        res.Add(new ASSETKKF_MODEL.Response.Report.RptAuditAssetTRN
            //        {
            //            ASSETNO = x.ASSETNO,
            //            ASSETNAME = x.ASSETNAME,
            //            TYPECODE = x.TYPECODE,
            //            TYPENAME = x.TYPENAME,
            //            GASTCODE = x.GASTCODE,
            //            GASTNAME = x.GASTNAME,
            //            OFFICECODE = x.OFFICECODE,
            //            OFNAME = x.OFNAME,
            //            DEPCODEOL = x.DEPCODEOL,
            //            STNAME = x.STNAME,
            //            AUDIT_RESULT = !String.IsNullOrEmpty(x.PCODE) ? (x.PCODE + " : " + x.PNAME) : "",
            //            AUDIT_NOTE = x.MEMO1,
            //            AUDIT_NO = x.AUDIT_NO,
            //            SQNO = x.SQNO,
            //            AUDIT_AT = x.MN + "/" + x.YR + " - " + x.YRMN.ToString(),
            //            ACCDT = x.ACCDT,
            //            COMPDT = x.COMPDT,
            //            LEADERCODE = x.LEADERCODE,
            //            LEADERNAME = x.LEADERNAME,
            //            INPID = x.INPID,
            //            INPNAME = x.INPNAME,
            //            INPDT = x.INPDT,
            //            DEPMST = x.DEPMST,
            //            IMGPATH = x.IMGPATH,
            //            POSITCODE = x.POSITCODE,
            //            POSITNAME = x.POSITNAME

            //        });
            //    });
            //}


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

        public List<Multiselect> GetAuditOFFICE(ASSETKKF_MODEL.Request.Report.RptAuditAssetReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT  OFFICECODE as id,(OFFICECODE + ' : ' + OFNAME) as description   FROM  ( ";
            cmd += " Select    OFFICECODE,MAX(OFNAME) AS OFNAME,DEPCODEOL,MAX(STNAME) AS STNAME,DEPCODE  from  FT_ASAUDITCUTDATE() D where 1 = 1 ";

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

            if (!String.IsNullOrEmpty(d.sqno))
            {
                cmd += " and SQNO = '" + d.sqno + "'";
            }


            cmd += " GROUP BY OFFICECODE,DEPCODEOL,DEPCODE )  ";
            cmd += " AS X   WHERE  1=1 ";

            var res = Query<Multiselect>(cmd, param).ToList();
            return res;
        }

        public List<Multiselect> GetTYPEASSET(ASSETKKF_MODEL.Request.Report.RptAuditAssetReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " Select TYPECODE as id, (TYPECODE + ' : ' + TYPENAME ) as description from FT_STTYPEASSET() WHERE 1=1 ";

            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

           
            cmd += " GROUP BY TYPECODE ,TYPENAME  ";
            cmd += " order BY TYPECODE ";

            var res = Query<Multiselect>(cmd, param).ToList();
            return res;
        }

        public List<Multiselect> GetGROUPASSET(ASSETKKF_MODEL.Request.Report.RptAuditAssetReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " Select GASTCODE as id, (GASTCODE + ' : ' + GASTNAME ) as description   ";
            cmd += " FROM (SELECT   A.*,B.ST from FT_STGROUPASSET() A,FT_STTYPEASSET() B ";
            cmd += " WHERE  A.TYPECODE = B.TYPECODE and A.compANY = B.cOMPANY  "; // AND B.ST = 'N'


            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                cmd += " and A.COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                cmd += " and A.TYPECODE = '" + d.TYPECODE + "'";
            }


            cmd += " ) AS X WHERE 1=1 ";


            cmd += " GROUP BY GASTCODE ,GASTNAME ";
            cmd += " order BY GASTCODE ";

            var res = Query<Multiselect>(cmd, param).ToList();
            return res;
        }


    }
}
