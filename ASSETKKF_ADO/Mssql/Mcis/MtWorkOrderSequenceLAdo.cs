using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class MtWorkOrderSequenceLAdo : Base
    {
        private static MtWorkOrderSequenceLAdo instant;

        public static MtWorkOrderSequenceLAdo GetInstant()
        {
            if (instant == null) instant = new MtWorkOrderSequenceLAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private MtWorkOrderSequenceLAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.MtWorkOrderSequenceL ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL>GetData(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequenceLReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Move_Flag", d.Move_Flag);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);         
            param.Add("@WorkOrderId", d.WorkOrderId);
             


/*
            param.Add("@WorkDate", d.WorkDate);
            
            param.Add("@EmployeeId", d.EmployeeId);
            
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            
            param.Add("@ReworkFlag", d.ReworkFlag);
            param.Add("@WorkStationGrp_STD", d.WorkStationGrp_STD);
            param.Add("@SeqNo_STD", d.SeqNo_STD);
            param.Add("@WorkStationGrpCd_Next", d.WorkStationGrpCd_Next);
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT);
            param.Add("@CauseStatus", d.CauseStatus);
            param.Add("@RemarkCause", d.RemarkCause);
            param.Add("@AppvCauseId", d.AppvCauseId);
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@ExtFlag", d.ExtFlag);
            param.Add("@EndSequence", d.EndSequence);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
*/
            string cmd = "SELECT * FROM mcis.dbo.MtWorkOrderSequenceL " +
            $"  WHERE (@Move_Flag IS NULL OR Move_Flag = @Move_Flag) " +
            $"    AND (@WorkStationGrpCd IS NULL OR WorkStationGrpCd = @WorkStationGrpCd) " +      
            $"    AND (@WorkOrderId IS NULL OR WorkOrderId = @WorkOrderId) " +

            /*                    
            $"WHERE (@WorkDate IS NULL OR WorkDate = @WorkDate) " +
            $"  AND (@WorkStationGrpCd IS NULL OR WorkStationGrpCd = @WorkStationGrpCd) " +
            $"  AND (@EmployeeId IS NULL OR EmployeeId = @EmployeeId) " +
            $"  AND (@WorkOrderId IS NULL OR WorkOrderId = @WorkOrderId) " +
            $"  AND (@EndTime IS NULL OR EndTime = @EndTime) " +
            $"  AND (@QtyAmt IS NULL OR QtyAmt = @QtyAmt) " +
            $"  AND (@DrawingCd IS NULL OR DrawingCd = @DrawingCd) " +
            $"  AND (@CusCod IS NULL OR CusCod = @CusCod) " +
            $"  AND (@Move_Flag IS NULL OR Move_Flag = @Move_Flag) " +
            $"  AND (@ReworkFlag IS NULL OR ReworkFlag = @ReworkFlag) " +
            $"  AND (@WorkStationGrp_STD IS NULL OR WorkStationGrp_STD = @WorkStationGrp_STD) " +
            $"  AND (@SeqNo_STD IS NULL OR SeqNo_STD = @SeqNo_STD) " +
            $"  AND (@WorkStationGrpCd_Next IS NULL OR WorkStationGrpCd_Next = @WorkStationGrpCd_Next) " +
            $"  AND (@SeqNo_NEXT IS NULL OR SeqNo_NEXT = @SeqNo_NEXT) " +
            $"  AND (@CauseStatus IS NULL OR CauseStatus = @CauseStatus) " +
            $"  AND (@RemarkCause IS NULL OR RemarkCause = @RemarkCause) " +
            $"  AND (@AppvCauseId IS NULL OR AppvCauseId = @AppvCauseId) " +
            $"  AND (@AppvCauseDt IS NULL OR AppvCauseDt = @AppvCauseDt) " +
            $"  AND (@User_Id IS NULL OR User_Id = @User_Id) " +
            $"  AND (@User_date IS NULL OR User_date = @User_date) " +
            $"  AND (@ExtFlag IS NULL OR ExtFlag = @ExtFlag) " +
            $"  AND (@EndSequence IS NULL OR EndSequence = @EndSequence) " +*/
            //$"AND (WorkDate LIKE @txtSearch OR WorkDate LIKE @txtSearch) " +  

            "ORDER BY  WorkStationGrpCd;"; 

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL> Search(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequenceLReq d )
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@WorkDateIsNull", d.WorkDate.ListNull()); 
            param.Add("@WorkStationGrpCdIsNull", d.WorkStationGrpCd.ListNull()); 
            param.Add("@EmployeeIdIsNull", d.EmployeeId.ListNull()); 
            param.Add("@WorkOrderIdIsNull", d.WorkOrderId.ListNull()); 
            param.Add("@EndTimeIsNull", d.EndTime.ListNull()); 
            param.Add("@QtyAmtIsNull", d.QtyAmt.ListNull()); 
            param.Add("@DrawingCdIsNull", d.DrawingCd.ListNull()); 
            param.Add("@CusCodIsNull", d.CusCod.ListNull()); 
            param.Add("@Move_FlagIsNull", d.Move_Flag.ListNull()); 
            param.Add("@ReworkFlagIsNull", d.ReworkFlag.ListNull()); 
            param.Add("@WorkStationGrp_STDIsNull", d.WorkStationGrp_STD.ListNull()); 
            param.Add("@SeqNo_STDIsNull", d.SeqNo_STD.ListNull()); 
            param.Add("@WorkStationGrpCd_NextIsNull", d.WorkStationGrpCd_Next.ListNull()); 
            param.Add("@SeqNo_NEXTIsNull", d.SeqNo_NEXT.ListNull()); 
            param.Add("@CauseStatusIsNull", d.CauseStatus.ListNull()); 
            param.Add("@RemarkCauseIsNull", d.RemarkCause.ListNull()); 
            param.Add("@AppvCauseIdIsNull", d.AppvCauseId.ListNull()); 
            param.Add("@AppvCauseDtIsNull", d.AppvCauseDt.ListNull()); 
            param.Add("@User_IdIsNull", d.User_Id.ListNull()); 
            param.Add("@User_dateIsNull", d.User_date.ListNull()); 
            param.Add("@ExtFlagIsNull", d.ExtFlag.ListNull()); 
            param.Add("@EndSequenceIsNull", d.EndSequence.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.MtWorkOrderSequenceL " +
            $"WHERE (@WorkDateIsNull IS NULL OR WorkDate IN ('{ d.WorkDate.Join("','") }')) " +
            $"AND (@WorkStationGrpCdIsNull IS NULL OR WorkStationGrpCd IN ('{ d.WorkStationGrpCd.Join("','") }')) " +
            $"AND (@EmployeeIdIsNull IS NULL OR EmployeeId IN ('{ d.EmployeeId.Join("','") }')) " +
            $"AND (@WorkOrderIdIsNull IS NULL OR WorkOrderId IN ('{ d.WorkOrderId.Join("','") }')) " +
            $"AND (@EndTimeIsNull IS NULL OR EndTime IN ('{ d.EndTime.Join("','") }')) " +
            $"AND (@QtyAmtIsNull IS NULL OR QtyAmt IN ('{ d.QtyAmt.Join("','") }')) " +
            $"AND (@DrawingCdIsNull IS NULL OR DrawingCd IN ('{ d.DrawingCd.Join("','") }')) " +
            $"AND (@CusCodIsNull IS NULL OR CusCod IN ('{ d.CusCod.Join("','") }')) " +
            $"AND (@Move_FlagIsNull IS NULL OR Move_Flag IN ('{ d.Move_Flag.Join("','") }')) " +
            $"AND (@ReworkFlagIsNull IS NULL OR ReworkFlag IN ('{ d.ReworkFlag.Join("','") }')) " +
            $"AND (@WorkStationGrp_STDIsNull IS NULL OR WorkStationGrp_STD IN ('{ d.WorkStationGrp_STD.Join("','") }')) " +
            $"AND (@SeqNo_STDIsNull IS NULL OR SeqNo_STD IN ('{ d.SeqNo_STD.Join("','") }')) " +
            $"AND (@WorkStationGrpCd_NextIsNull IS NULL OR WorkStationGrpCd_Next IN ('{ d.WorkStationGrpCd_Next.Join("','") }')) " +
            $"AND (@SeqNo_NEXTIsNull IS NULL OR SeqNo_NEXT IN ('{ d.SeqNo_NEXT.Join("','") }')) " +
            $"AND (@CauseStatusIsNull IS NULL OR CauseStatus IN ('{ d.CauseStatus.Join("','") }')) " +
            $"AND (@RemarkCauseIsNull IS NULL OR RemarkCause IN ('{ d.RemarkCause.Join("','") }')) " +
            $"AND (@AppvCauseIdIsNull IS NULL OR AppvCauseId IN ('{ d.AppvCauseId.Join("','") }')) " +
            $"AND (@AppvCauseDtIsNull IS NULL OR AppvCauseDt IN ('{ d.AppvCauseDt.Join("','") }')) " +
            $"AND (@User_IdIsNull IS NULL OR User_Id IN ('{ d.User_Id.Join("','") }')) " +
            $"AND (@User_dateIsNull IS NULL OR User_date IN ('{ d.User_date.Join("','") }')) " +
            $"AND (@ExtFlagIsNull IS NULL OR ExtFlag IN ('{ d.ExtFlag.Join("','") }')) " +
            $"AND (@EndSequenceIsNull IS NULL OR EndSequence IN ('{ d.EndSequence.Join("','") }')) " +
            $"AND (WorkDate LIKE @txtSearch OR WorkDate LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkOrderId", d.WorkOrderId);
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            param.Add("@Move_Flag", d.Move_Flag);
            param.Add("@ReworkFlag", d.ReworkFlag);
            param.Add("@WorkStationGrp_STD", d.WorkStationGrp_STD);
            param.Add("@SeqNo_STD", d.SeqNo_STD);
            param.Add("@WorkStationGrpCd_Next", d.WorkStationGrpCd_Next);
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT);
            param.Add("@CauseStatus", d.CauseStatus);
            param.Add("@RemarkCause", d.RemarkCause);
            param.Add("@AppvCauseId", d.AppvCauseId);
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@ExtFlag", d.ExtFlag);
            param.Add("@EndSequence", d.EndSequence);
            string cmd = "INSERT INTO mcis.dbo.MtWorkOrderSequenceL " +
            $"      (WorkDate, WorkStationGrpCd, EmployeeId, WorkOrderId, EndTime, QtyAmt, DrawingCd, CusCod, Move_Flag, ReworkFlag, WorkStationGrp_STD, SeqNo_STD, WorkStationGrpCd_Next, SeqNo_NEXT, CauseStatus, RemarkCause, AppvCauseId, AppvCauseDt, User_Id, User_date, ExtFlag, EndSequence) " +
            $"VALUES(@WorkDate, @WorkStationGrpCd, @EmployeeId, @WorkOrderId, @EndTime, @QtyAmt, @DrawingCd, @CusCod, @Move_Flag, @ReworkFlag, @WorkStationGrp_STD, @SeqNo_STD, @WorkStationGrpCd_Next, @SeqNo_NEXT, @CauseStatus, @RemarkCause, @AppvCauseId, @AppvCauseDt, @User_Id, @User_date, @ExtFlag, @EndSequence); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@CusCod", d.CusCod.GetValue());
            param.Add("@Move_Flag", d.Move_Flag.GetValue());
            param.Add("@ReworkFlag", d.ReworkFlag.GetValue());
            param.Add("@WorkStationGrp_STD", d.WorkStationGrp_STD.GetValue());
            param.Add("@SeqNo_STD", d.SeqNo_STD.GetValue());
            param.Add("@WorkStationGrpCd_Next", d.WorkStationGrpCd_Next.GetValue());
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT.GetValue());
            param.Add("@CauseStatus", d.CauseStatus.GetValue());
            param.Add("@RemarkCause", d.RemarkCause.GetValue());
            param.Add("@AppvCauseId", d.AppvCauseId.GetValue());
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            param.Add("@ExtFlag", d.ExtFlag.GetValue());
            param.Add("@EndSequence", d.EndSequence.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.MtWorkOrderSequenceL "+
            "SET WorkDate = @.WorkDate "+ 
            " , WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " , EmployeeId = @.EmployeeId "+ 
            " , WorkOrderId = @.WorkOrderId "+ 
            " , EndTime = @.EndTime "+ 
            " , QtyAmt = @.QtyAmt "+ 
            " , DrawingCd = @.DrawingCd "+ 
            " , CusCod = @.CusCod "+ 
            " , Move_Flag = @.Move_Flag "+ 
            " , ReworkFlag = @.ReworkFlag "+ 
            " , WorkStationGrp_STD = @.WorkStationGrp_STD "+ 
            " , SeqNo_STD = @.SeqNo_STD "+ 
            " , WorkStationGrpCd_Next = @.WorkStationGrpCd_Next "+ 
            " , SeqNo_NEXT = @.SeqNo_NEXT "+ 
            " , CauseStatus = @.CauseStatus "+ 
            " , RemarkCause = @.RemarkCause "+ 
            " , AppvCauseId = @.AppvCauseId "+ 
            " , AppvCauseDt = @.AppvCauseDt "+ 
            " , User_Id = @.User_Id "+ 
            " , User_date = @.User_date "+ 
            " , ExtFlag = @.ExtFlag "+ 
            " , EndSequence = @.EndSequence "+ 
            "WHERE WorkDate = @.WorkDate "+ 
            " AND WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND EndTime = @.EndTime "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND CusCod = @.CusCod "+ 
            " AND Move_Flag = @.Move_Flag "+ 
            " AND ReworkFlag = @.ReworkFlag "+ 
            " AND WorkStationGrp_STD = @.WorkStationGrp_STD "+ 
            " AND SeqNo_STD = @.SeqNo_STD "+ 
            " AND WorkStationGrpCd_Next = @.WorkStationGrpCd_Next "+ 
            " AND SeqNo_NEXT = @.SeqNo_NEXT "+ 
            " AND CauseStatus = @.CauseStatus "+ 
            " AND RemarkCause = @.RemarkCause "+ 
            " AND AppvCauseId = @.AppvCauseId "+ 
            " AND AppvCauseDt = @.AppvCauseDt "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " AND ExtFlag = @.ExtFlag "+ 
            " AND EndSequence = @.EndSequence "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceL d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@CusCod", d.CusCod.GetValue());
            param.Add("@Move_Flag", d.Move_Flag.GetValue());
            param.Add("@ReworkFlag", d.ReworkFlag.GetValue());
            param.Add("@WorkStationGrp_STD", d.WorkStationGrp_STD.GetValue());
            param.Add("@SeqNo_STD", d.SeqNo_STD.GetValue());
            param.Add("@WorkStationGrpCd_Next", d.WorkStationGrpCd_Next.GetValue());
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT.GetValue());
            param.Add("@CauseStatus", d.CauseStatus.GetValue());
            param.Add("@RemarkCause", d.RemarkCause.GetValue());
            param.Add("@AppvCauseId", d.AppvCauseId.GetValue());
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            param.Add("@ExtFlag", d.ExtFlag.GetValue());
            param.Add("@EndSequence", d.EndSequence.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.MtWorkOrderSequenceL "+
            "WHERE WorkDate = @.WorkDate "+ 
            " AND WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND EndTime = @.EndTime "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND CusCod = @.CusCod "+ 
            " AND Move_Flag = @.Move_Flag "+ 
            " AND ReworkFlag = @.ReworkFlag "+ 
            " AND WorkStationGrp_STD = @.WorkStationGrp_STD "+ 
            " AND SeqNo_STD = @.SeqNo_STD "+ 
            " AND WorkStationGrpCd_Next = @.WorkStationGrpCd_Next "+ 
            " AND SeqNo_NEXT = @.SeqNo_NEXT "+ 
            " AND CauseStatus = @.CauseStatus "+ 
            " AND RemarkCause = @.RemarkCause "+ 
            " AND AppvCauseId = @.AppvCauseId "+ 
            " AND AppvCauseDt = @.AppvCauseDt "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " AND ExtFlag = @.ExtFlag "+ 
            " AND EndSequence = @.EndSequence "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }


    }
}
