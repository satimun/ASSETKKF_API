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

        public static STPOSITASSETADO GetInstant(string conStr = null)
        {
            if (instant == null) instant = new STPOSITASSETADO(conStr);
            return instant;
        }

        private string conectStr { get; set; }

        private STPOSITASSETADO(string conStr = null)
        {
            conectStr = conStr;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Asset.STPOSITASSET> GetSTPOSITASSETLists(List<String> lstDepLike, String company, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            int i = 0;
            int j = 0;
            sql = "Select distinct POSITCODE,POSITDESC from  FR_STPOSITASSET()";
            sql += " where company = '" + company + "'";
            if (lstDepLike != null && lstDepLike.Count > 0)
            {
                //หน่วยงาน
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

                //พื้นที่
                sql += " and (";
                foreach (string s in lstDepLike)
                {
                    sql += "( '" + s + "'" + " = case when isnull(POSITCODE,'') <> '' then   SUBSTRING(POSITCODE,1,2) else '' end )";
                    if (j < lstDepLike.Count - 1)
                    {
                        sql += " or";
                    }
                    j++;
                }
                sql += " )";
            }

            sql += " order by POSITCODE";



            var res = Query<ASSETKKF_MODEL.Data.Mssql.Asset.STPOSITASSET>(sql, param, conectStr).ToList();
            res.ForEach(x => {
                x.id = x.POSITCODE;
                x.descriptions = x.POSITCODE + " : " + x.POSITDESC;
            });

            return res;
        }

    }
}
