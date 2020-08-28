using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ASSETKKF_MODEL.Request.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Audit
{
    public class AUDITPOSTMSTTOTEMPAdo : Base
    {
        private static AUDITPOSTMSTTOTEMPAdo instant;

        public static AUDITPOSTMSTTOTEMPAdo GetInstant()
        {
            if (instant == null) instant = new AUDITPOSTMSTTOTEMPAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private AUDITPOSTMSTTOTEMPAdo()
        {

        }

        public int deleteByAssetno(AuditPostReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "  EXEC  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            var res = ExecuteNonQuery(sql, param);
            return res;
        }
    }
}
