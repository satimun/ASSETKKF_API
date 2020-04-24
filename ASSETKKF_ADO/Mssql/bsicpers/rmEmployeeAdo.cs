using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.bsicpers
{
    public class rmEmployeeAdo : Base
    {
        private static rmEmployeeAdo instant;

        public static rmEmployeeAdo GetInstant()
        {
            if (instant == null) instant = new rmEmployeeAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private rmEmployeeAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee> ListActive()
        {
            string cmd = " SELECT * FROM bsicpers.dbo.rmEmployee ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee> GetData(ASSETKKF_MODEL.Request.bsicpers.rmEmployeeReq d )
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@EmployeeID", d.EmployeeID);
            param.Add("@EndDate", d.EndDate);
            /*
            param.Add("@EmploType", d.EmploType);
            param.Add("@Status", d.Status);
            param.Add("@Shift", d.Shift);
            param.Add("@Weekend", d.Weekend);
            param.Add("@DepCode", d.DepCode);
            param.Add("@Position", d.Position);
            param.Add("@TitleName", d.TitleName);
            param.Add("@FirstName", d.FirstName);
            param.Add("@LastName", d.LastName);
            param.Add("@StartDate", d.StartDate);
            param.Add("@EmploDate", d.EmploDate);
            param.Add("@EndDate", d.EndDate);
            param.Add("@EditDate", d.EditDate);*/
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM bsicpers.dbo.rmEmployee " +
            $"WHERE (EmployeeID = @EmployeeID) " +
            $"  AND ( EndDate IS NULL OR ISNULL(EndDate,'') >= ISNULL(@EndDate,'')  )" +
            
            /*
            $"  AND (@EmploType IS NULL OR EmploType = d.EmploType) " +
            $"  AND (@Status IS NULL OR Status = d.Status) " +
            $"  AND (@Shift IS NULL OR Shift = d.Shift) " +
            $"  AND (@Weekend IS NULL OR Weekend = d.Weekend) " +
            $"  AND (@DepCode IS NULL OR DepCode = d.DepCode) " +
            $"  AND (@Position IS NULL OR Position = d.Position) " +
            $"  AND (@TitleName IS NULL OR TitleName = d.TitleName) " +
            $"  AND (@FirstName IS NULL OR FirstName = d.FirstName) " +
            $"  AND (@LastName IS NULL OR LastName = d.LastName) " +
            $"  AND (@StartDate IS NULL OR StartDate = d.StartDate) " +
            $"  AND (@EmploDate IS NULL OR EmploDate = d.EmploDate) " +
            $"  AND (@EndDate IS NULL OR EndDate = d.EndDate) " +
            $"  AND (@EditDate IS NULL OR EditDate = d.EditDate) " +*/
            //$"AND (EmployeeID LIKE @txtSearch OR EmployeeID LIKE @txtSearch) " +  
            "ORDER BY EmployeeID;";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee> Search(ASSETKKF_MODEL.Request.bsicpers.rmEmployeeReq d )
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@EmployeeIDIsNull", d.EmployeeID.ListNull()); 
            param.Add("@EmploTypeIsNull", d.EmploType.ListNull()); 
            param.Add("@StatusIsNull", d.Status.ListNull()); 
            param.Add("@ShiftIsNull", d.Shift.ListNull()); 
            param.Add("@WeekendIsNull", d.Weekend.ListNull()); 
            param.Add("@DepCodeIsNull", d.DepCode.ListNull()); 
            param.Add("@PositionIsNull", d.Position.ListNull()); 
            param.Add("@TitleNameIsNull", d.TitleName.ListNull()); 
            param.Add("@FirstNameIsNull", d.FirstName.ListNull()); 
            param.Add("@LastNameIsNull", d.LastName.ListNull()); 
            param.Add("@StartDateIsNull", d.StartDate.ListNull()); 
            param.Add("@EmploDateIsNull", d.EmploDate.ListNull()); 
            param.Add("@EndDateIsNull", d.EndDate.ListNull()); 
            param.Add("@EditDateIsNull", d.EditDate.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM .dbo.rmEmployee " +
            $"WHERE (@EmployeeIDIsNull IS NULL OR EmployeeID IN ('{ d.EmployeeID.Join("','") }')) " +
            $"AND (@EmploTypeIsNull IS NULL OR EmploType IN ('{ d.EmploType.Join("','") }')) " +
            $"AND (@StatusIsNull IS NULL OR Status IN ('{ d.Status.Join("','") }')) " +
            $"AND (@ShiftIsNull IS NULL OR Shift IN ('{ d.Shift.Join("','") }')) " +
            $"AND (@WeekendIsNull IS NULL OR Weekend IN ('{ d.Weekend.Join("','") }')) " +
            $"AND (@DepCodeIsNull IS NULL OR DepCode IN ('{ d.DepCode.Join("','") }')) " +
            $"AND (@PositionIsNull IS NULL OR Position IN ('{ d.Position.Join("','") }')) " +
            $"AND (@TitleNameIsNull IS NULL OR TitleName IN ('{ d.TitleName.Join("','") }')) " +
            $"AND (@FirstNameIsNull IS NULL OR FirstName IN ('{ d.FirstName.Join("','") }')) " +
            $"AND (@LastNameIsNull IS NULL OR LastName IN ('{ d.LastName.Join("','") }')) " +
            $"AND (@StartDateIsNull IS NULL OR StartDate IN ('{ d.StartDate.Join("','") }')) " +
            $"AND (@EmploDateIsNull IS NULL OR EmploDate IN ('{ d.EmploDate.Join("','") }')) " +
            $"AND (@EndDateIsNull IS NULL OR EndDate IN ('{ d.EndDate.Join("','") }')) " +
            $"AND (@EditDateIsNull IS NULL OR EditDate IN ('{ d.EditDate.Join("','") }')) " +
            $"AND (EmployeeID LIKE @txtSearch OR EmployeeID LIKE @txtSearch) " +  
            "ORDER BY aFieldFirstName;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@EmployeeID", d.EmployeeID);
            param.Add("@EmploType", d.EmploType);
            param.Add("@Status", d.Status);
            param.Add("@Shift", d.Shift);
            param.Add("@Weekend", d.Weekend);
            param.Add("@DepCode", d.DepCode);
            param.Add("@Position", d.Position);
            param.Add("@TitleName", d.TitleName);
            param.Add("@FirstName", d.FirstName);
            param.Add("@LastName", d.LastName);
            param.Add("@StartDate", d.StartDate);
            param.Add("@EmploDate", d.EmploDate);
            param.Add("@EndDate", d.EndDate);
            param.Add("@EditDate", d.EditDate);
            string cmd = "INSERT INTO .dbo.rmEmployee " +
            $"      (EmployeeID, EmploType, Status, Shift, Weekend, DepCode, Position, TitleName, FirstName, LastName, StartDate, EmploDate, EndDate, EditDate) " +
            $"VALUES(@EmployeeID, @EmploType, @Status, @Shift, @Weekend, @DepCode, @Position, @TitleName, @FirstName, @LastName, @StartDate, @EmploDate, @EndDate, @EditDate); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@EmployeeID", d.EmployeeID.GetValue());
            param.Add("@EmploType", d.EmploType.GetValue());
            param.Add("@Status", d.Status.GetValue());
            param.Add("@Shift", d.Shift.GetValue());
            param.Add("@Weekend", d.Weekend.GetValue());
            param.Add("@DepCode", d.DepCode.GetValue());
            param.Add("@Position", d.Position.GetValue());
            param.Add("@TitleName", d.TitleName.GetValue());
            param.Add("@FirstName", d.FirstName.GetValue());
            param.Add("@LastName", d.LastName.GetValue());
            param.Add("@StartDate", d.StartDate);
            param.Add("@EmploDate", d.EmploDate);
            param.Add("@EndDate", d.EndDate);
            param.Add("@EditDate", d.EditDate);
            string cmd = "";
            /*
                   cmd = UPDATE .dbo.rmEmployee "+
            "SET EmployeeID = @.EmployeeID "+ 
            " , EmploType = @.EmploType "+ 
            " , Status = @.Status "+ 
            " , Shift = @.Shift "+ 
            " , Weekend = @.Weekend "+ 
            " , DepCode = @.DepCode "+ 
            " , Position = @.Position "+ 
            " , TitleName = @.TitleName "+ 
            " , FirstName = @.FirstName "+ 
            " , LastName = @.LastName "+ 
            " , StartDate = @.StartDate "+ 
            " , EmploDate = @.EmploDate "+ 
            " , EndDate = @.EndDate "+ 
            " , EditDate = @.EditDate "+ 
            "WHERE EmployeeID = @.EmployeeID "+ 
            " AND EmploType = @.EmploType "+ 
            " AND Status = @.Status "+ 
            " AND Shift = @.Shift "+ 
            " AND Weekend = @.Weekend "+ 
            " AND DepCode = @.DepCode "+ 
            " AND Position = @.Position "+ 
            " AND TitleName = @.TitleName "+ 
            " AND FirstName = @.FirstName "+ 
            " AND LastName = @.LastName "+ 
            " AND StartDate = @.StartDate "+ 
            " AND EmploDate = @.EmploDate "+ 
            " AND EndDate = @.EndDate "+ 
            " AND EditDate = @.EditDate "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.bsicpers.rmEmployee d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@EmployeeID", d.EmployeeID.GetValue());
            param.Add("@EmploType", d.EmploType.GetValue());
            param.Add("@Status", d.Status.GetValue());
            param.Add("@Shift", d.Shift.GetValue());
            param.Add("@Weekend", d.Weekend.GetValue());
            param.Add("@DepCode", d.DepCode.GetValue());
            param.Add("@Position", d.Position.GetValue());
            param.Add("@TitleName", d.TitleName.GetValue());
            param.Add("@FirstName", d.FirstName.GetValue());
            param.Add("@LastName", d.LastName.GetValue());
            param.Add("@StartDate", d.StartDate);
            param.Add("@EmploDate", d.EmploDate);
            param.Add("@EndDate", d.EndDate);
            param.Add("@EditDate", d.EditDate);
            string cmd = "";
            /*
                   cmd = DELETE FROM bsicpers.dbo.rmEmployee "+
            "WHERE EmployeeID = @.EmployeeID "+ 
            " AND EmploType = @.EmploType "+ 
            " AND Status = @.Status "+ 
            " AND Shift = @.Shift "+ 
            " AND Weekend = @.Weekend "+ 
            " AND DepCode = @.DepCode "+ 
            " AND Position = @.Position "+ 
            " AND TitleName = @.TitleName "+ 
            " AND FirstName = @.FirstName "+ 
            " AND LastName = @.LastName "+ 
            " AND StartDate = @.StartDate "+ 
            " AND EmploDate = @.EmploDate "+ 
            " AND EndDate = @.EndDate "+ 
            " AND EditDate = @.EditDate "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }


    }
}
