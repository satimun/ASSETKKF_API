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

        private string conectStr { get; set; }

        private AsFixedAssetAdo()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.AsFixedAsset> Search(ASSETKKF_MODEL.Data.Mssql.Asset.AsFixedAsset d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@OFFICECODE", d.OFFICECODE);

            string cmd = "SELECT * FROM [dbo].[FT_ASFIXEDASSET] (@OFFICECODE)";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.AsFixedAsset>(cmd, param).ToList();
            return res;
        }
    }
}
