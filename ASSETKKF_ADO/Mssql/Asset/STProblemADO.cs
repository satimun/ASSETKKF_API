using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class STProblemADO : Base
    {
        private static STProblemADO instant;

        public static STProblemADO GetInstant()
        {
            if (instant == null) instant = new STProblemADO();
            return instant;
        }

        private string conectStr { get; set; }

        private STProblemADO()
        {
        }

        public List<ProblemList> Search(STProblemReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@COMPANY", d.Company);
            string cmd = "SELECT distinct Pcode,Pname,SACC,FINDY  FROM [dbo].[FT_ASSTProblem] ()";
            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " WHERE COMPANY in (" + comp + ") ";
            }

            var obj = Query<ASSETKKF_MODEL.Data.Mssql.Asset.ASSTProblem>(cmd, param).ToList();

            List<ProblemList> res = new List<ProblemList>();
            if (obj != null && obj.Count > 0)
            {
                obj.ForEach(x => {
                    res.Add(new ProblemList
                    {
                        id = x.Pcode,
                        descriptions = x.Pcode + " : " + x.Pname ,
                        Pcode = x.Pcode,
                        Pname = x.Pname,
                        FINDY = x.FINDY
                    }); ;
                });
            }

            return res;
        }
    }
}
