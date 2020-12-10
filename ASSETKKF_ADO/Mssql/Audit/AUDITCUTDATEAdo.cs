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

        

        private AUDITCUTDATEAdo()
        {
           
        }

        public List<ASAUDITCUTDATE> getNoAudit(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT  *    FROM   FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") as C";
            sql += " where C.SQNO = " + QuoteStr(d.SQNO);
            sql += " and C.COMPANY = " + QuoteStr(d.COMPANY);
            sql += " AND  ASSETNO NOT IN  ( SELECT  X.ASSETNO  FROM  FT_ASAUDITPOSTMST_COMPANY(" + QuoteStr(d.COMPANY) + ") X  ";
            sql += " WHERE X.SQNO =" + QuoteStr(d.SQNO);
            sql += " AND  X.PCODE <> '' )";
            sql += " AND  ASSETNO NOT IN  ( SELECT  Y.ASSETNO  FROM  FT_ASAUDITPOSTMSTTOTEMP_COMPANY(" + QuoteStr(d.COMPANY) + ") Y  ";
            sql += " WHERE Y.SQNO =" + QuoteStr(d.SQNO);
            sql += " AND  Y.PCODE <> '' )";

            var res = Query<ASAUDITCUTDATE>(sql, param, conStr).ToList();
            return res;

        }

       
    }
}
