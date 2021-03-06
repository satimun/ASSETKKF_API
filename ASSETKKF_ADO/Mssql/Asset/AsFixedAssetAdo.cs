﻿using ASSETKKF_MODEL.Data.Mssql.Asset;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class AsFixedAssetAdo : Base
    {
        private static AsFixedAssetAdo instant;

        public static AsFixedAssetAdo GetInstant()
        {
            if (instant == null) instant = new AsFixedAssetAdo();
            return instant;
        }

       

        private AsFixedAssetAdo()
        {
            
        }

        public List<AsFixedAsset> Search(ASSETKKF_MODEL.Data.Mssql.Asset.AsFixedAsset d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@OFFICECODE", d.OFFICECODE);

            string cmd = "SELECT * FROM [FT_ASFIXEDASSET] (" + QuoteStr(d.OFFICECODE) + ") where 1 = 1";
            /*string cmd = "SELECT * FROM [FT_ASFIXEDASSET] (@OFFICECODE) ";
            cmd += " where 1 = 1";*/

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                cmd += " and  assetno = " + QuoteStr(d.ASSETNO);
            }
            var res = Query<AsFixedAsset>(cmd, param, conStr).ToList();
            return res;
        }
    }
}
