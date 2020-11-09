using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Audit;
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
        public int SP_AUDITPOSTMSTTOTEMP(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
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
            sql += " ,@SNNSTDT= '" + d.snnstdt + "'";
            sql += " ,@EXPSTDT= '" + d.expstdt + "'";
            sql += " ,@POTH= '" + d.poth + "'";
            sql += " ,@MEMO1= '" + d.MEMO1 + "'";
            var res = ExecuteNonQuery(sql, param, conStr); 
            return res;
        }

        public List<ASAUDITPOSTMSTTOTEMP> getDataToSendDep(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT  B.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= B.INPID) as INPNAME  ";
            sql += " ,(SELECT SACC FROM [dbo].[FT_ASSTProblem] () where COMPANY = B.COMPANY and PCODE = B.PCODE) as SACC";
            sql += " FROM  FT_ASAUDITPOSTMSTTOTEMP_COMPANY(" + QuoteStr(d.COMPANY) + ")  B";
            sql += " where B.SQNO = " + QuoteStr(d.SQNO);
            sql += " and B.COMPANY = " + QuoteStr(d.COMPANY);

           /* if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and ASSETNO = '" + d.ASSETNO + "'";
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and YR = '" + d.YEAR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and MN = '" + d.MN + "'";
            }
            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                sql += " and DEPCODEOL in (SELECT [DEPCODEOL] ";
                sql += " FROM FT_ASAUDITCUTDATE() ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
                sql += " group by[DEPCODEOL])";
            }

            if (!String.IsNullOrEmpty(d.cutdt))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and OFFICECODE = '" + d.OFFICECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and TYPECODE = '" + d.TYPECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and GASTCODE = '" + d.GASTCODE + "'";
            }*/

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby != null && d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby != null && d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var res = Query<ASAUDITPOSTMSTTOTEMP>(sql, param, conStr).ToList();
            return res;
        }

        public List<ASAUDITPOSTMSTTOTEMP> getAuditAssetNo(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  FT_ASAUDITPOSTMSTTOTEMP_COMPANY(" + QuoteStr(d.COMPANY) + ") as a ";
            sql += " left outer join [FT_ASAUDITPOSTMST_PHONE] () as b";
            sql += " on b.SQNO = a.SQNO and a.COMPANY = b.COMPANY and b.ASSETNO = a.ASSETNO   and (a.INPID = b.INPID or b.inpdt is not null)";
            sql += " where a.SQNO = '" + d.SQNO + "'";
            sql += " and a.COMPANY = '" + d.COMPANY + "'";
            sql += " and a.ASSETNO = '" + d.ASSETNO + "'";
            //sql += " and a.INPID = '" + d.UCODE + "'";

            var res = Query<ASAUDITPOSTMSTTOTEMP>(sql, param, conStr).ToList();
            return res;
        }



        public List<AuditTmpCompareTRN> getAuditTmpComparetoTRN(AuditPostReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  FT_AUDITTMPCOMPARETOTRN(" + QuoteStr(d.COMPANY) + "," + QuoteStr(d.SQNO) + ") as a ";

            var res = Query<AuditTmpCompareTRN>(sql, param, conStr).ToList();
            return res;
        }

    }
}
