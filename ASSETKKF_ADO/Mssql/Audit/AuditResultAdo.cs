using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Data.Mssql.Audit;
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

        private string conectStr { get; set; }

        private AuditResultAdo()
        {
        }

        public List<AuditResult> GetData(TaskAudit d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " SELECT * FROM [dbo].[FC_AuditResults] (";
            sql += " " + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.YR.ToString());
            sql += " ," + QuoteStr(d.MN.ToString());
            sql += " ) where 1 = 1";

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and DEPMST = " + QuoteStr(d.DEPMST);
            }

            if (!String.IsNullOrEmpty(d.SQNO))
            {
                sql += " and SQNO = " + QuoteStr(d.SQNO);
            }

            if (!String.IsNullOrEmpty(d.CUTDT.ToString()))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, m.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.CUTDT.ToString()) + "))";
            }

            var res = Query<AuditResult>(sql, param).ToList();
            return res;
        }
    }
}
