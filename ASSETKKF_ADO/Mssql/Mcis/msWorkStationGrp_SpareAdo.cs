using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class msWorkStationGrp_SpareAdo : Base
    {
        private static msWorkStationGrp_SpareAdo instant;

        public static msWorkStationGrp_SpareAdo GetInstant()
        {
            if (instant == null) instant = new msWorkStationGrp_SpareAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private msWorkStationGrp_SpareAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.msWorkStationGrp_Spare ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare> GetData(ASSETKKF_MODEL.Request.Mcis.msWorkStationGrp_SpareReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@WorkStationGrpCd_Spare", d.WorkStationGrpCd_Spare);
          /*  param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);*/
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM mcis.dbo.msWorkStationGrp_Spare " +
            $"WHERE (@WorkStationGrpCd IS NULL OR WorkStationGrpCd = @WorkStationGrpCd) " +
            $"  AND (@WorkStationGrpCd_Spare IS NULL OR WorkStationGrpCd_Spare = @WorkStationGrpCd_Spare) " +
            
            /*$"  AND (@User_Id IS NULL OR User_Id = @User_Id) " +
            $"  AND (@User_date IS NULL OR User_date = @User_date) " +*/
            //$"AND (WorkStationGrpCd LIKE @txtSearch OR WorkStationGrpCd LIKE @txtSearch) " +  
            $"ORDER BY  WorkStationGrpCd;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare> Search(ASSETKKF_MODEL.Request.Mcis.msWorkStationGrp_SpareReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@WorkStationGrpCdIsNull", d.WorkStationGrpCd.ListNull()); 
            param.Add("@WorkStationGrpCd_SpareIsNull", d.WorkStationGrpCd_Spare.ListNull()); 
            param.Add("@User_IdIsNull", d.User_Id.ListNull()); 
            param.Add("@User_dateIsNull", d.User_date.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.msWorkStationGrp_Spare " +
            $"WHERE (@WorkStationGrpCdIsNull IS NULL OR WorkStationGrpCd IN ('{ d.WorkStationGrpCd.Join("','") }')) " +
            $"AND (@WorkStationGrpCd_SpareIsNull IS NULL OR WorkStationGrpCd_Spare IN ('{ d.WorkStationGrpCd_Spare.Join("','") }')) " +
            $"AND (@User_IdIsNull IS NULL OR User_Id IN ('{ d.User_Id.Join("','") }')) " +
            $"AND (@User_dateIsNull IS NULL OR User_date IN ('{ d.User_date.Join("','") }')) " +
            $"AND (WorkStationGrpCd LIKE @txtSearch OR WorkStationGrpCd LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@WorkStationGrpCd_Spare", d.WorkStationGrpCd_Spare);
            param.Add("@User_Id", d.User_Id);
            param.Add("@User_date", d.User_date);
            string cmd = "INSERT INTO mcis.dbo.msWorkStationGrp_Spare " +
            $"      (WorkStationGrpCd, WorkStationGrpCd_Spare, User_Id, User_date) " +
            $"VALUES(@WorkStationGrpCd, @WorkStationGrpCd_Spare, @User_Id, @User_date); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@WorkStationGrpCd_Spare", d.WorkStationGrpCd_Spare.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.msWorkStationGrp_Spare "+
            "SET WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " , WorkStationGrpCd_Spare = @.WorkStationGrpCd_Spare "+ 
            " , User_Id = @.User_Id "+ 
            " , User_date = @.User_date "+ 
            "WHERE WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND WorkStationGrpCd_Spare = @.WorkStationGrpCd_Spare "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp_Spare d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@WorkStationGrpCd_Spare", d.WorkStationGrpCd_Spare.GetValue());
            param.Add("@User_Id", d.User_Id.GetValue());
            param.Add("@User_date", d.User_date);
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.msWorkStationGrp_Spare "+
            "WHERE WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND WorkStationGrpCd_Spare = @.WorkStationGrpCd_Spare "+ 
            " AND User_Id = @.User_Id "+ 
            " AND User_date = @.User_date "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }








    }
}
