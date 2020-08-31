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

        /// <summary>
        /// deleteBySQNOAssetno
        /// addByAssetnoInpid
        /// updateSNDST
        /// </summary>
        /// <param name="d"></param>
        /// <param name="flag"></param>
        /// <param name="transac"></param>
        /// <returns></returns>
        public int SP_AUDITPOSTMSTTOTEMP(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "  EXEC  SP_AUDITPOSTMSTTOTEMP";
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
