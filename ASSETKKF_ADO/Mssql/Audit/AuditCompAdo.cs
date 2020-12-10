using ASSETKKF_MODEL.Data.Mssql.Audit;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Request.Audit;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Audit
{
    public class AuditCompAdo : Base
    {
        private static AuditCompAdo instant;

        public static AuditCompAdo GetInstant()
        {
            if (instant == null) instant = new AuditCompAdo();
            return instant;
        }

        

        private AuditCompAdo()
        {
           
        }

        public List<AuditComp> GetData(AuditResultReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            string USERID = null;
            if ((!d.Menu3 && !d.Menu4))
            {
                USERID = d.INPID;
            }


            sql = " SELECT * FROM [dbo].[FC_AuditComp] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR);
            sql += " ," + QuoteStr(d.MN);
            sql += " ," + QuoteStr(USERID);
            sql += " ," + QuoteStr(d.YRMN);
            sql += " ) where 1 = 1";

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

            var res = Query<AuditComp>(sql, param, conStr).ToList();
            return res;
        }

        public List<AuditComp> getAuditAcc(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " SELECT  SQNO,COUNT(ASSETNO) AS QTY_TOTAL  FROM ( ";
            sql += "  SELECT  SQNO,ASSETNO   FROM     FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.COMPANY) + ")   B, FT_ASSTProblem() A   ";
            sql += " WHERE  A.PCODE = B.PCODE  AND  A.SACC = 'Y'  AND A.company = B.company";
            sql += " UNION ";
            sql += " SELECT  SQNO,ASSETNO   FROM   FT_ASAUDITPOSTTRN_COMPANY(" + QuoteStr(d.COMPANY) + ") B  WHERE  B.SNDACC = 'Y'   ";
            sql += " ) AS X  ";
            sql += " where SQNO = " + QuoteStr(d.SQNO);
            sql += " GROUP BY  SQNO ";
            //sql += " HAVING  COUNT(ASSETNO) = 0 ";

            var res = Query<AuditComp>(sql, param, conStr).ToList();
            return res;
        }

    }
}
