using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class MtWorkOrderSequence_MchAdo : Base
    {
        private static MtWorkOrderSequence_MchAdo instant;

        public static MtWorkOrderSequence_MchAdo GetInstant()
        {
            if (instant == null) instant = new MtWorkOrderSequence_MchAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private MtWorkOrderSequence_MchAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.MtWorkOrderSequence_Mch ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch> GetData(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequence_MchReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Pause_Flag", d.Pause_Flag);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);            
            param.Add("@WorkOrderId", d.WorkOrderId);
            /*
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);           
            param.Add("@EmployeeId", d.EmployeeId);           
            param.Add("@StartTime", d.StartTime);
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            param.Add("@Post_flag", d.Post_flag);
            param.Add("@SupplierCd", d.SupplierCd);
            param.Add("@Remark", d.Remark);
            param.Add("@Wantdate", d.Wantdate);
            param.Add("@ReworkFlag", d.ReworkFlag);*/
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM mcis.dbo.MtWorkOrderSequence_Mch " +
            $" WHERE (@WorkStationGrpCd IS NULL OR WorkStationGrpCd = @WorkStationGrpCd) " +
            $"   AND (@WorkOrderId IS NULL OR WorkOrderId = @WorkOrderId) " +
            $"   AND (@Pause_Flag IS NULL OR Pause_Flag = @Pause_Flag) ";

            if (d.Pause_Flag.Trim() =="N")
            {
                cmd = cmd + $" AND 0 < (select count(*) from zValue where ValueName =  'OUTSEQ' ";
                cmd = cmd + $" AND ValueData like '%@WorkStationGrpCd%' )  ";



            }

            /*
             * zsql.add(' AND 0 < (select count(*) from zValue where ValueName = ''OUTSEQ''    ');
                         zsql.add(' AND ValueData like ''%'+trim(TBBotgroup.Text)+'%'')   ');
               */

            /*
                        $" AND (@WorkDate IS NULL OR WorkDate = @WorkDate) " +
                        $"  AND (@ItemNo IS NULL OR ItemNo = @ItemNo) " +

                        $"  AND (@EmployeeId IS NULL OR EmployeeId = @EmployeeId) " +

                        $"  AND (@StartTime IS NULL OR StartTime = @StartTime) " +
                        $"  AND (@EndTime IS NULL OR EndTime = @EndTime) " +
                        $"  AND (@QtyAmt IS NULL OR QtyAmt = @QtyAmt) " +
                        $"  AND (@StdTime IS NULL OR StdTime = @StdTime) " +
                        $"  AND (@ActTime IS NULL OR ActTime = @ActTime) " +
                        $"  AND (@DiffTime IS NULL OR DiffTime = @DiffTime) " +
                        $"  AND (@Use_FreeTimeOT IS NULL OR Use_FreeTimeOT = @Use_FreeTimeOT) " +
                        $"  AND (@User_Id IS NULL OR User_Id = @User_Id) " +
                        $"  AND (@User_date IS NULL OR User_date = @User_date) " +
                        $"  AND (@DrawingCd IS NULL OR DrawingCd = @DrawingCd) " +
                        $"  AND (@CusCod IS NULL OR CusCod = @CusCod) " +
                        $"  AND (@Post_flag IS NULL OR Post_flag = @Post_flag) " +
                        $"  AND (@SupplierCd IS NULL OR SupplierCd = @SupplierCd) " +
                        $"  AND (@Remark IS NULL OR Remark = @Remark) " +
                        $"  AND (@Wantdate IS NULL OR Wantdate = @Wantdate) " +
                        $"  AND (@ReworkFlag IS NULL OR ReworkFlag = @ReworkFlag) " +

                        //$"AND (WorkDate LIKE @txtSearch OR WorkDate LIKE @txtSearch) " +  
                        */
            cmd = cmd+$" ORDER BY  WorkStationGrpCd;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch> GetDataView(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequence_MchReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeId", d.EmployeeId);            
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@WorkOrderId", d.WorkOrderId);
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@Pause_Flag", d.Pause_Flag);
            /*
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);           
                   
            param.Add("@StartTime", d.StartTime);
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            param.Add("@Post_flag", d.Post_flag);
            param.Add("@SupplierCd", d.SupplierCd);
            param.Add("@Remark", d.Remark);
            param.Add("@Wantdate", d.Wantdate);
            param.Add("@ReworkFlag", d.ReworkFlag);*/
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "  " +
             $" select z.* " +
             $" ,isnull(A.CustomerID + ' : ' + A.CustomerDesc, '') AS Customer_name from( " +
             $" select MtWorkOrderSequence_Mch.*, MtWorkOrderH.WorkOrderDesc " +
             $" , RTrim(TitleName) + ' ' + FirstName + '  ' + LastName AS Emp_name " +
             $" , MtWorkOrderSequence_Mch.WorkStationGrpCd + ' : ' + D.WorkStationGrpNm  AS Groupname " +
             $" , (Case when CustomerID <> ''  then CustomerID else CustDepCd end) AS CustomerID " +
             $" from MtWorkOrderSequence_Mch " +
             $" left join MtWorkOrderH on MtWorkOrderSequence_Mch.WorkOrderId = MtWorkOrderH.WorkOrderID " +
             $" left " +
             $" join Bsicpers.dbo.rmEmployee C on MtWorkOrderSequence_Mch.EmployeeId = C.EmployeeID " +
             $" left " +
             $" join msWorkStationGrp D on MtWorkOrderSequence_Mch.WorkStationGrpCd = D.WorkStationGrpCd " +
             $" left " +
             $" join mmMchProject B on SubString(MtWorkOrderSequence_Mch.WorkOrderId, 1, 6) = B.MchProjectID " +
             $" WHERE (@WorkStationGrpCd IS NULL OR MtWorkOrderSequence_Mch.WorkStationGrpCd = @WorkStationGrpCd) " +
             $"   AND (@WorkOrderId IS NULL OR MtWorkOrderSequence_Mch.WorkOrderId = @WorkOrderId) " +
             $"   AND (@EmployeeId IS NULL OR MtWorkOrderSequence_Mch.EmployeeId = @EmployeeId) " +
             $"   AND (@WorkDate  IS NULL OR MtWorkOrderSequence_Mch.WorkDate = DATEADD(D, 0, DATEDIFF(D, 0, @WorkDate ))    )" +
             $" )Z left join mmCustomer A on Z.CustomerID = A.CustomerID ORDER BY  ItemNo DESC, WorkDate ";

     

           
           // cmd = cmd + $" ORDER BY  WorkStationGrpCd;";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch> Search(ASSETKKF_MODEL.Request.Mcis.MtWorkOrderSequence_MchReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@WorkDateIsNull", d.WorkDate.ListNull()); 
            param.Add("@ItemNoIsNull", d.ItemNo.ListNull()); 
            param.Add("@WorkStationGrpCdIsNull", d.WorkStationGrpCd.ListNull()); 
            param.Add("@EmployeeIdIsNull", d.EmployeeId.ListNull()); 
            param.Add("@WorkOrderIdIsNull", d.WorkOrderId.ListNull()); 
            param.Add("@StartTimeIsNull", d.StartTime.ListNull()); 
            param.Add("@EndTimeIsNull", d.EndTime.ListNull()); 
            param.Add("@QtyAmtIsNull", d.QtyAmt.ListNull()); 
            param.Add("@StdTimeIsNull", d.StdTime.ListNull()); 
            param.Add("@ActTimeIsNull", d.ActTime.ListNull()); 
            param.Add("@DiffTimeIsNull", d.DiffTime.ListNull()); 
            param.Add("@Use_FreeTimeOTIsNull", d.Use_FreeTimeOT.ListNull()); 
            param.Add("@User_IdIsNull", d.User_Id.ListNull()); 
            param.Add("@User_dateIsNull", d.User_date.ListNull()); 
            param.Add("@DrawingCdIsNull", d.DrawingCd.ListNull()); 
            param.Add("@CusCodIsNull", d.CusCod.ListNull()); 
            param.Add("@Post_flagIsNull", d.Post_flag.ListNull()); 
            param.Add("@SupplierCdIsNull", d.SupplierCd.ListNull()); 
            param.Add("@RemarkIsNull", d.Remark.ListNull()); 
            param.Add("@WantdateIsNull", d.Wantdate.ListNull()); 
            param.Add("@ReworkFlagIsNull", d.ReworkFlag.ListNull()); 
            param.Add("@Pause_FlagIsNull", d.Pause_Flag.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.MtWorkOrderSequence_Mch " +
            $"WHERE (@WorkDateIsNull IS NULL OR WorkDate IN ('{ d.WorkDate.Join("','") }')) " +
            $"AND (@ItemNoIsNull IS NULL OR ItemNo IN ('{ d.ItemNo.Join("','") }')) " +
            $"AND (@WorkStationGrpCdIsNull IS NULL OR WorkStationGrpCd IN ('{ d.WorkStationGrpCd.Join("','") }')) " +
            $"AND (@EmployeeIdIsNull IS NULL OR EmployeeId IN ('{ d.EmployeeId.Join("','") }')) " +
            $"AND (@WorkOrderIdIsNull IS NULL OR WorkOrderId IN ('{ d.WorkOrderId.Join("','") }')) " +
            $"AND (@StartTimeIsNull IS NULL OR StartTime IN ('{ d.StartTime.Join("','") }')) " +
            $"AND (@EndTimeIsNull IS NULL OR EndTime IN ('{ d.EndTime.Join("','") }')) " +
            $"AND (@QtyAmtIsNull IS NULL OR QtyAmt IN ('{ d.QtyAmt.Join("','") }')) " +
            $"AND (@StdTimeIsNull IS NULL OR StdTime IN ('{ d.StdTime.Join("','") }')) " +
            $"AND (@ActTimeIsNull IS NULL OR ActTime IN ('{ d.ActTime.Join("','") }')) " +
            $"AND (@DiffTimeIsNull IS NULL OR DiffTime IN ('{ d.DiffTime.Join("','") }')) " +
            $"AND (@Use_FreeTimeOTIsNull IS NULL OR Use_FreeTimeOT IN ('{ d.Use_FreeTimeOT.Join("','") }')) " +
            $"AND (@User_IdIsNull IS NULL OR User_Id IN ('{ d.User_Id.Join("','") }')) " +
            $"AND (@User_dateIsNull IS NULL OR User_date IN ('{ d.User_date.Join("','") }')) " +
            $"AND (@DrawingCdIsNull IS NULL OR DrawingCd IN ('{ d.DrawingCd.Join("','") }')) " +
            $"AND (@CusCodIsNull IS NULL OR CusCod IN ('{ d.CusCod.Join("','") }')) " +
            $"AND (@Post_flagIsNull IS NULL OR Post_flag IN ('{ d.Post_flag.Join("','") }')) " +
            $"AND (@SupplierCdIsNull IS NULL OR SupplierCd IN ('{ d.SupplierCd.Join("','") }')) " +
            $"AND (@RemarkIsNull IS NULL OR Remark IN ('{ d.Remark.Join("','") }')) " +
            $"AND (@WantdateIsNull IS NULL OR Wantdate IN ('{ d.Wantdate.Join("','") }')) " +
            $"AND (@ReworkFlagIsNull IS NULL OR ReworkFlag IN ('{ d.ReworkFlag.Join("','") }')) " +
            $"AND (@Pause_FlagIsNull IS NULL OR Pause_Flag IN ('{ d.Pause_Flag.Join("','") }')) " +
            $"AND (WorkDate LIKE @txtSearch OR WorkDate LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@EmployeeId", d.EmployeeId);
            param.Add("@WorkOrderId", d.WorkOrderId);
            param.Add("@StartTime", d.StartTime);
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@CusCod", d.CusCod);
            param.Add("@Post_flag", d.Post_flag);
            param.Add("@SupplierCd", d.SupplierCd);
            param.Add("@Remark", d.Remark);
            param.Add("@Wantdate", d.Wantdate);
            param.Add("@ReworkFlag", d.ReworkFlag);
            param.Add("@Pause_Flag", d.Pause_Flag);
            string cmd = "INSERT INTO mcis.dbo.MtWorkOrderSequence_Mch " +
            $"      (WorkDate, ItemNo, WorkStationGrpCd, EmployeeId, WorkOrderId, StartTime, EndTime, QtyAmt, StdTime, ActTime, DiffTime, Use_FreeTimeOT, User_Id, User_date, DrawingCd, CusCod, Post_flag, SupplierCd, Remark, Wantdate, ReworkFlag, Pause_Flag) " +
            $"VALUES(@WorkDate, @ItemNo, @WorkStationGrpCd, @EmployeeId, @WorkOrderId, @StartTime, @EndTime, @QtyAmt, @StdTime, @ActTime, @DiffTime, @Use_FreeTimeOT, @User_Id, @User_date, @DrawingCd, @CusCod, @Post_flag, @SupplierCd, @Remark, @Wantdate, @ReworkFlag, @Pause_Flag); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@StartTime", d.StartTime);
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@CusCod", d.CusCod.GetValue());
            param.Add("@Post_flag", d.Post_flag.GetValue());
            param.Add("@SupplierCd", d.SupplierCd.GetValue());
            param.Add("@Remark", d.Remark.GetValue());
            param.Add("@Wantdate", d.Wantdate);
            param.Add("@ReworkFlag", d.ReworkFlag.GetValue());
            param.Add("@Pause_Flag", d.Pause_Flag.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.MtWorkOrderSequence_Mch "+
            "SET WorkDate = @.WorkDate "+ 
            " , ItemNo = @.ItemNo "+ 
            " , WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " , EmployeeId = @.EmployeeId "+ 
            " , WorkOrderId = @.WorkOrderId "+ 
            " , StartTime = @.StartTime "+ 
            " , EndTime = @.EndTime "+ 
            " , QtyAmt = @.QtyAmt "+ 
            " , StdTime = @.StdTime "+ 
            " , ActTime = @.ActTime "+ 
            " , DiffTime = @.DiffTime "+ 
            " , Use_FreeTimeOT = @.Use_FreeTimeOT "+ 
            " , User_Id = @.User_Id "+ 
            " , User_date = @.User_date "+ 
            " , DrawingCd = @.DrawingCd "+ 
            " , CusCod = @.CusCod "+ 
            " , Post_flag = @.Post_flag "+ 
            " , SupplierCd = @.SupplierCd "+ 
            " , Remark = @.Remark "+ 
            " , Wantdate = @.Wantdate "+ 
            " , ReworkFlag = @.ReworkFlag "+ 
            " , Pause_Flag = @.Pause_Flag "+ 
            "WHERE WorkDate = @.WorkDate "+ 
            " AND ItemNo = @.ItemNo "+ 
            " AND WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND StartTime = @.StartTime "+ 
            " AND EndTime = @.EndTime "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND StdTime = @.StdTime "+ 
            " AND ActTime = @.ActTime "+ 
            " AND DiffTime = @.DiffTime "+ 
            " AND Use_FreeTimeOT = @.Use_FreeTimeOT "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND CusCod = @.CusCod "+ 
            " AND Post_flag = @.Post_flag "+ 
            " AND SupplierCd = @.SupplierCd "+ 
            " AND Remark = @.Remark "+ 
            " AND Wantdate = @.Wantdate "+ 
            " AND ReworkFlag = @.ReworkFlag "+ 
            " AND Pause_Flag = @.Pause_Flag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.MtWorkOrderSequence_Mch d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkDate", d.WorkDate);
            param.Add("@ItemNo", d.ItemNo);
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@EmployeeId", d.EmployeeId.GetValue());
            param.Add("@WorkOrderId", d.WorkOrderId.GetValue());
            param.Add("@StartTime", d.StartTime);
            param.Add("@EndTime", d.EndTime);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@StdTime", d.StdTime);
            param.Add("@ActTime", d.ActTime);
            param.Add("@DiffTime", d.DiffTime);
            param.Add("@Use_FreeTimeOT", d.Use_FreeTimeOT.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@CusCod", d.CusCod.GetValue());
            param.Add("@Post_flag", d.Post_flag.GetValue());
            param.Add("@SupplierCd", d.SupplierCd.GetValue());
            param.Add("@Remark", d.Remark.GetValue());
            param.Add("@Wantdate", d.Wantdate);
            param.Add("@ReworkFlag", d.ReworkFlag.GetValue());
            param.Add("@Pause_Flag", d.Pause_Flag.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.MtWorkOrderSequence_Mch "+
            "WHERE WorkDate = @.WorkDate "+ 
            " AND ItemNo = @.ItemNo "+ 
            " AND WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND EmployeeId = @.EmployeeId "+ 
            " AND WorkOrderId = @.WorkOrderId "+ 
            " AND StartTime = @.StartTime "+ 
            " AND EndTime = @.EndTime "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND StdTime = @.StdTime "+ 
            " AND ActTime = @.ActTime "+ 
            " AND DiffTime = @.DiffTime "+ 
            " AND Use_FreeTimeOT = @.Use_FreeTimeOT "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND CusCod = @.CusCod "+ 
            " AND Post_flag = @.Post_flag "+ 
            " AND SupplierCd = @.SupplierCd "+ 
            " AND Remark = @.Remark "+ 
            " AND Wantdate = @.Wantdate "+ 
            " AND ReworkFlag = @.ReworkFlag "+ 
            " AND Pause_Flag = @.Pause_Flag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

         

}
}
