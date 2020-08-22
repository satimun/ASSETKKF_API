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

        public List<TaskAudit> GetData(AsFixedAsset d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " SELECT A.COMPANY ,A.SQNO , A.AUDIT_NO,A.YR,A.MN,A.YRMN,CUTDT";
            sql += " ,MAX(A.DEPMST) as DEPMST,MAX(A.DEPCODEOL) as DEPCODEOL";
            sql += " ,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_WAIT) as QTY_WAIT,sum(QTY_TRN) as QTY_TRN ";
            sql += " , Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end PROGRESS";
            sql += " ,(select  MIN(P.INPDT) from FT_ASAUDITPOSTMST() P where  FLAG  in ('P') and P.SQNO = A.SQNO and P.COMPANY = A.COMPANY ) as STARTDT";
            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.SQNO = A.SQNO and P.COMPANY = A.COMPANY ) as LASTDT";
            sql += "  FROM [dbo].[AuditSummary] ( null ,null) as A";
            sql += "  left join [FT_ASAUDITCUTDATEMST] () as  C on c.COMPANY = A.COMPANY and c.SQNO = A.SQNO";
            sql += " where C.FLAG not in ('C','X')";
            sql += " and C.INPID =" + QuoteStr(d.INPID);
            sql += " group by A.COMPANY, A.SQNO,A.audit_no,A.YR,A.MN,A.YRMN,CUTDT";

            var res = Query<TaskAudit>(sql, param).ToList();
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

        

    }
}
