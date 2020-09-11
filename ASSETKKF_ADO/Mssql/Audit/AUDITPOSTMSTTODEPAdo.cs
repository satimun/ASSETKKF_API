using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using ASSETKKF_MODEL.Request.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Audit
{
    public class AUDITPOSTMSTTODEPAdo : Base
    {
        private static AUDITPOSTMSTTODEPAdo instant;

        public static AUDITPOSTMSTTODEPAdo GetInstant()
        {
            if (instant == null) instant = new AUDITPOSTMSTTODEPAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private AUDITPOSTMSTTODEPAdo()
        {

        }

        public int SP_AUDITPOSTMSTTODEP(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "  EXEC  SP_AUDITPOSTMSTTODEP";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@MODE = '" + d.MODE + "'";
            sql += " ,@FINDY = '" + d.FINDY + "'";
            sql += " ,@PCODE= '" + d.PCODE + "'";
            sql += " ,@PNAME= '" + d.PNAME + "'";
            var res = ExecuteNonQuery(sql, param);
            return res;
        }


    }
}
