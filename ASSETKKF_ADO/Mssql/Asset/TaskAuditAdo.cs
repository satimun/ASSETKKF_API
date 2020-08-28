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

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.TaskAudit> GetData(AsFixedAsset d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = @"select COMPANY,SQNO,AUDIT_NO,YR,MN,YRMN,CUTDT 
,DEPMST,MAX(DEPCODEOL) as DEPCODEOL ,STARTDT ,LASTDT 
,sum(QTY_TOTAL) as QTY_TOTAL,sum(QTY_CHECKED) as QTY_CHECKED,sum(QTY_TOTAL) - sum(QTY_CHECKED) as QTY_WAIT,sum(QTY_TRN) as QTY_TRN  
, (Case when sum(QTY_TOTAL) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY_TOTAL) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end ) as PROGRESS  
from(
SELECT m.COMPANY ,m.SQNO , m.AUDIT_NO,m.YR,m.MN,m.YRMN,m.CUTDT 
,MAX(m.DEPMST) as DEPMST,MAX(d.DEPCODEOL) as DEPCODEOL 
,(select  MIN(P.INPDT) from FT_ASAUDITPOSTMST() P where  FLAG  in ('P') and P.SQNO = m.SQNO and P.COMPANY = m.COMPANY ) as STARTDT 
,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.SQNO = m.SQNO and P.COMPANY = m.COMPANY ) as LASTDT  
, ( select COUNT(AssETNO) from FT_ASAUDITCUTDATE() as D1 where SQNO = M.SQNO and DEPCODEOL = D.DEPCODEOL and OFFICECODE = D.OFFICECODE
 and ASSETNO not in (SELECT ASSETNO from [FT_ASAUDITPOSTMST] () where SQNO = D1.SQNO and COMPANY = D1.COMPANY and ASSETNO = D1.ASSETNO and 'Y' = ISNULL(SNDST,''))
) as  QTY_TOTAL
, ( select COUNT(AssETNO) from (select AssETNO from FT_ASAUDITPOSTMST() where SQNO = M.SQNO and DEPCODEOL = D.DEPCODEOL and OFFICECODE = D.OFFICECODE and  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) 
and 'Y' <> ISNULL(SNDST,'') group by ASSETNO
) as P) as  QTY_CHECKED
, ( select COUNT(AssETNO) from FT_ASAUDITPOSTTRN() where SQNO = M.SQNO and COMPANY = m.COMPANY  and YR = m.YR and MN = m.MN  and DEPCODE = D.DEPCODE  and OFFICECODE = D.OFFICECODE
and 'Y' <> ISNULL(SNDST,'') ) as  QTY_TRN
 from  FT_ASAUDITCUTDATE() D
left join FT_ASAUDITCUTDATEMST() as  M on m.COMPANY = d.COMPANY and m.SQNO = d.SQNO  
left join FT_ASAUDITPOSTMST() as  P on P.COMPANY = m.COMPANY and P.SQNO = m.SQNO
where m.FLAG not in ('C','X') and ( P.INPID =" + QuoteStr(d.INPID);
sql += "or  M.INPID =" + QuoteStr(d.INPID) + ")";
sql += @" group by m.COMPANY, m.SQNO,m.audit_no,m.YR,m.MN,m.YRMN,m.CUTDT ,d.OFFICECODE,D.DEPCODEOL,D.DEPCODE
) as z
group by  COMPANY,SQNO,AUDIT_NO,YR,MN,YRMN,CUTDT 
,DEPMST ,STARTDT ,LASTDT ";

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

        

    }
}
