using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response.Home;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class TaskAuditAdo : Base
    {
        private static TaskAuditAdo instant;

        public static TaskAuditAdo GetInstant()
        {
            if (instant == null) instant = new TaskAuditAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private TaskAuditAdo()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.TaskAudit> GetData(TaskAudit d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " SELECT * FROM [dbo].[FC_Taskuser] (" + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.INPID);
            sql += " ) where 1 = 1";

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and COMPANY = '" + d.COMPANY + "'";
            }

            if (!String.IsNullOrEmpty(d.YR.ToString()))
            {
                sql += " and YR = '" + d.YR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN.ToString()))
            {
                sql += " and MN = '" + d.MN + "'";
            }

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.TaskAudit>(sql, param).ToList();
            return res;
        }

        public int AuditCancel(AsFixedAsset d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [SP_AUDITCANCEL]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@USERID = '" + d.INPID + "'";


            var res = ExecuteNonQuery(sql, param);
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.TaskAudit> GetTracking(TaskAudit d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " SELECT * FROM [dbo].[FC_TrackingAudit] (" + QuoteStr(d.COMPANY);
            sql += " ," + QuoteStr(d.INPID);
            sql += " )  where 1 = 1";

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and COMPANY = '" + d.COMPANY + "'";
            }

            if (!String.IsNullOrEmpty(d.YR.ToString()))
            {
                sql += " and YR = '" + d.YR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN.ToString()))
            {
                sql += " and MN = '" + d.MN + "'";
            }

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.TaskAudit>(sql, param).ToList();
            return res;
        }

    }
}
