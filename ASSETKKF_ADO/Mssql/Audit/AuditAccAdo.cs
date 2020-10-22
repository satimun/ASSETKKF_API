using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Request.Audit;
using ASSETKKF_MODEL.Response.Audit;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Audit
{
    public  class AuditAccAdo : Base
    {
        private static AuditAccAdo instant;

        public static AuditAccAdo GetInstant()
        {
            if (instant == null) instant = new AuditAccAdo();
            return instant;
        }

        

        private AuditAccAdo()
        {
            
        }

        public List<AuditAcc> GetData(AuditResultReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            string USERID = d.INPID;


            sql = " SELECT C.* ";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.STID) as STNAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR1ID) as MGR1NAME";
            sql += " ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= C.MGR2ID) as MGR2NAME";
            sql += " FROM [dbo].[FC_AuditACC] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR);
            sql += " ," + QuoteStr(d.MN);
            sql += " ," + QuoteStr(USERID);
            sql += " ) as C where 1 = 1";


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

            var res = Query<AuditAcc>(sql, param, conStr).ToList();
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
FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")  B ";

            sql += " WHERE   SQNO = " + QuoteStr(d.SQNO);
            sql += " and COMPANY =" + QuoteStr(d.COMPANY);
            sql += " AND   SNDACCDT IS NULL";
            sql += " AND  B.PCODE IN (SELECT  A.PCODE FROM  FT_ASSTProblem() A  WHERE  isnull(A.SACC,'') = 'Y' and COMPANY = " + QuoteStr(d.COMPANY) + ") ";
            sql += " GROUP BY MN,YR,OFFICECODE   )  AS X GROUP BY  MN,YR 	) as Z ";

            var res = Query<SummaryAudit>(sql, param, conStr).FirstOrDefault();
            return res;
        }

        public List<SummaryResult> GetSummaryResult(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = @" SELECT PCODE,PNAME,DEPCODE,DEPCODEOL,MAX(STNAME) AS STNAME,COUNT(PCODE) AS QTY  FROM    FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")   B ";
            sql += " WHERE   SQNO = " + QuoteStr(d.SQNO);
            sql += " and COMPANY =" + QuoteStr(d.COMPANY);
            sql += " AND  SNDACCDT IS NULL ";
            sql += " AND  B.PCODE IN (SELECT  A.PCODE FROM  FT_ASSTProblem() A  WHERE  A.SACC = 'Y' and COMPANY = " + QuoteStr(d.COMPANY) + ") ";
            sql += " GROUP BY MN,YR,PCODE,PNAME,DEPCODE,DEPCODEOL  ";
            sql += " order BY DEPCODE,DEPCODEOL, PCODE,PNAME  ";

            var res = Query<SummaryResult>(sql, param, conStr).ToList();
            return res;

        }


    }
}
