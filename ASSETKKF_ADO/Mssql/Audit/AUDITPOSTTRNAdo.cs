﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Audit
{
    public class AUDITPOSTTRNAdo : Base
    {
        private static AUDITPOSTTRNAdo instant;

        public static AUDITPOSTTRNAdo GetInstant()
        {
            if (instant == null) instant = new AUDITPOSTTRNAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private AUDITPOSTTRNAdo()
        {
           
        }

        public List<ASAUDITPOSTTRN> getPOSTTRNDuplicate(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT P.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME from  [FT_ASAUDITPOSTTRN_COMPANY](" + QuoteStr(d.COMPANY) + ") as P";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.stid) as stidname";
            sql += " left outer join  FT_ASAUDITCUTDATEMST_COMPANY](" + QuoteStr(d.COMPANY) + ") M on M.SQNO = P.SQNO and M.COMPANY = P.COMPANY";
            sql += " where P.SQNO = " + QuoteStr(d.SQNO);
            sql += " and P.COMPANY = " + QuoteStr(d.COMPANY);
            sql += " and  M.FLAG not in ('X','C')";

            sql += " AND  P.ASSETNO IN ( SELECT  X.ASSETNO    FROM    [FT_ASAUDITPOSTTRN_COMPANY](" + QuoteStr(d.COMPANY) + ")  X";
            sql += "  where  X.SQNO = " + QuoteStr(d.SQNO);
            sql += "  and x.COMPANY = " + QuoteStr(d.COMPANY);
            sql += " GROUP BY  X.ASSETNO  HAVING  COUNT(X.ASSETNO) >1  )";

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and ASSETNO = '" + d.ASSETNO + "'";
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and YR = '" + d.YEAR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and MN = '" + d.MN + "'";
            }
            //if (!String.IsNullOrEmpty(d.DEPMST))
            //{
            //    sql += " and DEPCODEOL in (SELECT [DEPCODEOL] ";
            //    sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") ";
            //    sql += " where DEPMST = '" + d.DEPMST + "'";
            //    sql += " and company = '" + d.COMPANY + "'";
            //    if (!String.IsNullOrEmpty(d.YEAR))
            //    {
            //        sql += " and YR = " + QuoteStr(d.YEAR);
            //    }

            //    if (!String.IsNullOrEmpty(d.MN))
            //    {
            //        sql += " and MN = " + QuoteStr(d.MN);
            //    }

            //    if (!String.IsNullOrEmpty(d.YRMN))
            //    {
            //        sql += " and YRMN = " + QuoteStr(d.YRMN);
            //    }
            //    sql += " group by[DEPCODEOL])";
            //}

            //if (!String.IsNullOrEmpty(d.cutdt))
            //{
            //    sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            //}

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and OFFICECODE = '" + d.OFFICECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and TYPECODE = '" + d.TYPECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and GASTCODE = '" + d.GASTCODE + "'";
            }

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (!String.IsNullOrEmpty(d.orderby) && d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (!String.IsNullOrEmpty(d.orderby) &&  d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (!String.IsNullOrEmpty(d.orderby) &&  d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).ToList();
            return res;

        }

        public int saveAUDITPOSTTRN(AUDITPOSTTRNReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [dbo].[SP_AUDITPOSTTRN]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@DEPMST = '" + d.DEPMST + "'";
            sql += " ,@YR= " + d.YR;
            sql += " ,@MN = " + d.MN;
            sql += " ,@YRMN = " + d.YRMN;
            sql += ", @FINDY = '" + d.FINDY + "'";
            sql += " ,@PCODE = '" + d.PCODE + "'";
            sql += " ,@PNAME = '" + d.PNAME + "'";
            sql += " ,@MEMO1 = '" + d.MEMO1 + "'";
            sql += " ,@CUTDT = '" + d.CUTDT + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += ", @ASSETNAME = '" + d.ASSETNAME + "'";
            sql += ", @OFFOLD = '" + d.OFFOLD + "'";
            sql += " ,@OFFNAMOLD = '" + d.OFFNAMOLD + "'";
            sql += " ,@OFFICECODE = '" + d.OFFICECODE + "'";
            sql += " ,@OFNAME = '" + d.OFNAME + "'";
            sql += " ,@DEPCODE = '" + d.DEPCODE + "'";
            sql += " ,@DEPCODEOL = '" + d.DEPCODEOL + "'";
            sql += " ,@STNAME = '" + d.STNAME + "'";
            sql += " ,@LEADERCODE = '" + d.LEADERCODE + "'";
            sql += " ,@LEADERNAME = '" + d.LEADERNAME + "'";
            sql += " ,@AREANAME = '" + d.AREANAME + "'";
            sql += " ,@AREACODE = '" + d.AREACODE + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@MODE = '" + d.MODE + "'";
            sql += " ,@POSITNAME = '" + d.POSITNAME + "'";
            sql += " ,@ASSETNONEW = '" + d.ASSETNONEW + "'";
            sql += " ,@INPID = '" + d.INPID + "'";

            var res = ExecuteNonQuery(sql, param, conStr);
            return res;

        }

        public int SP_AUDITPOSTTRNPHONE(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "  EXEC  SP_AUDITPOSTTRNPHONE";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@INPID = '" + d.INPID + "'";
            sql += " ,@IMGPATH = '" + d.IMGPATH + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@FLAG = '" + d.FLAG + "'";
            sql += " ,@MEMO= '" + d.MEMO1 + "'";
            sql += " ,@MODE = '" + d.MODE + "'";

            var res = ExecuteNonQuery(sql, param, conStr);
            return res;
        }

        public List<ASAUDITPOSTTRN> getPOSTTRN(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT P.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.stid) as stidname";
            sql += " from  [FT_ASAUDITPOSTTRN_COMPANY](" + QuoteStr(d.COMPANY) + ") as P";           
            sql += " where P.SQNO = " + QuoteStr(d.SQNO);
            sql += " and P.COMPANY = " + QuoteStr(d.COMPANY);

            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).ToList();
            return res;

        }

        public List<ASAUDITPOSTTRN> getPOSTTRNDep(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT P.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME ,ISNULL(D.FLAG_ACCEPT,'') as FLAG_ACCEPT";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.stid) as stidname";
            sql += " from  [FT_ASAUDITPOSTTRN_COMPANY](" + QuoteStr(d.COMPANY) + ") as P";
            sql += " left outer join [FT_ASAUDITCUTDATEMST_COMPANY](" + QuoteStr(d.COMPANY) + ") as D on D.sqno = P.sqno";
            sql += " where P.SQNO = " + QuoteStr(d.SQNO);
            sql += " and P.COMPANY = " + QuoteStr(d.COMPANY);

            sql += " AND  isnull(P.STY,'') = '' ";
            sql += " AND  isnull(P.SNDST,'') = 'Y' ";
            sql += " AND  isnull(P.SNDACC,'') = '' ";

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

            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).ToList();
            return res;

        }

        public List<ASAUDITPOSTTRN> getPOSTTRNComp(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT P.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.stid) as stidname";
            sql += " from  [FT_ASAUDITPOSTTRN_COMPANY](" + QuoteStr(d.COMPANY) + ") as P";
            sql += " where P.SQNO = " + QuoteStr(d.SQNO);
            sql += " and P.COMPANY = " + QuoteStr(d.COMPANY);

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

            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).ToList();
            return res;

        }

        public List<ASAUDITPOSTTRN> getPOSTTRNACC(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT P.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.stid) as stidname";
            sql += " from  [FT_ASAUDITPOSTTRN_COMPANY](" + QuoteStr(d.COMPANY) + ") as P";
            sql += " where P.SQNO = " + QuoteStr(d.SQNO);
            sql += " and P.COMPANY = " + QuoteStr(d.COMPANY);

            //sql += " AND  P.PCODE IN (SELECT  A.PCODE FROM  FT_ASSTProblem() A  WHERE  A.SACC = 'Y' and COMPANY = " + QuoteStr(d.COMPANY) + ") ";

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

            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).ToList();
            return res;

        }

        public List<ASAUDITPOSTTRN> getAuditTrnFIXEDASSET(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  FT_ASAUDITPOSTTRN_ASFIXEDASSET(" + QuoteStr(d.COMPANY) + "," + QuoteStr(d.SQNO) + ") as a ";

            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).ToList();
            return res;
        }




    }
}
