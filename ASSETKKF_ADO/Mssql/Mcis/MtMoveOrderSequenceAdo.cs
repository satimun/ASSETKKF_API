using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Core.Util;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class MtMoveOrderSequenceAdo : Base
    {
        private static MtMoveOrderSequenceAdo instant;

        public static MtMoveOrderSequenceAdo GetInstant()
        {
            if (instant == null) instant = new MtMoveOrderSequenceAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private MtMoveOrderSequenceAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.MtMoveOrderSequence ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.GetNextWorkStationGrp> GetNextWorkStationGrp(ASSETKKF_MODEL.Request.Mcis.MtMoveOrderSequenceReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkOrderId_move", d.WorkOrderId);
            //param.Add("@Process_Flag", d.Process_Flag);

            string cmd = "EXEC [dbo].[GetNextWorkStationGrp] " +
            $"  @WorkOrderId = @WorkOrderId_move ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.GetNextWorkStationGrp>(cmd, param).ToList();
            return res;

        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence> GetData(ASSETKKF_MODEL.Request.Mcis.MtMoveOrderSequenceReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Sequence", d.Sequence);
            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkOrderId", d.WorkOrderId);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@WorkStationGrpCd_FR", d.WorkStationGrpCd_FR);
            param.Add("@WorkStationGrpCd_STD", d.WorkStationGrpCd_STD);
            param.Add("@WorkStationGrpCd_TO", d.WorkStationGrpCd_TO);
            /*
            param.Add("@MoveDate", d.MoveDate);
            param.Add("@MoveTime", d.MoveTime);
            
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT);
            param.Add("@EndTime", d.EndTime);
            param.Add("@CauseStatus", d.CauseStatus);
            param.Add("@RemarkCause", d.RemarkCause);
            param.Add("@AppvCauseId", d.AppvCauseId);
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@Process_Flag", d.Process_Flag);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */
            string cmd = "SELECT * FROM mcis.dbo.MtMoveOrderSequence " +
            $"WHERE (@Sequence IS NULL OR Sequence = @Sequence) " +
            $"  AND (@EmployeeId IS NULL OR EmployeeId = @EmployeeId) " +
            $"  AND (@WorkOrderId IS NULL OR WorkOrderId = @WorkOrderId) " +
            $"  AND (@DrawingCd IS NULL OR DrawingCd = @DrawingCd) " +
            $"  AND (@WorkStationGrpCd_FR IS NULL OR WorkStationGrpCd_FR = @WorkStationGrpCd_FR) " +
            $"  AND (@WorkStationGrpCd_STD IS NULL OR WorkStationGrpCd_STD = @WorkStationGrpCd_STD) " +
            $"  AND (@WorkStationGrpCd_TO IS NULL OR WorkStationGrpCd_TO = @WorkStationGrpCd_TO) " +

            /*
            $"  AND (@MoveDate IS NULL OR MoveDate = @MoveDate) " +
            $"  AND (@MoveTime IS NULL OR MoveTime = @MoveTime) " +
           
            $"  AND (@SeqNo_NEXT IS NULL OR SeqNo_NEXT = @SeqNo_NEXT) " +
            $"  AND (@EndTime IS NULL OR EndTime = @EndTime) " +
            $"  AND (@CauseStatus IS NULL OR CauseStatus = @CauseStatus) " +
            $"  AND (@RemarkCause IS NULL OR RemarkCause = @RemarkCause) " +
            $"  AND (@AppvCauseId IS NULL OR AppvCauseId = @AppvCauseId) " +
            $"  AND (@AppvCauseDt IS NULL OR AppvCauseDt = @AppvCauseDt) " +
            $"  AND (@Process_Flag IS NULL OR Process_Flag = @Process_Flag) " +
            $"  AND (@User_Id IS NULL OR User_Id = @User_Id) " +
            $"  AND (@User_date IS NULL OR User_date = @User_date) " +*/
            //$"AND (Sequence LIKE @txtSearch OR Sequence LIKE @txtSearch) " +  
             $"ORDER BY  Sequence;";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence>InsertMove(ASSETKKF_MODEL.Request.Mcis.MtMoveOrderSequenceReq d)
        {
           // EmployeeId,WorkOrderId,WorkStationGrpCd_TO,MoveDate,MoveTime,User_Id,AppvCauseId,RemarkCause,CauseStatus
            DynamicParameters param = new DynamicParameters();
            param.Add("@_EmployeeId", d.EmployeeId );
            param.Add("@_WorkOrderId", d.WorkOrderId);
            param.Add("@_WorkStationGrpCd_TO", d.WorkStationGrpCd_TO);
            param.Add("@_MoveDate", d.MoveDate);
            param.Add("@_EndTime", d.EndTime);
            param.Add("@_User_Id", d.User_Id);
            param.Add("@_AppvCauseId", d.AppvCauseId);
            param.Add("@_RemarkCause", d.RemarkCause);
            param.Add("@_CauseStatus", d.CauseStatus);


            string cmd = "EXEC[dbo].[MtMoveOrderSequenceInsert]" +
                $" @EmployeeId = @_EmployeeId " +
                $",@WorkOrderID = @_WorkOrderID  " +
                $",@WorkStationGrpCd_TO = @_WorkStationGrpCd_TO" +
                $",@CauseStatus = @_CauseStatus" +
                $",@RemarkCause = @_RemarkCause" +
                $",@AppvCauseId = @_AppvCauseId" +
                $",@User_Id = @_User_Id" +
                $",@EndTime = @_EndTime" +
                $",@MoveDate = @_MoveDate" +
                $"" +
                $"";
 
  

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence>(cmd, param).ToList();
            return res;
        }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence> Search(ASSETKKF_MODEL.Request.Mcis.MtMoveOrderSequenceReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@SequenceIsNull", d.Sequence.ListNull()); 
            param.Add("@MoveDateIsNull", d.MoveDate.ListNull()); 
            param.Add("@MoveTimeIsNull", d.MoveTime.ListNull()); 
            param.Add("@EmployeeIdIsNull", d.EmployeeId.ListNull()); 
            param.Add("@WorkOrderIdIsNull", d.WorkOrderId.ListNull()); 
            param.Add("@DrawingCdIsNull", d.DrawingCd.ListNull()); 
            param.Add("@WorkStationGrpCd_FRIsNull", d.WorkStationGrpCd_FR.ListNull()); 
            param.Add("@WorkStationGrpCd_STDIsNull", d.WorkStationGrpCd_STD.ListNull()); 
            param.Add("@WorkStationGrpCd_TOIsNull", d.WorkStationGrpCd_TO.ListNull()); 
            param.Add("@SeqNo_NEXTIsNull", d.SeqNo_NEXT.ListNull()); 
            param.Add("@EndTimeIsNull", d.EndTime.ListNull()); 
            param.Add("@CauseStatusIsNull", d.CauseStatus.ListNull()); 
            param.Add("@RemarkCauseIsNull", d.RemarkCause.ListNull()); 
            param.Add("@AppvCauseIdIsNull", d.AppvCauseId.ListNull()); 
            param.Add("@AppvCauseDtIsNull", d.AppvCauseDt.ListNull()); 
            param.Add("@Process_FlagIsNull", d.Process_Flag.ListNull()); 
            param.Add("@User_IdIsNull", d.User_Id.ListNull()); 
            param.Add("@User_dateIsNull", d.User_date.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.MtMoveOrderSequence " +
            $"WHERE (@SequenceIsNull IS NULL OR Sequence IN ('{ d.Sequence.Join("','") }')) " +
            $"AND (@MoveDateIsNull IS NULL OR MoveDate IN ('{ d.MoveDate.Join("','") }')) " +
            $"AND (@MoveTimeIsNull IS NULL OR MoveTime IN ('{ d.MoveTime.Join("','") }')) " +
            $"AND (@EmployeeIdIsNull IS NULL OR EmployeeId IN ('{ d.EmployeeId.Join("','") }')) " +
            $"AND (@WorkOrderIdIsNull IS NULL OR WorkOrderId IN ('{ d.WorkOrderId.Join("','") }')) " +
            $"AND (@DrawingCdIsNull IS NULL OR DrawingCd IN ('{ d.DrawingCd.Join("','") }')) " +
            $"AND (@WorkStationGrpCd_FRIsNull IS NULL OR WorkStationGrpCd_FR IN ('{ d.WorkStationGrpCd_FR.Join("','") }')) " +
            $"AND (@WorkStationGrpCd_STDIsNull IS NULL OR WorkStationGrpCd_STD IN ('{ d.WorkStationGrpCd_STD.Join("','") }')) " +
            $"AND (@WorkStationGrpCd_TOIsNull IS NULL OR WorkStationGrpCd_TO IN ('{ d.WorkStationGrpCd_TO.Join("','") }')) " +
            $"AND (@SeqNo_NEXTIsNull IS NULL OR SeqNo_NEXT IN ('{ d.SeqNo_NEXT.Join("','") }')) " +
            $"AND (@EndTimeIsNull IS NULL OR EndTime IN ('{ d.EndTime.Join("','") }')) " +
            $"AND (@CauseStatusIsNull IS NULL OR CauseStatus IN ('{ d.CauseStatus.Join("','") }')) " +
            $"AND (@RemarkCauseIsNull IS NULL OR RemarkCause IN ('{ d.RemarkCause.Join("','") }')) " +
            $"AND (@AppvCauseIdIsNull IS NULL OR AppvCauseId IN ('{ d.AppvCauseId.Join("','") }')) " +
            $"AND (@AppvCauseDtIsNull IS NULL OR AppvCauseDt IN ('{ d.AppvCauseDt.Join("','") }')) " +
            $"AND (@Process_FlagIsNull IS NULL OR Process_Flag IN ('{ d.Process_Flag.Join("','") }')) " +
            $"AND (@User_IdIsNull IS NULL OR User_Id IN ('{ d.User_Id.Join("','") }')) " +
            $"AND (@User_dateIsNull IS NULL OR User_date IN ('{ d.User_date.Join("','") }')) " +
            $"AND (Sequence LIKE @txtSearch OR Sequence LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence>(cmd, param).ToList();
            return res;
        }

         
        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@Sequence", d.Sequence);
            param.Add("@MoveDate", d.MoveDate);
            param.Add("@MoveTime", d.MoveTime);
            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkOrderId", d.WorkOrderId);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@WorkStationGrpCd_FR", d.WorkStationGrpCd_FR);
            param.Add("@WorkStationGrpCd_STD", d.WorkStationGrpCd_STD);
            param.Add("@WorkStationGrpCd_TO", d.WorkStationGrpCd_TO);
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT);
            param.Add("@EndTime", d.EndTime);
            param.Add("@CauseStatus", d.CauseStatus);
            param.Add("@RemarkCause", d.RemarkCause);
            param.Add("@AppvCauseId", d.AppvCauseId);
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@Process_Flag", d.Process_Flag);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            string cmd = "INSERT INTO mcis.dbo.MtMoveOrderSequence " +
            $"      (Sequence, MoveDate, MoveTime, EmployeeId, WorkOrderId, DrawingCd, WorkStationGrpCd_FR, WorkStationGrpCd_STD, WorkStationGrpCd_TO, SeqNo_NEXT, EndTime, CauseStatus, RemarkCause, AppvCauseId, AppvCauseDt, Process_Flag, User_Id, User_date) " +
            $"VALUES(@Sequence, @MoveDate, @MoveTime, @EmployeeId, @WorkOrderId, @DrawingCd, @WorkStationGrpCd_FR, @WorkStationGrpCd_STD, @WorkStationGrpCd_TO, @SeqNo_NEXT, @EndTime, @CauseStatus, @RemarkCause, @AppvCauseId, @AppvCauseDt, @Process_Flag, @User_Id, @User_date); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@Sequence", d.Sequence);
            param.Add("@MoveDate", d.MoveDate);
            param.Add("@MoveTime", d.MoveTime);
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@WorkStationGrpCd_FR", d.WorkStationGrpCd_FR.GetValue());
            param.Add("@WorkStationGrpCd_STD", d.WorkStationGrpCd_STD.GetValue());
            param.Add("@WorkStationGrpCd_TO", d.WorkStationGrpCd_TO.GetValue());
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT.GetValue());
            param.Add("@EndTime", d.EndTime);
            param.Add("@CauseStatus", d.CauseStatus.GetValue());
            param.Add("@RemarkCause", d.RemarkCause.GetValue());
            param.Add("@AppvCauseId", d.AppvCauseId.GetValue());
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@Process_Flag", d.Process_Flag.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.MtMoveOrderSequence "+
            "SET Sequence = @.Sequence "+ 
            " , MoveDate = @.MoveDate "+ 
            " , MoveTime = @.MoveTime "+ 
            " , EmployeeId = @.EmployeeId "+ 
            " , WorkOrderId = @.WorkOrderId "+ 
            " , DrawingCd = @.DrawingCd "+ 
            " , WorkStationGrpCd_FR = @.WorkStationGrpCd_FR "+ 
            " , WorkStationGrpCd_STD = @.WorkStationGrpCd_STD "+ 
            " , WorkStationGrpCd_TO = @.WorkStationGrpCd_TO "+ 
            " , SeqNo_NEXT = @.SeqNo_NEXT "+ 
            " , EndTime = @.EndTime "+ 
            " , CauseStatus = @.CauseStatus "+ 
            " , RemarkCause = @.RemarkCause "+ 
            " , AppvCauseId = @.AppvCauseId "+ 
            " , AppvCauseDt = @.AppvCauseDt "+ 
            " , Process_Flag = @.Process_Flag "+ 
            " , User_Id = @.User_Id "+ 
            " , User_date = @.User_date "+ 
            "WHERE Sequence = @.Sequence "+ 
            " AND MoveDate = @.MoveDate "+ 
            " AND MoveTime = @.MoveTime "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND WorkStationGrpCd_FR = @.WorkStationGrpCd_FR "+ 
            " AND WorkStationGrpCd_STD = @.WorkStationGrpCd_STD "+ 
            " AND WorkStationGrpCd_TO = @.WorkStationGrpCd_TO "+ 
            " AND SeqNo_NEXT = @.SeqNo_NEXT "+ 
            " AND EndTime = @.EndTime "+ 
            " AND CauseStatus = @.CauseStatus "+ 
            " AND RemarkCause = @.RemarkCause "+ 
            " AND AppvCauseId = @.AppvCauseId "+ 
            " AND AppvCauseDt = @.AppvCauseDt "+ 
            " AND Process_Flag = @.Process_Flag "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.MtMoveOrderSequence d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@Sequence", d.Sequence);
            param.Add("@MoveDate", d.MoveDate);
            param.Add("@MoveTime", d.MoveTime);
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@WorkStationGrpCd_FR", d.WorkStationGrpCd_FR.GetValue());
            param.Add("@WorkStationGrpCd_STD", d.WorkStationGrpCd_STD.GetValue());
            param.Add("@WorkStationGrpCd_TO", d.WorkStationGrpCd_TO.GetValue());
            param.Add("@SeqNo_NEXT", d.SeqNo_NEXT.GetValue());
            param.Add("@EndTime", d.EndTime);
            param.Add("@CauseStatus", d.CauseStatus.GetValue());
            param.Add("@RemarkCause", d.RemarkCause.GetValue());
            param.Add("@AppvCauseId", d.AppvCauseId.GetValue());
            param.Add("@AppvCauseDt", d.AppvCauseDt);
            param.Add("@Process_Flag", d.Process_Flag.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.MtMoveOrderSequence "+
            "WHERE Sequence = @.Sequence "+ 
            " AND MoveDate = @.MoveDate "+ 
            " AND MoveTime = @.MoveTime "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND WorkStationGrpCd_FR = @.WorkStationGrpCd_FR "+ 
            " AND WorkStationGrpCd_STD = @.WorkStationGrpCd_STD "+ 
            " AND WorkStationGrpCd_TO = @.WorkStationGrpCd_TO "+ 
            " AND SeqNo_NEXT = @.SeqNo_NEXT "+ 
            " AND EndTime = @.EndTime "+ 
            " AND CauseStatus = @.CauseStatus "+ 
            " AND RemarkCause = @.RemarkCause "+ 
            " AND AppvCauseId = @.AppvCauseId "+ 
            " AND AppvCauseDt = @.AppvCauseDt "+ 
            " AND Process_Flag = @.Process_Flag "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

         


 





    }
}
