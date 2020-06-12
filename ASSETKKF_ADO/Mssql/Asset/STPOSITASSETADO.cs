using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class STPOSITASSETADO : Base
    {
        private static STPOSITASSETADO instant;

        public static STPOSITASSETADO GetInstant()
        {
            if (instant == null) instant = new STPOSITASSETADO();
            return instant;
        }

        private string conectStr { get; set; }

        private STPOSITASSETADO()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.STPOSITASSET> GetSTPOSITASSETLists(List<String> lstDepLike, String company, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            int i = 0;
            sql = "Select * from  FR_STPOSITASSET()";
            sql += " where company = '" + company + "'";
            if (lstDepLike != null && lstDepLike.Count > 0)
            {
                sql += " and (";
                foreach (string s in lstDepLike)
                {
                    sql += "( '" + s + "'" + " = case when isnull(DEPCODEOL,'') <> '' then   SUBSTRING(DEPCODEOL,1,2) else '' end )";
                    if (i < lstDepLike.Count - 1)
                    {
                        sql += " or";
                    }
                    i++;
                }
                sql += " )";
            }
            

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.STPOSITASSET>(sql, param).ToList();
            res.ForEach(x => {
                x.id = x.POSITCODE;
                x.descriptions = x.POSITCODE + " : " + x.POSITDESC;
            });

            return res;
        }

    }
}
