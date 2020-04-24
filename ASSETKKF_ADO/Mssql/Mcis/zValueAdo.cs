using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class zValueAdo : Base
    {
        private static zValueAdo instant;

        public static zValueAdo GetInstant()
        {
            if (instant == null) instant = new zValueAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private zValueAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.zValue> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.zValue ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.zValue>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.zValue> GetData(ASSETKKF_MODEL.Request.Mcis.zValueReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@ValueName", d.ValueName);
            param.Add("@ValueData", d.ValueData);
            param.Add("@ValueDes", d.ValueDes);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM mcis.dbo.zValue " +
            $"WHERE (@ValueName IS NULL OR ValueName = @ValueName) " +
            $"  AND (@ValueData IS NULL OR ValueData = @ValueData) " +
            $"  AND (@ValueDes IS NULL OR ValueDes = @ValueDes) " +
            //$"AND (ValueName LIKE @txtSearch OR ValueName LIKE @txtSearch) " +  
            "ORDER BY ValueName ;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.zValue>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.zValue> Search(ASSETKKF_MODEL.Request.Mcis.zValueReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@ValueNameIsNull", d.ValueName.ListNull()); 
            param.Add("@ValueDataIsNull", d.ValueData.ListNull()); 
            param.Add("@ValueDesIsNull", d.ValueDes.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.zValue " +
            $"WHERE (@ValueNameIsNull IS NULL OR ValueName IN ('{ d.ValueName.Join("','") }')) " +
            $"AND (@ValueDataIsNull IS NULL OR ValueData IN ('{ d.ValueData.Join("','") }')) " +
            $"AND (@ValueDesIsNull IS NULL OR ValueDes IN ('{ d.ValueDes.Join("','") }')) " +
            $"AND (ValueName LIKE @txtSearch OR ValueName LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.zValue>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.zValue d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@ValueName", d.ValueName);
            param.Add("@ValueData", d.ValueData);
            param.Add("@ValueDes", d.ValueDes);
            string cmd = "INSERT INTO mcis.dbo.zValue " +
            $"      (ValueName, ValueData, ValueDes) " +
            $"VALUES(@ValueName, @ValueData, @ValueDes); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.zValue d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@ValueName", d.ValueName.GetValue());
            param.Add("@ValueData", d.ValueData.GetValue());
            param.Add("@ValueDes", d.ValueDes.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.zValue "+
            "SET ValueName = @.ValueName "+ 
            " , ValueData = @.ValueData "+ 
            " , ValueDes = @.ValueDes "+ 
            "WHERE ValueName = @.ValueName "+ 
            " AND ValueData = @.ValueData "+ 
            " AND ValueDes = @.ValueDes "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.zValue d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@ValueName", d.ValueName.GetValue());
            param.Add("@ValueData", d.ValueData.GetValue());
            param.Add("@ValueDes", d.ValueDes.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.zValue "+
            "WHERE ValueName = @.ValueName "+ 
            " AND ValueData = @.ValueData "+ 
            " AND ValueDes = @.ValueDes "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

         
    }



 
}
