using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class muTokenAdo : Base
    {
        private static muTokenAdo instant;

        public static muTokenAdo GetInstant()
        {
            if (instant == null) instant = new muTokenAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private muTokenAdo()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.muToken> ListActive()
        {
            string cmd = "SELECT * FROM muToken " +
                "WHERE ExpiryTime > GETDATE() AND Status = 'A';";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.muToken>(cmd, null).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.muToken> Search(string Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = "SELECT * FROM muToken " +
                "WHERE Code=@Code;";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.muToken>(cmd, param).ToList();
            return res;
        }

        public int Get(string Code, DateTime ExpiryTime, string userCode = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);
            param.Add("@ExpiryTime", ExpiryTime);
            param.Add("@UpdateBy", userCode);

            string cmd = $"UPDATE muToken SET " +
                "ExpiryTime=@ExpiryTime, " +
                "UpdateBy=@UpdateBy, " +
                "Timestamp=GETDATE() " +
                "WHERE Code=@Code;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(string Code, string userCode = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);
            param.Add("@UpdateBy",String.IsNullOrEmpty(userCode)?"": userCode);

            string cmd = $"UPDATE muToken SET " +
                "Status='C', " +
                "UpdateBy=@UpdateBy, " +
                "Timestamp=GETDATE() " +
                "WHERE Code=@Code;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Asset.muToken d, string userCode = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", d.Code);
            param.Add("@UserCode", d.UserCode);
            param.Add("@AccessToken_Code", d.AccessToken_Code);
            param.Add("@ExpiryTime", d.ExpiryTime);
            param.Add("@Type", d.Type);
            param.Add("@UpdateBy", userCode);

            string cmd = "INSERT INTO muToken (Code, UserCode, AccessToken_Code, ExpiryTime, Type, Status, UpdateBy, Timestamp) " +
                "VALUES (@Code, @UserCode, @AccessToken_Code, @ExpiryTime, @Type, 'A', @UpdateBy, GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }
    }
}
