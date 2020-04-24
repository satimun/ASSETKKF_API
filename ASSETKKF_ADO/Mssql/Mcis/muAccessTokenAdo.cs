using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class muAccessTokenAdo : Base
    {
        private static muAccessTokenAdo instant;

        public static muAccessTokenAdo GetInstant()
        {
            if (instant == null) instant = new muAccessTokenAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private muAccessTokenAdo()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.muAccessToken> ListActive()
        {
            string cmd = "SELECT * FROM muAccessToken " +
                "WHERE Status='A';";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.muAccessToken>(cmd, null).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.muAccessToken> Search(string Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = "SELECT * FROM muAccessToken " +
                "WHERE Code=@Code;";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.muAccessToken>(cmd, param).ToList();
            return res;
        }

        public int Update(string Code, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = $"UPDATE muAccessToken SET " +
                "CountUse=CountUse+1 " +
                "WHERE Code=@Code;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Insert(string Code, string IPAddress, string Agent, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);
            param.Add("@IPAddress", IPAddress);
            param.Add("@Agent", Agent);

            string cmd = "INSERT INTO muAccessToken (Code, IPAddress, Agent, CountUse, Status, UpdateBy, Timestamp) " +
                "VALUES (@Code, @IPAddress, @Agent, 1, 'A', 0, GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }
    }
}