using ASSETKKF_MODEL.Data.Mssql.Audit;
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

        private string conectStr { get; set; }

        private AuditCompAdo()
        {
        }

        public List<AuditComp> GetData(AuditResultReq d, SqlTransaction transac = null)
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

            var res = Query<AuditComp>(sql, param).ToList();
            return res;
        }
    }
}
