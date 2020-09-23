using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Audit
{
    public class AUDITPOSTMSTTODEPAdo : Base
    {
        private static AUDITPOSTMSTTODEPAdo instant;

        public static AUDITPOSTMSTTODEPAdo GetInstant()
        {
            if (instant == null) instant = new AUDITPOSTMSTTODEPAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private AUDITPOSTMSTTODEPAdo()
        {

        }

        public int SP_AUDITPOSTMSTTODEP(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "  EXEC  SP_AUDITPOSTMSTTODEP";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@MODE = '" + d.MODE + "'";
            sql += " ,@FINDY = '" + d.FINDY + "'";
            sql += " ,@PCODE= '" + d.PCODE + "'";
            sql += " ,@PNAME= '" + d.PNAME + "'";
            sql += " ,@MEMO= '" + d.MEMO1 + "'";
            
            var res = ExecuteNonQuery(sql, param);
            return res;
        }


        public List<ASAUDITPOSTMSTTODEP> getDataToClear(AuditPostReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT  B.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.INPID) as INPNAME ,ISNULL(D.FLAG_ACCEPT,'') as FLAG_ACCEPT";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.stid) as stidname";
            sql += "  FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")  B";
            sql += " left outer join [FT_ASAUDITCUTDATEMST_COMPANY](" + QuoteStr(d.COMPANY) + ") as D on D.sqno = B.sqno";
            sql += " where B.SQNO = " + QuoteStr(d.SQNO);
            sql += " and B.COMPANY = " + QuoteStr(d.COMPANY);
            //sql += " and isnull(STY,'') = '' ";
            sql += " and ISNULL(D.FLAG_ACCEPT,'')  in ('','0') ";

            if (!String.IsNullOrEmpty(d.filter))
            {
                switch (d.filter)
                {
                    case "0":
                        sql += " and isnull(PCOD,'') <> '' ";
                        break;
                    case "1":
                        sql += " and isnull(PCOD,'') = '' ";
                        break;

                }
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and B.ASSETNO = " + QuoteStr(d.ASSETNO);
            }

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby != null && d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var res = Query<ASAUDITPOSTMSTTODEP>(sql, param).ToList();
            return res;
        }

        public int SP_AUDITPOSTMSTTODEPPHONE(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "  EXEC  SP_AUDITPOSTMSTTODEPPHONE";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@INPID = '" + d.INPID + "'";
            sql += " ,@IMGPATH = '" + d.IMGPATH + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@FLAG = '" + d.FLAG + "'";
            sql += " ,@MEMO= '" + d.MEMO1 + "'";
            sql += " ,@MODE = '" + d.MODE + "'";            
            
            var res = ExecuteNonQuery(sql, param);
            return res;
        }

        public List<ASAUDITPOSTMSTTODEP> getDataToConfirm(AuditPostReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT  B.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.INPID) as INPNAME ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.stid) as stidname";
            sql += "  FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")  B";
            sql += " where B.SQNO = " + QuoteStr(d.SQNO);
            sql += " and B.COMPANY = " + QuoteStr(d.COMPANY);
            sql += " and isnull(STY,'') = '' ";
            sql += " and ISNULL(COMY, '') = case when ISNULL(STCY , '') <> '' then 'Y' else ISNULL(COMY, '') end";

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " where B.ASSETNO = " + QuoteStr(d.ASSETNO);
            }

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby != null && d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var res = Query<ASAUDITPOSTMSTTODEP>(sql, param).ToList();
            return res;
        }

        public List<ASAUDITPOSTMSTTODEP> getAuditAssetNo(AuditPostReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ") as a ";
            sql += " where a.SQNO = '" + d.SQNO + "'";
            sql += " and a.COMPANY = '" + d.COMPANY + "'";
            sql += " and a.ASSETNO = '" + d.ASSETNO + "'";
            //sql += " and a.INPID = '" + d.UCODE + "'";

            var res = Query<ASAUDITPOSTMSTTODEP>(sql, param).ToList();
            return res;
        }

        public List<ASAUDITPOSTMSTTODEP> getDataToCompEdit(AuditPostReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT  B.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.INPID) as INPNAME ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.stid) as stidname";
            sql += "  FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")  B";
            sql += " where B.SQNO = " + QuoteStr(d.SQNO);
            sql += " and B.COMPANY = " + QuoteStr(d.COMPANY);

            sql += " and SNDACCDT IS NULL";



            if (!String.IsNullOrEmpty(d.depy))
            {
                sql += " and isnull(STY,'') = " + QuoteStr(d.depy);
            }

            if (!String.IsNullOrEmpty(d.filter))
            {
                switch (d.filter)
                {
                    case "0":
                        sql += " and isnull(PCOD,'') <> '' ";
                            break;
                    case "1":
                        sql += " and isnull(PCOD,'') = '' ";
                        break;

                }
            }

            if (!String.IsNullOrEmpty(d.filter))
            {
                sql += " and isnull(STY,'') = " + QuoteStr(d.filter);
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and B.ASSETNO = " + QuoteStr(d.ASSETNO);
            }

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby != null && d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var res = Query<ASAUDITPOSTMSTTODEP>(sql, param).ToList();
            return res;
        }

        public List<ASAUDITPOSTMSTTODEP> getDataToACCEdit(AuditPostReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT  B.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.INPID) as INPNAME ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.stid) as stidname";
            sql += "  FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")  B";
            sql += " where B.SQNO = " + QuoteStr(d.SQNO);
            sql += " and B.COMPANY = " + QuoteStr(d.COMPANY);

            sql += " and SNDACCDT IS NULL";
            sql += " AND  B.PCODE IN (SELECT  A.PCODE FROM  FT_ASSTProblem() A  WHERE  A.SACC = 'Y' and COMPANY = " + QuoteStr(d.COMPANY) + ") ";



            if (!String.IsNullOrEmpty(d.depy))
            {
                sql += " and isnull(STY,'') = " + QuoteStr(d.depy);
            }

            if (!String.IsNullOrEmpty(d.filter))
            {
                switch (d.filter)
                {
                    case "0":
                        sql += " and isnull(PCOD,'') <> '' ";
                        break;
                    case "1":
                        sql += " and isnull(PCOD,'') = '' ";
                        break;

                }
            }

            if (!String.IsNullOrEmpty(d.filter))
            {
                sql += " and isnull(STY,'') = " + QuoteStr(d.filter);
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and B.ASSETNO = " + QuoteStr(d.ASSETNO);
            }

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby != null && d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var res = Query<ASAUDITPOSTMSTTODEP>(sql, param).ToList();
            return res;
        }



    }
}
