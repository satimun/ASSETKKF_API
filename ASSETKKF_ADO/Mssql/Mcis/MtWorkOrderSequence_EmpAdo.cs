using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class MtWorkOrderSequence_EmpAdo : Base
    {
        private static MtWorkOrderSequence_EmpAdo instant;

        public static MtWorkOrderSequence_EmpAdo GetInstant()
        {
            if (instant == null) instant = new MtWorkOrderSequence_EmpAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private MtWorkOrderSequence_EmpAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.MtWorkOrderSequence_Emp ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp> GetDataViwe(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequence_EmpReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@WorkOrderId", d.WorkOrderId);
            param.Add("@WorkDate", d.WorkDate);
            /*
            param.Add("@ItemNo", d.ItemNo);
            param.Add("@StartTime", d.StartTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@EndTime", d.EndTime);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            param.Add("@Post_flag", d.Post_flag);
            param.Add("@Pause_Flag", d.Pause_Flag);
            param.Add("@ReworkFlag", d.ReworkFlag);*/
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = ""+
            $"select Z.*,isnull(A.CustomerID + ' : ' + A.CustomerDesc, '') AS Customer_name from( " +
            $"   select MtWorkOrderSequence_Emp.*, MtWorkOrderH.WorkOrderDesc " +
            $"   , RTrim(TitleName) + ' ' + FirstName + '  ' + LastName AS Emp_name " +
            $"   , MtWorkOrderSequence_Emp.WorkStationGrpCd + ' : ' + C.WorkStationGrpNm AS Groupname " +
            $"   , (Case when CustomerID <> '' then CustomerID else CustDepCd end) AS CustomerID " +
            $"from MtWorkOrderSequence_Emp " +
            $"left join MtWorkOrderH on MtWorkOrderSequence_Emp.WorkOrderId = MtWorkOrderH.WorkOrderID " +
            $"left join msWorkStationGrp C on MtWorkOrderSequence_Emp.WorkStationGrpCd = C.WorkStationGrpCd " +             
            $"left join Bsicpers.dbo.rmEmployee D on MtWorkOrderSequence_Emp.EmployeeId = D.EmployeeID " +             
            $"left join mmMchProject B on SubString(MtWorkOrderSequence_Emp.WorkOrderId, 1, 6) = B.MchProjectID " +
         $" WHERE (@WorkStationGrpCd IS NULL OR MtWorkOrderSequence_Emp.WorkStationGrpCd = @WorkStationGrpCd) " +
             $"   AND (@WorkOrderId IS NULL OR MtWorkOrderSequence_Emp.WorkOrderId = @WorkOrderId) " +
             $"   AND (@EmployeeId IS NULL OR MtWorkOrderSequence_Emp.EmployeeId = @EmployeeId) " +
             $"   AND (@WorkDate  IS NULL OR MtWorkOrderSequence_Emp.WorkDate = DATEADD(D, 0, DATEDIFF(D, 0, @WorkDate ))    )" +

            $" )Z left join mmCustomer A on Z.CustomerID = A.CustomerID  " +
  
            /*
            $" AND (@WorkDate IS NULL OR WorkDate = @WorkDate) " +
            $"  AND (@ItemNo IS NULL OR ItemNo = @ItemNo) " +
          
            $"  AND (@StartTime IS NULL OR StartTime = @StartTime) " +
            $"  AND (@QtyAmt IS NULL OR QtyAmt = @QtyAmt) " +
            $"  AND (@EndTime IS NULL OR EndTime = @EndTime) " +
            $"  AND (@StdTime IS NULL OR StdTime = @StdTime) " +
            $"  AND (@ActTime IS NULL OR ActTime = @ActTime) " +
            $"  AND (@DiffTime IS NULL OR DiffTime = @DiffTime) " +
            $"  AND (@Use_FreeTimeOT IS NULL OR Use_FreeTimeOT = @Use_FreeTimeOT) " +
            $"  AND (@User_Id IS NULL OR User_Id = @User_Id) " +
            $"  AND (@User_date IS NULL OR User_date = @User_date) " +
            $"  AND (@DrawingCd IS NULL OR DrawingCd = @DrawingCd) " +
            $"  AND (@CusCod IS NULL OR CusCod = @CusCod) " +
            $"  AND (@Post_flag IS NULL OR Post_flag = @Post_flag) " +
            $"  AND (@Pause_Flag IS NULL OR Pause_Flag = @Pause_Flag) " +
            $"  AND (@ReworkFlag IS NULL OR ReworkFlag = @ReworkFlag) " +*/
            //$"AND (WorkDate LIKE @txtSearch OR WorkDate LIKE @txtSearch) " +  
            $"ORDER BY WorkDate,ItemNo;";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp> GetData(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequence_EmpReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@WorkOrderId", d.WorkOrderId);

            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);            
            param.Add("@StartTime", d.StartTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@EndTime", d.EndTime);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            param.Add("@Post_flag", d.Post_flag);
            param.Add("@Pause_Flag", d.Pause_Flag);
            param.Add("@ReworkFlag", d.ReworkFlag);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM mcis.dbo.MtWorkOrderSequence_Emp " +
            $"  WHERE (@WorkStationGrpCd IS NULL OR WorkStationGrpCd = @WorkStationGrpCd) " +
            $"  AND (@WorkOrderId IS NULL OR WorkOrderId = @WorkOrderId) " +
            $"  AND (@EmployeeId IS NULL OR EmployeeId = @EmployeeId) " +
            /*
            $" AND (@WorkDate IS NULL OR WorkDate = @WorkDate) " +
            $"  AND (@ItemNo IS NULL OR ItemNo = @ItemNo) " +
          
            $"  AND (@StartTime IS NULL OR StartTime = @StartTime) " +
            $"  AND (@QtyAmt IS NULL OR QtyAmt = @QtyAmt) " +
            $"  AND (@EndTime IS NULL OR EndTime = @EndTime) " +
            $"  AND (@StdTime IS NULL OR StdTime = @StdTime) " +
            $"  AND (@ActTime IS NULL OR ActTime = @ActTime) " +
            $"  AND (@DiffTime IS NULL OR DiffTime = @DiffTime) " +
            $"  AND (@Use_FreeTimeOT IS NULL OR Use_FreeTimeOT = @Use_FreeTimeOT) " +
            $"  AND (@User_Id IS NULL OR User_Id = @User_Id) " +
            $"  AND (@User_date IS NULL OR User_date = @User_date) " +
            $"  AND (@DrawingCd IS NULL OR DrawingCd = @DrawingCd) " +
            $"  AND (@CusCod IS NULL OR CusCod = @CusCod) " +
            $"  AND (@Post_flag IS NULL OR Post_flag = @Post_flag) " +
            $"  AND (@Pause_Flag IS NULL OR Pause_Flag = @Pause_Flag) " +
            $"  AND (@ReworkFlag IS NULL OR ReworkFlag = @ReworkFlag) " +*/
            //$"AND (WorkDate LIKE @txtSearch OR WorkDate LIKE @txtSearch) " +  
            $"ORDER BY  WorkStationGrpCd;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp> Search(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequence_EmpReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@WorkDateIsNull", d.WorkDate.ListNull()); 
            param.Add("@ItemNoIsNull", d.ItemNo.ListNull()); 
            param.Add("@EmployeeIdIsNull", d.EmployeeId.ListNull()); 
            param.Add("@WorkStationGrpCdIsNull", d.WorkStationGrpCd.ListNull()); 
            param.Add("@WorkOrderIdIsNull", d.WorkOrderId.ListNull()); 
            param.Add("@StartTimeIsNull", d.StartTime.ListNull()); 
            param.Add("@QtyAmtIsNull", d.QtyAmt.ListNull()); 
            param.Add("@EndTimeIsNull", d.EndTime.ListNull()); 
            param.Add("@StdTimeIsNull", d.StdTime.ListNull()); 
            param.Add("@ActTimeIsNull", d.ActTime.ListNull()); 
            param.Add("@DiffTimeIsNull", d.DiffTime.ListNull()); 
            param.Add("@Use_FreeTimeOTIsNull", d.Use_FreeTimeOT.ListNull()); 
            param.Add("@User_IdIsNull", d.User_Id.ListNull()); 
            param.Add("@User_dateIsNull", d.User_date.ListNull()); 
            param.Add("@DrawingCdIsNull", d.DrawingCd.ListNull()); 
            param.Add("@CusCodIsNull", d.CusCod.ListNull()); 
            param.Add("@Post_flagIsNull", d.Post_flag.ListNull()); 
            param.Add("@Pause_FlagIsNull", d.Pause_Flag.ListNull()); 
            param.Add("@ReworkFlagIsNull", d.ReworkFlag.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.MtWorkOrderSequence_Emp " +
            $"WHERE (@WorkDateIsNull IS NULL OR WorkDate IN ('{ d.WorkDate.Join("','") }')) " +
            $"AND (@ItemNoIsNull IS NULL OR ItemNo IN ('{ d.ItemNo.Join("','") }')) " +
            $"AND (@EmployeeIdIsNull IS NULL OR EmployeeId IN ('{ d.EmployeeId.Join("','") }')) " +
            $"AND (@WorkStationGrpCdIsNull IS NULL OR WorkStationGrpCd IN ('{ d.WorkStationGrpCd.Join("','") }')) " +
            $"AND (@WorkOrderIdIsNull IS NULL OR WorkOrderId IN ('{ d.WorkOrderId.Join("','") }')) " +
            $"AND (@StartTimeIsNull IS NULL OR StartTime IN ('{ d.StartTime.Join("','") }')) " +
            $"AND (@QtyAmtIsNull IS NULL OR QtyAmt IN ('{ d.QtyAmt.Join("','") }')) " +
            $"AND (@EndTimeIsNull IS NULL OR EndTime IN ('{ d.EndTime.Join("','") }')) " +
            $"AND (@StdTimeIsNull IS NULL OR StdTime IN ('{ d.StdTime.Join("','") }')) " +
            $"AND (@ActTimeIsNull IS NULL OR ActTime IN ('{ d.ActTime.Join("','") }')) " +
            $"AND (@DiffTimeIsNull IS NULL OR DiffTime IN ('{ d.DiffTime.Join("','") }')) " +
            $"AND (@Use_FreeTimeOTIsNull IS NULL OR Use_FreeTimeOT IN ('{ d.Use_FreeTimeOT.Join("','") }')) " +
            $"AND (@User_IdIsNull IS NULL OR User_Id IN ('{ d.User_Id.Join("','") }')) " +
            $"AND (@User_dateIsNull IS NULL OR User_date IN ('{ d.User_date.Join("','") }')) " +
            $"AND (@DrawingCdIsNull IS NULL OR DrawingCd IN ('{ d.DrawingCd.Join("','") }')) " +
            $"AND (@CusCodIsNull IS NULL OR CusCod IN ('{ d.CusCod.Join("','") }')) " +
            $"AND (@Post_flagIsNull IS NULL OR Post_flag IN ('{ d.Post_flag.Join("','") }')) " +
            $"AND (@Pause_FlagIsNull IS NULL OR Pause_Flag IN ('{ d.Pause_Flag.Join("','") }')) " +
            $"AND (@ReworkFlagIsNull IS NULL OR ReworkFlag IN ('{ d.ReworkFlag.Join("','") }')) " +
            $"AND (WorkDate LIKE @txtSearch OR WorkDate LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceCloseOrder> CloseOrder(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequenceCloseOrderReq d)
        {
            DynamicParameters param = new DynamicParameters();

            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkOrderID", d.WorkOrderID);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
           
            param.Add("@AddEmployeeId", d.AddEmployeeId);
            param.Add("@User_Id", d.User_Id);
            param.Add("@USERAPP", d.USERAPP);

            param.Add("@CloseChoose", d.CloseChoose);
            param.Add("@EndSequence", d.EndSequence);
            param.Add("@DateSelect", d.DateSelect);
            param.Add("@TimeSelect", d.TimeSelect);
            param.Add("@DateSelectFull", d.DateSelectFull);
           /*
            string cmd = "" +
                $"EXEC [dbo].[MtWorkOrderSequenceCheckInsert] " +
                $"  @EmployeeId" +
                $" ,@WorkOrderID" +
                $" ,@WorkStationGrpCd" +
                $" ,@AddEmployeeId" +
                $" ,@User_Id" +
                $" ,@USERAPP" +
                $" ,@CloseChoose" +
                $" ,@EndSequence" +
                $" ,@DateSelect" +
                $" ,@TimeSelect" +
                $" ,@DateSelectFull";*/
          
            string cmd = "" +
                $"EXEC [dbo].[MtWorkOrderSequenceCheckInsert] " +
                $"   '" + d.EmployeeId +"'"+
                $" , '" + d.WorkOrderID + "'" +
                $" , '" + d.WorkStationGrpCd + "'" +
                $" , '" + d.AddEmployeeId + "'" +
                $" , '" + d.User_Id + "'" +
                $" , '" + d.USERAPP + "'" +
                $" , '" + d.CloseChoose.ToString() + "'" +
                $" , '" + d.EndSequence + "'" +
                $" , '" + DateTimeUtil.GetDateString(d.DateSelect) + "'" +
                $" , '" + d.CloseChoose.ToString() + "'" +
                $" , '" + DateTimeUtil.GetDateTimeString(d.DateSelectFull)+"'" ;
                

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequenceCloseOrder>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);
            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@WorkOrderId", d.WorkOrderId);
            param.Add("@StartTime", d.StartTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@EndTime", d.EndTime);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            param.Add("@Post_flag", d.Post_flag);
            param.Add("@Pause_Flag", d.Pause_Flag);
            param.Add("@ReworkFlag", d.ReworkFlag);

            string cmd = "INSERT INTO mcis.dbo.MtWorkOrderSequence_Emp " +
            $"      (WorkDate, ItemNo, EmployeeId, WorkStationGrpCd, WorkOrderId, StartTime, QtyAmt, EndTime, StdTime, ActTime, DiffTime, Use_FreeTimeOT, User_Id, User_date, DrawingCd, CusCod, Post_flag, Pause_Flag, ReworkFlag) " +
            $" VALUES(@WorkDate, @ItemNo, @EmployeeId, @WorkStationGrpCd, @WorkOrderId, @StartTime, @QtyAmt, @EndTime, @StdTime, @ActTime, @DiffTime, @Use_FreeTimeOT, @User_Id, @User_date, @DrawingCd, @CusCod, @Post_flag, @Pause_Flag, @ReworkFlag); " +
            $" SELECT SCOPE_IDENTITY();";

            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@StartTime", d.StartTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@EndTime", d.EndTime);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@CusCod", d.CusCod.GetValue());
            param.Add("@Post_flag", d.Post_flag.GetValue());
            param.Add("@Pause_Flag", d.Pause_Flag.GetValue());
            param.Add("@ReworkFlag", d.ReworkFlag.GetValue());
            string cmd = "";

            /*
            cmd = UPDATE mcis.dbo.MtWorkOrderSequence_Emp "+
            "SET WorkDate = @.WorkDate "+ 
            " , ItemNo = @.ItemNo "+ 
            " , EmployeeId = @.EmployeeId "+ 
            " , WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " , WorkOrderId = @.WorkOrderId "+ 
            " , StartTime = @.StartTime "+ 
            " , QtyAmt = @.QtyAmt "+ 
            " , EndTime = @.EndTime "+ 
            " , StdTime = @.StdTime "+ 
            " , ActTime = @.ActTime "+ 
            " , DiffTime = @.DiffTime "+ 
            " , Use_FreeTimeOT = @.Use_FreeTimeOT "+ 
            " , User_Id = @.User_Id "+ 
            " , User_date = @.User_date "+ 
            " , DrawingCd = @.DrawingCd "+ 
            " , CusCod = @.CusCod "+ 
            " , Post_flag = @.Post_flag "+ 
            " , Pause_Flag = @.Pause_Flag "+ 
            " , ReworkFlag = @.ReworkFlag "+ 
            "WHERE WorkDate = @.WorkDate "+ 
            " AND ItemNo = @.ItemNo "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND StartTime = @.StartTime "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND EndTime = @.EndTime "+ 
            " AND StdTime = @.StdTime "+ 
            " AND ActTime = @.ActTime "+ 
            " AND DiffTime = @.DiffTime "+ 
            " AND Use_FreeTimeOT = @.Use_FreeTimeOT "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND CusCod = @.CusCod "+ 
            " AND Post_flag = @.Post_flag "+ 
            " AND Pause_Flag = @.Pause_Flag "+ 
            " AND ReworkFlag = @.ReworkFlag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Emp d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@StartTime", d.StartTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@EndTime", d.EndTime);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@CusCod", d.CusCod.GetValue());
            param.Add("@Post_flag", d.Post_flag.GetValue());
            param.Add("@Pause_Flag", d.Pause_Flag.GetValue());
            param.Add("@ReworkFlag", d.ReworkFlag.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.MtWorkOrderSequence_Emp "+
            "WHERE WorkDate = @.WorkDate "+ 
            " AND ItemNo = @.ItemNo "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND StartTime = @.StartTime "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND EndTime = @.EndTime "+ 
            " AND StdTime = @.StdTime "+ 
            " AND ActTime = @.ActTime "+ 
            " AND DiffTime = @.DiffTime "+ 
            " AND Use_FreeTimeOT = @.Use_FreeTimeOT "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND CusCod = @.CusCod "+ 
            " AND Post_flag = @.Post_flag "+ 
            " AND Pause_Flag = @.Pause_Flag "+ 
            " AND ReworkFlag = @.ReworkFlag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        } 



}
}
