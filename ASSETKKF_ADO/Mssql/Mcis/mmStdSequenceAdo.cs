using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class mmStdSequenceAdo : Base
    {
        private static mmStdSequenceAdo instant;

        public static mmStdSequenceAdo GetInstant()
        {
            if (instant == null) instant = new mmStdSequenceAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private mmStdSequenceAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.mmStdSequence ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence> GetData(ASSETKKF_MODEL.Request.Mcis.mmStdSequenceReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd);
            /*
            param.Add("@SeqNo", d.SeqNo);
            param.Add("@WorkStationGrp", d.WorkStationGrp);
            param.Add("@TaskCode", d.TaskCode);
            param.Add("@WorkCenterCode", d.WorkCenterCode);
            param.Add("@MachCode", d.MachCode);
            param.Add("@SetupTime", d.SetupTime);
            param.Add("@NoOfMins", d.NoOfMins);
            param.Add("@ProductionRate", d.ProductionRate);
            param.Add("@UnitCode", d.UnitCode);
            param.Add("@TimeCode", d.TimeCode);
            param.Add("@CycleTime", d.CycleTime);
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@LastUpdUser", d.LastUpdUser);
            param.Add("@SECM_NO", d.SECM_NO);
            param.Add("@Tm", d.Tm);
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@RoutingDateSt", d.RoutingDateSt);
            param.Add("@RoutingDateEnd", d.RoutingDateEnd);
            param.Add("@NewFlag", d.NewFlag);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */
            string cmd = "SELECT * FROM mcis.dbo.mmStdSequence " +
            $"WHERE  ISNULL(DrawingCd,'') = ISNULL(@DrawingCd,'')  " +
         
            /*$"  AND (@SeqNo IS NULL OR SeqNo = @SeqNo) " +
            $"  AND (@WorkStationGrp IS NULL OR WorkStationGrp = @WorkStationGrp) " +
            $"  AND (@TaskCode IS NULL OR TaskCode = @TaskCode) " +
            $"  AND (@WorkCenterCode IS NULL OR WorkCenterCode = @WorkCenterCode) " +
            $"  AND (@MachCode IS NULL OR MachCode = @MachCode) " +
            $"  AND (@SetupTime IS NULL OR SetupTime = @SetupTime) " +
            $"  AND (@NoOfMins IS NULL OR NoOfMins = @NoOfMins) " +
            $"  AND (@ProductionRate IS NULL OR ProductionRate = @ProductionRate) " +
            $"  AND (@UnitCode IS NULL OR UnitCode = @UnitCode) " +
            $"  AND (@TimeCode IS NULL OR TimeCode = @TimeCode) " +
            $"  AND (@CycleTime IS NULL OR CycleTime = @CycleTime) " +
            $"  AND (@LastUpdDt IS NULL OR LastUpdDt = @LastUpdDt) " +
            $"  AND (@LastUpdUser IS NULL OR LastUpdUser = @LastUpdUser) " +
            $"  AND (@SECM_NO IS NULL OR SECM_NO = @SECM_NO) " +
            $"  AND (@Tm IS NULL OR Tm = @Tm) " +
            $"  AND (@USER_ID IS NULL OR USER_ID = @USER_ID) " +
            $"  AND (@USER_DATE IS NULL OR USER_DATE = @USER_DATE) " +
            $"  AND (@RoutingDateSt IS NULL OR RoutingDateSt = @RoutingDateSt) " +
            $"  AND (@RoutingDateEnd IS NULL OR RoutingDateEnd = @RoutingDateEnd) " +
            $"  AND (@NewFlag IS NULL OR NewFlag = @NewFlag) " +*/
            //$"AND (DrawingCd LIKE @txtSearch OR DrawingCd LIKE @txtSearch) " +  
             "ORDER BY  DrawingCd;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence> Search(ASSETKKF_MODEL.Request.Mcis.mmStdSequenceReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@DrawingCdIsNull", d.DrawingCd.ListNull()); 
            param.Add("@SeqNoIsNull", d.SeqNo.ListNull()); 
            param.Add("@WorkStationGrpIsNull", d.WorkStationGrp.ListNull()); 
            param.Add("@TaskCodeIsNull", d.TaskCode.ListNull()); 
            param.Add("@WorkCenterCodeIsNull", d.WorkCenterCode.ListNull()); 
            param.Add("@MachCodeIsNull", d.MachCode.ListNull()); 
            param.Add("@SetupTimeIsNull", d.SetupTime.ListNull()); 
            param.Add("@NoOfMinsIsNull", d.NoOfMins.ListNull()); 
            param.Add("@ProductionRateIsNull", d.ProductionRate.ListNull()); 
            param.Add("@UnitCodeIsNull", d.UnitCode.ListNull()); 
            param.Add("@TimeCodeIsNull", d.TimeCode.ListNull()); 
            param.Add("@CycleTimeIsNull", d.CycleTime.ListNull()); 
            param.Add("@LastUpdDtIsNull", d.LastUpdDt.ListNull()); 
            param.Add("@LastUpdUserIsNull", d.LastUpdUser.ListNull()); 
            param.Add("@SECM_NOIsNull", d.SECM_NO.ListNull()); 
            param.Add("@TmIsNull", d.Tm.ListNull()); 
            param.Add("@USER_IDIsNull", d.USER_ID.ListNull()); 
            param.Add("@USER_DATEIsNull", d.USER_DATE.ListNull()); 
            param.Add("@RoutingDateStIsNull", d.RoutingDateSt.ListNull()); 
            param.Add("@RoutingDateEndIsNull", d.RoutingDateEnd.ListNull()); 
            param.Add("@NewFlagIsNull", d.NewFlag.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.mmStdSequence " +
            $"WHERE (@DrawingCdIsNull IS NULL OR DrawingCd IN ('{ d.DrawingCd.Join("','") }')) " +
            $"AND (@SeqNoIsNull IS NULL OR SeqNo IN ('{ d.SeqNo.Join("','") }')) " +
            $"AND (@WorkStationGrpIsNull IS NULL OR WorkStationGrp IN ('{ d.WorkStationGrp.Join("','") }')) " +
            $"AND (@TaskCodeIsNull IS NULL OR TaskCode IN ('{ d.TaskCode.Join("','") }')) " +
            $"AND (@WorkCenterCodeIsNull IS NULL OR WorkCenterCode IN ('{ d.WorkCenterCode.Join("','") }')) " +
            $"AND (@MachCodeIsNull IS NULL OR MachCode IN ('{ d.MachCode.Join("','") }')) " +
            $"AND (@SetupTimeIsNull IS NULL OR SetupTime IN ('{ d.SetupTime.Join("','") }')) " +
            $"AND (@NoOfMinsIsNull IS NULL OR NoOfMins IN ('{ d.NoOfMins.Join("','") }')) " +
            $"AND (@ProductionRateIsNull IS NULL OR ProductionRate IN ('{ d.ProductionRate.Join("','") }')) " +
            $"AND (@UnitCodeIsNull IS NULL OR UnitCode IN ('{ d.UnitCode.Join("','") }')) " +
            $"AND (@TimeCodeIsNull IS NULL OR TimeCode IN ('{ d.TimeCode.Join("','") }')) " +
            $"AND (@CycleTimeIsNull IS NULL OR CycleTime IN ('{ d.CycleTime.Join("','") }')) " +
            $"AND (@LastUpdDtIsNull IS NULL OR LastUpdDt IN ('{ d.LastUpdDt.Join("','") }')) " +
            $"AND (@LastUpdUserIsNull IS NULL OR LastUpdUser IN ('{ d.LastUpdUser.Join("','") }')) " +
            $"AND (@SECM_NOIsNull IS NULL OR SECM_NO IN ('{ d.SECM_NO.Join("','") }')) " +
            $"AND (@TmIsNull IS NULL OR Tm IN ('{ d.Tm.Join("','") }')) " +
            $"AND (@USER_IDIsNull IS NULL OR USER_ID IN ('{ d.USER_ID.Join("','") }')) " +
            $"AND (@USER_DATEIsNull IS NULL OR USER_DATE IN ('{ d.USER_DATE.Join("','") }')) " +
            $"AND (@RoutingDateStIsNull IS NULL OR RoutingDateSt IN ('{ d.RoutingDateSt.Join("','") }')) " +
            $"AND (@RoutingDateEndIsNull IS NULL OR RoutingDateEnd IN ('{ d.RoutingDateEnd.Join("','") }')) " +
            $"AND (@NewFlagIsNull IS NULL OR NewFlag IN ('{ d.NewFlag.Join("','") }')) " +
            $"AND (DrawingCd LIKE @txtSearch OR DrawingCd LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@SeqNo", d.SeqNo);
            param.Add("@WorkStationGrp", d.WorkStationGrp);
            param.Add("@TaskCode", d.TaskCode);
            param.Add("@WorkCenterCode", d.WorkCenterCode);
            param.Add("@MachCode", d.MachCode);
            param.Add("@SetupTime", d.SetupTime);
            param.Add("@NoOfMins", d.NoOfMins);
            param.Add("@ProductionRate", d.ProductionRate);
            param.Add("@UnitCode", d.UnitCode);
            param.Add("@TimeCode", d.TimeCode);
            param.Add("@CycleTime", d.CycleTime);
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@LastUpdUser", d.LastUpdUser);
            param.Add("@SECM_NO", d.SECM_NO);
            param.Add("@Tm", d.Tm);
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@RoutingDateSt", d.RoutingDateSt);
            param.Add("@RoutingDateEnd", d.RoutingDateEnd);
            param.Add("@NewFlag", d.NewFlag);
            string cmd = "INSERT INTO mcis.dbo.mmStdSequence " +
            $"      (DrawingCd, SeqNo, WorkStationGrp, TaskCode, WorkCenterCode, MachCode, SetupTime, NoOfMins, ProductionRate, UnitCode, TimeCode, CycleTime, LastUpdDt, LastUpdUser, SECM_NO, Tm, USER_ID, USER_DATE, RoutingDateSt, RoutingDateEnd, NewFlag) " +
            $"VALUES(@DrawingCd, @SeqNo, @WorkStationGrp, @TaskCode, @WorkCenterCode, @MachCode, @SetupTime, @NoOfMins, @ProductionRate, @UnitCode, @TimeCode, @CycleTime, @LastUpdDt, @LastUpdUser, @SECM_NO, @Tm, @USER_ID, @USER_DATE, @RoutingDateSt, @RoutingDateEnd, @NewFlag); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@SeqNo", d.SeqNo.GetValue());
            param.Add("@WorkStationGrp", d.WorkStationGrp.GetValue());
            param.Add("@TaskCode", d.TaskCode.GetValue());
            param.Add("@WorkCenterCode", d.WorkCenterCode.GetValue());
            param.Add("@MachCode", d.MachCode.GetValue());
            param.Add("@SetupTime", d.SetupTime);
            param.Add("@NoOfMins", d.NoOfMins);
            param.Add("@ProductionRate", d.ProductionRate);
            param.Add("@UnitCode", d.UnitCode.GetValue());
            param.Add("@TimeCode", d.TimeCode.GetValue());
            param.Add("@CycleTime", d.CycleTime);
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@LastUpdUser", d.LastUpdUser.GetValue());
            param.Add("@SECM_NO", d.SECM_NO.GetValue());
            param.Add("@Tm", d.Tm.GetValue());
            param.Add("@USER_ID", d.USER_ID.GetValue());
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@RoutingDateSt", d.RoutingDateSt);
            param.Add("@RoutingDateEnd", d.RoutingDateEnd);
            param.Add("@NewFlag", d.NewFlag.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.mmStdSequence "+
            "SET DrawingCd = @.DrawingCd "+ 
            " , SeqNo = @.SeqNo "+ 
            " , WorkStationGrp = @.WorkStationGrp "+ 
            " , TaskCode = @.TaskCode "+ 
            " , WorkCenterCode = @.WorkCenterCode "+ 
            " , MachCode = @.MachCode "+ 
            " , SetupTime = @.SetupTime "+ 
            " , NoOfMins = @.NoOfMins "+ 
            " , ProductionRate = @.ProductionRate "+ 
            " , UnitCode = @.UnitCode "+ 
            " , TimeCode = @.TimeCode "+ 
            " , CycleTime = @.CycleTime "+ 
            " , LastUpdDt = @.LastUpdDt "+ 
            " , LastUpdUser = @.LastUpdUser "+ 
            " , SECM_NO = @.SECM_NO "+ 
            " , Tm = @.Tm "+ 
            " , USER_ID = @.USER_ID "+ 
            " , USER_DATE = @.USER_DATE "+ 
            " , RoutingDateSt = @.RoutingDateSt "+ 
            " , RoutingDateEnd = @.RoutingDateEnd "+ 
            " , NewFlag = @.NewFlag "+ 
            "WHERE DrawingCd = @.DrawingCd "+ 
            " AND SeqNo = @.SeqNo "+ 
            " AND WorkStationGrp = @.WorkStationGrp "+ 
            " AND TaskCode = @.TaskCode "+ 
            " AND WorkCenterCode = @.WorkCenterCode "+ 
            " AND MachCode = @.MachCode "+ 
            " AND SetupTime = @.SetupTime "+ 
            " AND NoOfMins = @.NoOfMins "+ 
            " AND ProductionRate = @.ProductionRate "+ 
            " AND UnitCode = @.UnitCode "+ 
            " AND TimeCode = @.TimeCode "+ 
            " AND CycleTime = @.CycleTime "+ 
            " AND LastUpdDt = @.LastUpdDt "+ 
            " AND LastUpdUser = @.LastUpdUser "+ 
            " AND SECM_NO = @.SECM_NO "+ 
            " AND Tm = @.Tm "+ 
            " AND USER_ID = @.USER_ID "+ 
            " AND USER_DATE = @.USER_DATE "+ 
            " AND RoutingDateSt = @.RoutingDateSt "+ 
            " AND RoutingDateEnd = @.RoutingDateEnd "+ 
            " AND NewFlag = @.NewFlag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.mmStdSequence d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@SeqNo", d.SeqNo.GetValue());
            param.Add("@WorkStationGrp", d.WorkStationGrp.GetValue());
            param.Add("@TaskCode", d.TaskCode.GetValue());
            param.Add("@WorkCenterCode", d.WorkCenterCode.GetValue());
            param.Add("@MachCode", d.MachCode.GetValue());
            param.Add("@SetupTime", d.SetupTime);
            param.Add("@NoOfMins", d.NoOfMins);
            param.Add("@ProductionRate", d.ProductionRate);
            param.Add("@UnitCode", d.UnitCode.GetValue());
            param.Add("@TimeCode", d.TimeCode.GetValue());
            param.Add("@CycleTime", d.CycleTime);
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@LastUpdUser", d.LastUpdUser.GetValue());
            param.Add("@SECM_NO", d.SECM_NO.GetValue());
            param.Add("@Tm", d.Tm.GetValue());
            param.Add("@USER_ID", d.USER_ID.GetValue());
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@RoutingDateSt", d.RoutingDateSt);
            param.Add("@RoutingDateEnd", d.RoutingDateEnd);
            param.Add("@NewFlag", d.NewFlag.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.mmStdSequence "+
            "WHERE DrawingCd = @.DrawingCd "+ 
            " AND SeqNo = @.SeqNo "+ 
            " AND WorkStationGrp = @.WorkStationGrp "+ 
            " AND TaskCode = @.TaskCode "+ 
            " AND WorkCenterCode = @.WorkCenterCode "+ 
            " AND MachCode = @.MachCode "+ 
            " AND SetupTime = @.SetupTime "+ 
            " AND NoOfMins = @.NoOfMins "+ 
            " AND ProductionRate = @.ProductionRate "+ 
            " AND UnitCode = @.UnitCode "+ 
            " AND TimeCode = @.TimeCode "+ 
            " AND CycleTime = @.CycleTime "+ 
            " AND LastUpdDt = @.LastUpdDt "+ 
            " AND LastUpdUser = @.LastUpdUser "+ 
            " AND SECM_NO = @.SECM_NO "+ 
            " AND Tm = @.Tm "+ 
            " AND USER_ID = @.USER_ID "+ 
            " AND USER_DATE = @.USER_DATE "+ 
            " AND RoutingDateSt = @.RoutingDateSt "+ 
            " AND RoutingDateEnd = @.RoutingDateEnd "+ 
            " AND NewFlag = @.NewFlag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }










































    }
}
