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
    public class AUDITCUTDATEAdo : Base
    {
        private static AUDITCUTDATEAdo instant;

        public static AUDITCUTDATEAdo GetInstant()
        {
            if (instant == null) instant = new AUDITCUTDATEAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private AUDITCUTDATEAdo()
        {

        }

        public List<ASAUDITCUTDATE> getNoAudit(AuditPostReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT  *    FROM   FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") as C";
            sql += " where C.SQNO = " + QuoteStr(d.SQNO);
            sql += " and C.COMPANY = " + QuoteStr(d.COMPANY);
            sql += " AND  ASSETNO NOT IN  ( SELECT  X.ASSETNO  FROM  FT_ASAUDITPOSTMST() X  ";
            sql += " WHERE X.SQNO =" + QuoteStr(d.SQNO);
            sql += " AND  X.PCODE <> '' )";

            var res = Query<ASAUDITCUTDATE>(sql, param).ToList();
            return res;

        }
    }
}
