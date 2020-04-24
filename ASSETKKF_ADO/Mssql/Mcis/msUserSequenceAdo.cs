using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class msUserSequenceAdo : Base
    {
        private static msUserSequenceAdo instant;

        public static msUserSequenceAdo GetInstant()
        {
            if (instant == null) instant = new msUserSequenceAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private msUserSequenceAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.msUserSequence ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence> GetData(ASSETKKF_MODEL.Request.Mcis.msUserSequenceReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@USERCODE", d.USERCODE);
            /*
            param.Add("@STDATE", d.STDATE);
            param.Add("@ENDATE", d.ENDATE);
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@EDIT_TYPE", d.EDIT_TYPE);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");*/

            string cmd = "SELECT * FROM mcis.dbo.msUserSequence " +
            $"WHERE (@USERCODE IS NULL OR USERCODE = @USERCODE) " +
            $"AND STDATE <= DATEADD(D, 0, DATEDIFF(D, 0, GETDATE() )) " +
            $"and (ENDATE is null or ENDATE >= DATEADD(D, 0, DATEDIFF(D, 0, GETDATE() )) )" +
            $"AND EDIT_TYPE = 'A' " +

            /*
            $"  AND (@STDATE IS NULL OR STDATE = @STDATE) " +
            $"  AND (@ENDATE IS NULL OR ENDATE = @ENDATE) " +
            $"  AND (@USER_ID IS NULL OR USER_ID = @USER_ID) " +
            $"  AND (@USER_DATE IS NULL OR USER_DATE = @USER_DATE) " +
            $"  AND (@EDIT_TYPE IS NULL OR EDIT_TYPE = @EDIT_TYPE) " +
            //$"AND (USERCODE LIKE @txtSearch OR USERCODE LIKE @txtSearch) " +  */
            "ORDER BY  USERCODE;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence> Search(ASSETKKF_MODEL.Request.Mcis.msUserSequenceReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@USERCODEIsNull", d.USERCODE.ListNull()); 
            param.Add("@STDATEIsNull", d.STDATE.ListNull()); 
            param.Add("@ENDATEIsNull", d.ENDATE.ListNull()); 
            param.Add("@USER_IDIsNull", d.USER_ID.ListNull()); 
            param.Add("@USER_DATEIsNull", d.USER_DATE.ListNull()); 
            param.Add("@EDIT_TYPEIsNull", d.EDIT_TYPE.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.msUserSequence " +
            $"WHERE (@USERCODEIsNull IS NULL OR USERCODE IN ('{ d.USERCODE.Join("','") }')) " +
            $"AND (@STDATEIsNull IS NULL OR STDATE IN ('{ d.STDATE.Join("','") }')) " +
            $"AND (@ENDATEIsNull IS NULL OR ENDATE IN ('{ d.ENDATE.Join("','") }')) " +
            $"AND (@USER_IDIsNull IS NULL OR USER_ID IN ('{ d.USER_ID.Join("','") }')) " +
            $"AND (@USER_DATEIsNull IS NULL OR USER_DATE IN ('{ d.USER_DATE.Join("','") }')) " +
            $"AND (@EDIT_TYPEIsNull IS NULL OR EDIT_TYPE IN ('{ d.EDIT_TYPE.Join("','") }')) " +
            $"AND (USERCODE LIKE @txtSearch OR USERCODE LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@USERCODE", d.USERCODE);
            param.Add("@STDATE", d.STDATE);
            param.Add("@ENDATE", d.ENDATE);
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@EDIT_TYPE", d.EDIT_TYPE);
            string cmd = "INSERT INTO mcis.dbo.msUserSequence " +
            $"      (USERCODE, STDATE, ENDATE, USER_ID, USER_DATE, EDIT_TYPE) " +
            $"VALUES(@USERCODE, @STDATE, @ENDATE, @USER_ID, @USER_DATE, @EDIT_TYPE); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@USERCODE", d.USERCODE.GetValue());
            param.Add("@STDATE", d.STDATE);
            param.Add("@ENDATE", d.ENDATE);
            param.Add("@USER_ID", d.USER_ID.GetValue());
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@EDIT_TYPE", d.EDIT_TYPE.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.msUserSequence "+
            "SET USERCODE = @.USERCODE "+ 
            " , STDATE = @.STDATE "+ 
            " , ENDATE = @.ENDATE "+ 
            " , USER_ID = @.USER_ID "+ 
            " , USER_DATE = @.USER_DATE "+ 
            " , EDIT_TYPE = @.EDIT_TYPE "+ 
            "WHERE USERCODE = @.USERCODE "+ 
            " AND STDATE = @.STDATE "+ 
            " AND ENDATE = @.ENDATE "+ 
            " AND USER_ID = @.USER_ID "+ 
            " AND USER_DATE = @.USER_DATE "+ 
            " AND EDIT_TYPE = @.EDIT_TYPE "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@USERCODE", d.USERCODE.GetValue());
            param.Add("@STDATE", d.STDATE);
            param.Add("@ENDATE", d.ENDATE);
            param.Add("@USER_ID", d.USER_ID.GetValue());
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@EDIT_TYPE", d.EDIT_TYPE.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.msUserSequence "+
            "WHERE USERCODE = @.USERCODE "+ 
            " AND STDATE = @.STDATE "+ 
            " AND ENDATE = @.ENDATE "+ 
            " AND USER_ID = @.USER_ID "+ 
            " AND USER_DATE = @.USER_DATE "+ 
            " AND EDIT_TYPE = @.EDIT_TYPE "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Save(ASSETKKF_MODEL.Data.Mssql.Mcis.msUserSequence d, string userID = "", SqlTransaction transac = null)
        {
            /*
            if (d.ID.HasValue)
            {
                Update(d, userID, transac);
                return d.ID.Value;
            }
            else
            {
                var req = new ASSETKKF_MODEL.Request.Mcis.msUserSequenceReq()
            req.USERCODE= new List<string>(); 
            req.USERCODE.Add(d.USERCODE);

            req.STDATE= new List<string>(); 
            req.STDATE.Add(d.STDATE);

            req.ENDATE= new List<string>(); 
            req.ENDATE.Add(d.ENDATE);

            req.USER_ID= new List<string>(); 
            req.USER_ID.Add(d.USER_ID);

            req.USER_DATE= new List<string>(); 
            req.USER_DATE.Add(d.USER_DATE);

            req.EDIT_TYPE= new List<string>(); 
            req.EDIT_TYPE.Add(d.EDIT_TYPE);

                var _msUserSequence = Search(req, transac).FirstOrDefault();
                if (msUserSequence != null)
                {
                 throw new Exception(Model.Enum.ErrorCode.V001.ToString());
                 
                } 
                
            return Insert(d, userID, transac);
            */
            return Insert(d, userID, transac);
        }
    }







}
