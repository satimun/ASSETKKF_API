using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Request.Track;
using ASSETKKF_MODEL.Response.Track;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Track
{
    public class TrackHDAdo : Base
    {
        private static TrackHDAdo instant;
        public static TrackHDAdo GetInstant(string conStr = null)
        {
            if (instant == null) instant = new TrackHDAdo(conStr);
            return instant;
        }

        private string conectStr { get; set; }

        private TrackHDAdo(string conStr = null)
        {
            conectStr = conStr;
        }

        public List<TrackHDRes> GetData(TrackOfflineReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = $"select * from (" +
                " select COMPANY, AUDIT_NO, INPID" +
                " FROM[assetkkf].[dbo].[TRACKPOSTMST]" +
                " UNION ALL" +
                " select COMPANY, AUDIT_NO, INPID" +
                " FROM[assetkkf].[dbo].[TRACKPOSTTRN]" +
                " ) as Z";
                
            sql += " where 1 = 1";

            if (!String.IsNullOrEmpty(d.company))
            {
                sql += " and company = " + QuoteStr(d.company);
            }            

            if (!String.IsNullOrEmpty(d.audit_no))
            {
                sql += " and audit_no = " + QuoteStr(d.audit_no);
            }

            if (!String.IsNullOrEmpty(d.inpid))
            {
                sql += " and inpid = " + QuoteStr(d.inpid);
            }

            sql += " group by COMPANY, AUDIT_NO, INPID ";

            var res = Query<TrackHDRes>(sql, param, conectStr).ToList();
            return res;
        }


    }
}
