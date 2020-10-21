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
    public class AuditDepAdo : Base
    {
        private static AuditDepAdo instant;

        public static AuditDepAdo GetInstant(string conStr = null)
        {
            if (instant == null) instant = new AuditDepAdo(conStr);
            return instant;
        }

        private string conectStr { get; set; }

        private AuditDepAdo(string conStr = null)
        {
            conectStr = conStr;
        }

        public List<AuditDep> GetData(AuditResultReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            string USERID = null;
            if ((!d.Menu3 && !d.Menu4))
            {
                USERID = d.INPID;
            }


                sql = " SELECT * FROM [dbo].[FC_AuditDep] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR);
            sql += " ," + QuoteStr(d.MN);
            sql += " ," + QuoteStr(USERID);
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

            var res = Query<AuditDep>(sql, param, conectStr).ToList();
            return res;
        }
    }
}
