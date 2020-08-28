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
    public class AUDITPOSTMSTAdo : Base
    {
        private static AUDITPOSTMSTAdo instant;

        public static AUDITPOSTMSTAdo GetInstant()
        {
            if (instant == null) instant = new AUDITPOSTMSTAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private AUDITPOSTMSTAdo()
        {

        }

        public List<ASAUDITPOSTMST> getPOSTMSTDuplicate(AuditPostReq d, string flag = null, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT * from  [FT_ASAUDITPOSTMST] () as P";
            sql += " left outer join  FT_ASAUDITCUTDATEMST() M on M.SQNO = P.SQNO and M.COMPANY = P.COMPANY";            
            sql += " where SQNO = " + QuoteStr(d.SQNO);
            sql += " and COMPANY = " + QuoteStr(d.COMPANY );
            sql += "  AND  PCODE <> '' AND P.ASSETNO IN ( SELECT  X.ASSETNO  FROM  [FT_ASAUDITPOSTMST] () X  WHERE  X.PCODE <> '' ";
            sql += " and  M.FLAG not in ('X','C')";
            sql += "  AND  X.SQNO = " + QuoteStr(d.SQNO);
            sql += "  and x.COMPANY = " + QuoteStr(d.COMPANY);
            sql += " GROUP BY  X.ASSETNO  HAVING  COUNT(X.ASSETNO) >1  )";

            if(!String.IsNullOrEmpty(d.DEPCODEOL))
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
            }

            if (String.IsNullOrEmpty(d.orderby) || d.orderby.Equals("1"))
            {
                sql += " order by  ASSETNO,OFFICECODE ";
            }

            if (d.orderby.Equals("2"))
            {
                sql += " order by  OFFICECODE,ASSETNO ";
            }

            if (d.orderby.Equals("3"))
            {
                sql += " order by  DEPCODEOL,OFFICECODE,ASSETNO ";
            }

            if (d.orderby.Equals("4"))
            {
                sql += " order by  POSITCODE,OFFICECODE,ASSETNO ";
            }

            var res = Query<ASAUDITPOSTMST>(sql, param).ToList();
            return res;

        }

    }
}
