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
    public class TrackPostMSTAdo : Base
    {
        private static TrackPostMSTAdo instant;
        public static TrackPostMSTAdo GetInstant()
        {
            if (instant == null) instant = new TrackPostMSTAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private TrackPostMSTAdo()
        {
        }

        public List<TrackPostMSTRes> GetData(TrackOfflineReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " select * from TRACKPOSTMST";
            sql += " where 1 = 1";

            if (!String.IsNullOrEmpty(d.company))
            {
                sql += " and company = " + QuoteStr(d.company);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " and sqno = " + QuoteStr(d.sqno);
            }

            if (!String.IsNullOrEmpty(d.inpid))
            {
                sql += " and inpid = " + QuoteStr(d.inpid);
            }

            var res = Query<TrackPostMSTRes>(sql, param).ToList();
            return res;
        }

        public int Insert(TrackOfflineReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.company);
            param.Add("@AUDIT_NO", d.audit_no);
            param.Add("@DEPCODEOL", d.depcodeol);
            param.Add("@INPID", d.inpid);
            param.Add("@INPDT", d.inpdt);
            param.Add("@ASSETNO", d.assetno);
            param.Add("@MEMO1", d.memo1);
            param.Add("@FLAG", d.flag);
            param.Add("@MEMO1", d.memo1);
            param.Add("@TRACKID", d.ucode);

            sql = "insert into TRACKPOSTMST";
            sql += " (COMPANY,AUDIT_NO,DEPCODEOL ";
            sql += " ,INPID,INPDT,ASSETNO,MEMO1,FLAG ";
            sql += " ,TRACKID,TRACKDT)";
            sql += " values (@COMPANY,@AUDIT_NO,@DEPCODEOL ";
            sql += " ,@INPID,@INPDT,@ASSETNO,@MEMO1,@FLAG";
            sql += " ,@TRACKID, GETDATE())";

            var res = ExecuteNonQuery(transac, sql, param);
            return res;

        }

        public int UpdateTransfer(TrackOfflineReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.company);
            param.Add("@AUDIT_NO", d.audit_no);
            param.Add("@INPID", d.inpid);
            param.Add("@ASSETNO", d.assetno);

            param.Add("@UCODE", d.ucode);
            param.Add("@FLAG", d.flag);
            param.Add("@TRANSY", d.transy);
            param.Add("@REMARK", d.remark);

            sql = $"update TRACKPOSTMST set " +
                " SQNO = @SQNO ," +
                " FLAG = @FLAG," +
                " TRACKID = @UCODE," +
                " TRACKDT = GETDATE()," +
                " REMARK = @REMARK" +
                " where COMPANY = @COMPANY"+
                " and AUDIT_NO = @AUDIT_NO" +
                " and INPID = @INPID" +
                " and ASSETNO = @ASSETNO ";

            var res = ExecuteNonQuery(transac, sql, param);
            return res;

        }

        public int UpdateAudit(TrackOfflineReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.company);
            param.Add("@AUDIT_NO", d.audit_no);
            param.Add("@INPID", d.inpid);
            param.Add("@ASSETNO", d.assetno);

            param.Add("@UCODE", d.ucode);
            param.Add("@FLAG", d.flag);
            param.Add("@TRANSY", d.transy);
            param.Add("@REMARK", d.remark);

            sql = $"update TRACKPOSTMST set " +
                " SQNO = @SQNO ," +
                " FLAG = @FLAG," +
                " TRANSY = @TRANSY," +
                " TRANSID = @UCODE," +
                " TRANSDT = GETDATE()," +
                " REMARK = @REMARK" +
                " where COMPANY = @COMPANY" +
                " and AUDIT_NO = @AUDIT_NO" +
                " and INPID = @INPID" +
                " and ASSETNO = @ASSETNO ";

            var res = ExecuteNonQuery(transac, sql, param);
            return res;

        }




    }
}
