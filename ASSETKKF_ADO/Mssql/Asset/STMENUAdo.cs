using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class STMENUAdo : Base
    {
        private static STMENUAdo instant;

        public static STMENUAdo GetInstant()
        {
            if (instant == null) instant = new STMENUAdo();
            return instant;
        }

        

        private STMENUAdo()
        {
            
        }

        public int Insert(STMENU d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MENUCODE", d.MENUCODE);
            param.Add("@MENUNAME", d.MENUNAME);
            param.Add("@FLAG", "1");
            param.Add("@INPID", d.INPID);

            string cmd = "INSERT INTO STMENU (MENUCODE, MENUNAME, FLAG, INPID, INPDT) " +
                "VALUES (@MENUCODE, @MENUNAME, @FLAG, @INPID, GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param, conStr);
            return res;
        }

        public int Update(STMENU d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MENUCODE", d.MENUCODE);
            param.Add("@MENUNAME", d.MENUNAME);
            param.Add("@FLAG", d.FLAG);
            param.Add("@INPID", d.INPID);

            string cmd = "Update STMENU set MENUNAME = @MENUNAME , FLAG = @FLAG, INPID = @INPID, INPDT = GETDATE() " +
                "Where MENUCODE = @MENUCODE ;";
            var res = ExecuteNonQuery(transac, cmd, param, conStr);
            return res;
        }

        public int Delete(STMENU d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MENUCODE", d.MENUCODE);

            string cmd = "Delete from STMENU " +
                "Where MENUCODE = @MENUCODE ;";
            var res = ExecuteNonQuery(transac, cmd, param, conStr);
            return res;
        }

        public List<STMENU> Search(STMENU d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            

            sql = "Select * from STMENU Where 1 = 1";

            if (!String.IsNullOrEmpty(d.MENUCODE))
            {
                sql += " and MENUCODE = @MENUCODE ";
                param.Add("@MENUCODE", d.MENUCODE);
            }

            if (!String.IsNullOrEmpty(d.MENUNAME))
            {
                sql += " and MENUNAME = @MENUNAME ";
                param.Add("@MENUNAME", d.MENUNAME);
            }

            var res = Query<STMENU>(sql, param, conStr).ToList();
            return res;
        }


        public List<STMENU> ListActive(STMENU d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "Select * from STMENU Where FLAG = '1' ";

            if (!String.IsNullOrEmpty(d.MENUCODE))
            {
                sql += " and MENUCODE = @MENUCODE ";
                param.Add("@MENUCODE", d.MENUCODE);
            }

            if (!String.IsNullOrEmpty(d.MENUNAME))
            {
                sql += " and MENUNAME = @MENUNAME ";
                param.Add("@MENUNAME", d.MENUNAME);
            }

            var res = Query<STMENU>(sql, param, conStr).ToList();
            return res;
        }

       


    }
}
