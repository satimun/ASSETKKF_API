﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class muAccessTokenAdo : Base
    {
        private static muAccessTokenAdo instant;

        public static muAccessTokenAdo GetInstant()
        {
            if (instant == null) instant = new muAccessTokenAdo();
            return instant;
        }

        

        private muAccessTokenAdo()
        {
           
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.muAccessToken> ListActive(string conStr = null)
        {
            string cmd = "SELECT * FROM muAccessToken " +
                "WHERE Status='A';";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.muAccessToken>(cmd, null, conStr).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.muAccessToken> Search(string Code, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = "SELECT * FROM muAccessToken " +
                "WHERE Code=@Code;";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.muAccessToken>(cmd, param, conStr).ToList();
            return res;
        }

        public int Update(string Code, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);

            string cmd = $"UPDATE muAccessToken SET " +
                "CountUse=CountUse+1 " +
                "WHERE Code=@Code;";
            var res = ExecuteNonQuery(transac, cmd, param, conStr);
            return res;
        }

        public int Insert(string Code, string IPAddress, string Agent, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", Code);
            param.Add("@IPAddress", IPAddress);
            param.Add("@Agent", Agent);

            string cmd = "INSERT INTO muAccessToken (Code, IPAddress, Agent, CountUse, Status, UpdateBy, Timestamp) " +
                "VALUES (@Code, @IPAddress, @Agent, 1, 'A', 0, GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param, conStr);
            return res;
        }
    }
}
