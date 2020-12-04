using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Data.Mssql.Audit;
using ASSETKKF_MODEL.Request.Audit;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Audit
{
    public class AuditResultAdo : Base
    {
        private static AuditResultAdo instant;

        public static AuditResultAdo GetInstant()
        {
            if (instant == null) instant = new AuditResultAdo();
            return instant;
        }


        private AuditResultAdo()
        {
            
        }

        public List<AuditResult> GetData(AuditResultReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " SELECT * FROM [dbo].[FC_AuditResults] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR);
            sql += " ," + QuoteStr(d.MN);
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

            if (!String.IsNullOrEmpty(d.YRMN))
            {
                sql += " and YRMN = " + QuoteStr(d.YRMN);
            }

            var res = Query<AuditResult>(sql, param, conStr).ToList();
            return res;
        }
    }
}
