using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class MsCauseAppvAdo : Base
    {
        private static MsCauseAppvAdo instant;

        public static MsCauseAppvAdo GetInstant()
        {
            if (instant == null) instant = new MsCauseAppvAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private MsCauseAppvAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.MsCauseAppv ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv> GetData(ASSETKKF_MODEL.Request.Mcis.MsCauseAppvReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@CauseID", d.CauseID);
           // param.Add("@CauseName", d.CauseName);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM mcis.dbo.MsCauseAppv " +
            $"WHERE (@CauseID IS NULL OR CauseID = @CauseID) " +
            //$"  AND (@CauseName IS NULL OR CauseName = @CauseName) " +
            //$"AND (CauseID LIKE @txtSearch OR CauseID LIKE @txtSearch) " +  
            $"ORDER BY  CauseID;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv> Search(ASSETKKF_MODEL.Request.Mcis.MsCauseAppvReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@CauseIDIsNull", d.CauseID.ListNull()); 
            param.Add("@CauseNameIsNull", d.CauseName.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.MsCauseAppv " +
            $"WHERE (@CauseIDIsNull IS NULL OR CauseID IN ('{ d.CauseID.Join("','") }')) " +
            $"AND (@CauseNameIsNull IS NULL OR CauseName IN ('{ d.CauseName.Join("','") }')) " +
            $"AND (CauseID LIKE @txtSearch OR CauseID LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@CauseID", d.CauseID);
            param.Add("@CauseName", d.CauseName);
            string cmd = "INSERT INTO mcis.dbo.MsCauseAppv " +
            $"      (CauseID, CauseName) " +
            $"VALUES(@CauseID, @CauseName); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@CauseID", d.CauseID.GetValue());
            param.Add("@CauseName", d.CauseName.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.MsCauseAppv "+
            "SET CauseID = @.CauseID "+ 
            " , CauseName = @.CauseName "+ 
            "WHERE CauseID = @.CauseID "+ 
            " AND CauseName = @.CauseName "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.MsCauseAppv d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@CauseID", d.CauseID.GetValue());
            param.Add("@CauseName", d.CauseName.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.MsCauseAppv "+
            "WHERE CauseID = @.CauseID "+ 
            " AND CauseName = @.CauseName "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

         
    }

 
}
