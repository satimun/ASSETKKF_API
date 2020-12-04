using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Request.Audit;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Audit
{
   public  class AuditManagerAdo : Base
    {
        private static AuditManagerAdo instant;

        public static AuditManagerAdo GetInstant()
        {
            if (instant == null) instant = new AuditManagerAdo();
            return instant;
        }

        

        private AuditManagerAdo()
        {
            
        }

        public List<AuditManager> GetData2Send(AuditResultReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            string USERID = d.INPID;
            

            sql = " SELECT C.* ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.STID) as STNAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR1ID) as MGR1NAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR2ID) as MGR2NAME";
            sql += " FROM [dbo].[FC_AuditManager] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR);
            sql += " ," + QuoteStr(d.MN);
            sql += " ," + QuoteStr(USERID);
            sql += " ," + QuoteStr(d.YRMN);
            sql += " ) as C where 1 = 1";

            sql += "  and ISNULL(FLAG_ACCEPT,'') in ('','0') ";

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and DEPMST = " + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.SQNO))
            {
                sql += " and SQNO = " + QuoteStr(d.SQNO);
            }

            if (!String.IsNullOrEmpty(d.CUTDT))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.CUTDT) + "))";


            }

            var res = Query<AuditManager>(sql, param, conStr).ToList();
            return res;
        }

        public List<AuditManager> GetData2MGR1(AuditResultReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            string USERID = d.INPID;


            sql = " SELECT C.* ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.STID) as STNAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR1ID) as MGR1NAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR2ID) as MGR2NAME";
            sql += " FROM [dbo].[FC_AuditManager] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR);
            sql += " ," + QuoteStr(d.MN);
            sql += " ," + QuoteStr(USERID);
            sql += " ," + QuoteStr(d.YRMN);
            sql += " ) as C where 1 = 1";

            sql += "  and ISNULL(FLAG_ACCEPT,'') in ('1') ";
            sql += " and ISNULL(STY,'') = 'Y' ";

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and DEPMST = " + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.SQNO))
            {
                sql += " and SQNO = " + QuoteStr(d.SQNO);
            }

            if (!String.IsNullOrEmpty(d.CUTDT))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.CUTDT) + "))";


            }

            var res = Query<AuditManager>(sql, param, conStr).ToList();
            return res;
        }

        public List<AuditManager> GetData2MGR2(AuditResultReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            string USERID = d.INPID;


            sql = " SELECT C.* ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.STID) as STNAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR1ID) as MGR1NAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR2ID) as MGR2NAME";
            sql += " FROM [dbo].[FC_AuditManager] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR);
            sql += " ," + QuoteStr(d.MN);
            sql += " ," + QuoteStr(USERID);
            sql += " ," + QuoteStr(d.YRMN);
            sql += " ) as C where 1 = 1";

            sql += "  and ISNULL(FLAG_ACCEPT,'') in ('2') ";
            sql += " and ISNULL(MGR1Y,'') = 'Y' ";

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and DEPMST = " + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.SQNO))
            {
                sql += " and SQNO = " + QuoteStr(d.SQNO);
            }

            if (!String.IsNullOrEmpty(d.CUTDT))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.CUTDT) + "))";


            }

            var res = Query<AuditManager>(sql, param, conStr).ToList();
            return res;
        }

        public int saveAUDITCUTDATEMST(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [dbo].[SP_AUDITCUTDATEMST]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@MODE = '" + d.mode + "'";
            sql += " ,@FLAG = '" + d.FLAG + "'";


            var res = ExecuteNonQuery(sql, param, conStr);
            return res;
        }

        public SummaryAudit GetSummaryAudit(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = @"select *
, (Case when QTY_ASSET > 0 then CAST(((CAST(QTY_AUDIT as DECIMAL(9,2)) /CAST(QTY_ASSET as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end ) as PROGRESS  
 from (
SELECT COUNT(OFFICECODE) AS QTY_HUMAN,SUM(SASSET1)  AS QTY_ASSET,SUM(SAUDIT1) AS QTY_AUDIT  
FROM ( SELECT MN,YR,OFFICECODE,COUNT(ASSETNO) AS SASSET1,SUM(CASE WHEN  isnull(PCODE,'') = '' THEN 0 ELSE 1 END) AS SAUDIT1    
FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) +")  B ";

            sql += " WHERE   SQNO = " + QuoteStr(d.SQNO);
            sql += " and COMPANY =" + QuoteStr(d.COMPANY);
            sql += " and    SNDST = 'Y'   AND   SNDACCDT IS NULL";
            sql += " GROUP BY MN,YR,OFFICECODE   )  AS X GROUP BY  MN,YR 	) as Z ";

            var res = Query<SummaryAudit>(sql, param, conStr).FirstOrDefault();
            return res;
        }

        public List<SummaryResult> GetSummaryResult(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = @" SELECT PCODE,PNAME,DEPCODE,DEPCODEOL,MAX(STNAME) AS STNAME,COUNT(PCODE) AS QTY  FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")   B ";
            sql += " WHERE   SQNO = " + QuoteStr(d.SQNO);
            sql += " and COMPANY =" + QuoteStr(d.COMPANY);
            sql += " and    PCODE <> ''    AND  SNDST = 'Y'  AND  SNDACCDT IS NULL ";
            sql += " GROUP BY MN,YR,PCODE,PNAME,DEPCODE,DEPCODEOL  ";
            sql += " order BY DEPCODE,DEPCODEOL, PCODE,PNAME  ";

            var res = Query<SummaryResult>(sql, param, conStr).ToList();
            return res;

        }

    }
}
