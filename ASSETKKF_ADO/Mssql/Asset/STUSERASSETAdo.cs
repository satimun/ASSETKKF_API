using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class STUSERASSETAdo : Base
    {
        private static STUSERASSETAdo instant;

        public static STUSERASSETAdo GetInstant()
        {
            if (instant == null) instant = new STUSERASSETAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private STUSERASSETAdo()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET> Search(ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@USERCODE", d.UCODE);
            param.Add("@UserName", d.OFFICECODE);

            string cmd = "SELECT * FROM [dbo].[FT_UserAsset] (" + QuoteStr(d.UCODE) + ") where 1 = 1";

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                cmd += " and COMPANY = " + QuoteStr(d.COMPANY);
                
            }

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET d, string EditUser = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            //param.Add("@UserCode", d.UserCode);

            string cmd = "";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET d, string EditUser = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            //param.Add("@UserCode", d.UserCode);

            string cmd = "";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET> CheckApprover(ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@USERCODE", d.UCODE);

            string cmd = "SELECT * FROM [dbo].[FT_UserAsset] (" + QuoteStr(d.UCODE) + ")" + 
                $" where A_Approv = 'Y'";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET>(cmd, param).ToList();
            return res;
        }


    }
}
