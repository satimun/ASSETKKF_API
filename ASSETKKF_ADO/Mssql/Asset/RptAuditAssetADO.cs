using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
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
            sql = " select *   ";
            sql += " , (SELECT max(OFNAME) FROM [dbo].[FT_UserAsset] (FT_ASAUDITPOSTMST.INPID)) as INPNAME  ";
            sql += "  , (select max(audit_no) from FT_ASAUDITCUTDATEMST() where SQNO = FT_ASAUDITPOSTMST.SQNO) as audit_no";
            sql += " from  [FT_ASAUDITPOSTMST] ()  ";
            sql += " where 1 = 1";

            sql += " and YR = (SELECT YR from(  ";
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
            sql += "   ) as c)   ";


            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                sql += " and COMPANY in (" + comp + ") ";
            }

            if(d.cutdt != null)
            {
                param.Add("@CUTDT", d.cutdt);
                sql += " and cutdt = " + d.cutdt;
            }

            if(d.inpdt != null)
            {
                param.Add("@INPDT", d.cutdt);
                sql += " and inpdt = " + QuoteStr(d.sqno);
            }

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                //param.Add("@AUDITNO", d.audit_no);
                //sql += " and audit_no = @AUDITNO";

                param.Add("@AUDITNO", d.audit_no);
                param.Add("@auditno_lk", $"%{d.audit_no}%");
                sql += " AND (audit_no LIKE @auditno_lk OR audit_no = @AUDITNO )";
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                param.Add("@SQNO", d.sqno);
                sql += " and sqno = " + QuoteStr(d.sqno);

            }

            if (!string.IsNullOrEmpty(d.sqno_copm))
            {
                sql += " and COMPANY = " + QuoteStr(d.sqno_copm);
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                param.Add("@DEPCODEOL", d.DEPCODEOL);
                sql += " and DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            }

            if (!String.IsNullOrEmpty(d.LEADERCODE))
            {
                param.Add("@LEADERCODE", d.LEADERCODE);
                sql += " and LEADERCODE = " + QuoteStr(d.LEADERCODE);
            }

            if (!String.IsNullOrEmpty(d.PCODE))
            {
                param.Add("@PCODE", d.PCODE);
                sql += " and PCODE = " + QuoteStr(d.PCODE);
            }
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
            sql = " select *   ";
            sql += " , (SELECT max(OFNAME) FROM [dbo].[FT_UserAsset] (FT_ASAUDITPOSTTRN.INPID)) as INPNAME  ";
            sql += "  , (select max(audit_no) from FT_ASAUDITCUTDATEMST() where SQNO = FT_ASAUDITPOSTTRN.SQNO) as audit_no";
            sql += " from  [FT_ASAUDITPOSTTRN] ()  ";
            sql += " where 1 = 1";

            sql += " and YR = (SELECT YR from(  ";
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
            sql += "   ) as c)   ";


            if (!String.IsNullOrEmpty(d.company))
            {
                var comp = "";
                comp = "'" + d.company.Replace(",", "','") + "'";
                sql += " and COMPANY in (" + comp + ") ";
            }

            if (d.cutdt != null)
            {
                param.Add("@CUTDT", d.cutdt);
                sql += " and cutdt = " + d.cutdt;
            }

            if (d.inpdt != null)
            {
                param.Add("@INPDT", d.cutdt);
                sql += " and inpdt = " + QuoteStr(d.sqno);
            }

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                //param.Add("@AUDITNO", d.audit_no);
                //sql += " and audit_no = @AUDITNO";

                param.Add("@AUDITNO", d.audit_no);
                param.Add("@auditno_lk", $"%{d.audit_no}%");
                sql += " AND (audit_no LIKE @auditno_lk OR audit_no = @AUDITNO )";
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                param.Add("@SQNO", d.sqno);
                sql += " and sqno = " + QuoteStr(d.sqno);

            }

            if (!string.IsNullOrEmpty(d.sqno_copm))
            {
                sql += " and COMPANY = " + QuoteStr(d.sqno_copm);
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                param.Add("@DEPCODEOL", d.DEPCODEOL);
                sql += " and DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            }

            if (!String.IsNullOrEmpty(d.LEADERCODE))
            {
                param.Add("@LEADERCODE", d.LEADERCODE);
                sql += " and LEADERCODE = " + QuoteStr(d.LEADERCODE);
            }

            if (!String.IsNullOrEmpty(d.PCODE))
            {
                param.Add("@PCODE", d.PCODE);
                sql += " and PCODE = " + QuoteStr(d.PCODE);
            }
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


    }
}
