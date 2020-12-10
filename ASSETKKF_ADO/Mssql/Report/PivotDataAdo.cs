using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Report;
using Dapper;
using Newtonsoft.Json;

namespace ASSETKKF_ADO.Mssql.Report
{
    public class PivotDataAdo : Base
    {
        private static PivotDataAdo instant;
        public static PivotDataAdo GetInstant()
        {
            if (instant == null) instant = new PivotDataAdo();
            return instant;
        }

        

        private PivotDataAdo()
        {
            
        }

        public DataTable getProblemByDep(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            string mn = "";
            string yrmn = "";

            if (!String.IsNullOrEmpty(d.mn))
            {
                mn = d.mn;
            }

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                yrmn = d.yrmn;
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.Company);
            param.Add("@YEAR", d.year);
            param.Add("@MN", mn);
            param.Add("@YRMN", yrmn);

            var obs = QuerySP<dynamic>("SP_ProblemByDep",param, conStr);

            //IList<dynamic> data = obs.ToList();


            var dt = ToDataTable(obs);                        

            return dt;
        }

        public DataTable getProblemByDepcodeol(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            string mn = "";
            string yrmn = "";

            if (!String.IsNullOrEmpty(d.mn))
            {
                mn = d.mn;
            }

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                yrmn = d.yrmn;
            }

            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.Company);
            param.Add("@YEAR", d.year);
            param.Add("@DEPMST", d.depmst);
            param.Add("@MN", mn);
            param.Add("@YRMN", yrmn);

            var obs = QuerySP<dynamic>("SP_ProblemByDepcodeol", param, conStr);

            //IList<dynamic> data = obs.ToList();


            var dt = ToDataTable(obs);

            return dt;
        }

        public List<Quantity> getQuantityByDep(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select max (QTY_ASSET) QTY_ASSET, max (QTY_AUDIT) QTY_AUDIT from (";
            sql += " SELECT SUM(SASSET1)  AS QTY_ASSET , null AS QTY_AUDIT ";
            sql += " FROM ( SELECT COUNT(ASSETNO) AS SASSET1";
            sql += " FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.Company) + ")  B ";
            sql += " WHERE   DEPMST = " + QuoteStr(d.depmst)  ;
            sql += " and YR = " + QuoteStr(d.year);
            sql += " and MN =  case when ISNULL(" + QuoteStr(d.mn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.mn) + ",'') else MN end ";
            sql += " and YRMN =  case when ISNULL(" + QuoteStr(d.yrmn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.yrmn) + ",'') else YRMN end ";
            sql += "  )  AS X";
            sql += " union ";
            sql += " SELECT null AS QTY_ASSET, SUM(SAUDIT1) AS QTY_AUDIT";
            sql += " FROM ( SELECT SUM(CASE WHEN  isnull(PCODE,'') = '' THEN 0 ELSE 1 END) AS SAUDIT1";
            sql += " FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.Company) + ")  B ";
            sql += " WHERE   DEPMST = " + QuoteStr(d.depmst);
            sql += " and YR = " + QuoteStr(d.year);
            sql += " and MN =  case when ISNULL(" + QuoteStr(d.mn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.mn) + ",'') else MN end ";
            sql += " and YRMN =  case when ISNULL(" + QuoteStr(d.yrmn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.yrmn) + ",'') else YRMN end ";
            sql += "  )  AS Y";
            sql += "  ) as Z";

            var res = Query<Quantity>(sql, param, conStr).ToList();
            return res;
        }

        public List<Quantity> getQuantityByDEPCODEOL(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select max (QTY_ASSET) QTY_ASSET, max (QTY_AUDIT) QTY_AUDIT from (";
            sql += " SELECT SUM(SASSET1)  AS QTY_ASSET , null AS QTY_AUDIT ";
            sql += " FROM ( SELECT COUNT(ASSETNO) AS SASSET1";
            sql += " FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.Company) + ")  B ";
            sql += " WHERE   DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            sql += " and YR = " + QuoteStr(d.year);
            sql += " and MN =  case when ISNULL(" + QuoteStr(d.mn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.mn) + ",'') else MN end ";
            sql += " and YRMN =  case when ISNULL(" + QuoteStr(d.yrmn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.yrmn) + ",'') else YRMN end ";
            sql += "  )  AS X";
            sql += " union ";
            sql += " SELECT null AS QTY_ASSET, SUM(SAUDIT1) AS QTY_AUDIT";
            sql += " FROM ( SELECT SUM(CASE WHEN  isnull(PCODE,'') = '' THEN 0 ELSE 1 END) AS SAUDIT1";
            sql += " FROM  FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.Company) + ")  B ";
            sql += " WHERE   DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            sql += " and YR = " + QuoteStr(d.year);
            sql += " and MN =  case when ISNULL(" + QuoteStr(d.mn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.mn) + ",'') else MN end ";
            sql += " and YRMN =  case when ISNULL(" + QuoteStr(d.yrmn) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.yrmn) + ",'') else YRMN end ";
            sql += "  )  AS Y";
            sql += "  ) as Z";

            var res = Query<Quantity>(sql, param, conStr).ToList();
            return res;
        }

    }
}
