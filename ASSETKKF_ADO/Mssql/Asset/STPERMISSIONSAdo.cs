using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Response.Permissions;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class STPERMISSIONSAdo : Base
    {
        private static STPERMISSIONSAdo instant;

        public static STPERMISSIONSAdo GetInstant()
        {
            if (instant == null) instant = new STPERMISSIONSAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private STPERMISSIONSAdo()
        {
        }

        public int Insert(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MENUCODE", d.MENUCODE);
            param.Add("@GUCODE", d.GUCODE);
            param.Add("@COMPANY", d.COMPANY);
            param.Add("@P_ACCESS", d.P_ACCESS);
            param.Add("@P_MANAGE", d.P_MANAGE);
            param.Add("@P_DELETE", d.P_DELETE);
            param.Add("@P_APPROVE", d.P_APPROVE);
            param.Add("@P_EXPORT", d.P_APPROVE);
            param.Add("@INPID", d.INPID);

            string cmd = "INSERT INTO STPERMISSIONS (MENUCODE, GUCODE, COMPANY, P_ACCESS,  P_MANAGE, P_DELETE, P_APPROVE,P_EXPORT, INPID, INPDT) " +
                "VALUES (@MENUCODE, @GUCODE, @COMPANY, @P_ACCESS, @P_MANAGE, @P_DELETE, @P_APPROVE, @P_EXPORT, @INPID, GETDATE());";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Update(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MENUCODE", d.MENUCODE);
            param.Add("@GUCODE", d.GUCODE);
            param.Add("@COMPANY", d.COMPANY);
            param.Add("@INPID", d.INPID);

            string cmd = "Update STPERMISSIONS SET " +
                "  INPID = @INPID, INPDT = GETDATE()";

            if (!String.IsNullOrEmpty(d.P_ACCESS))
            {
                cmd += " , P_ACCESS = @P_ACCESS ";
                param.Add("@P_ACCESS", d.P_ACCESS);
            }

            if (!String.IsNullOrEmpty(d.P_MANAGE))
            {
                cmd += " , P_MANAGE = @P_MANAGE ";
                param.Add("@P_MANAGE", d.P_MANAGE);
            }

            if (!String.IsNullOrEmpty(d.P_DELETE))
            {
                cmd += " , P_DELETE = @P_DELETE ";
                param.Add("@P_DELETE", d.P_DELETE);
            }

            if (!String.IsNullOrEmpty(d.P_APPROVE))
            {
                cmd += " ,P_APPROVE = @P_APPROVE ";
                param.Add("@P_APPROVE", d.P_APPROVE);
            }

            if (!String.IsNullOrEmpty(d.P_EXPORT))
            {
                cmd += " , P_EXPORT = @P_EXPORT ";
                param.Add("@P_EXPORT", d.P_EXPORT);
            }


            cmd += " Where MENUCODE = @MENUCODE and GUCODE = @GUCODE and COMPANY =@COMPANY ;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MENUCODE", d.MENUCODE);
            param.Add("@GUCODE", d.GUCODE);
            param.Add("@COMPANY", d.COMPANY);
            param.Add("@INPID", d.INPID);

            string cmd = "Delete From STPERMISSIONS  " +
                " Where MENUCODE = @MENUCODE and GUCODE = @GUCODE and COMPANY =@COMPANY ;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int DeleteAllByMenu(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@MENUCODE", d.MENUCODE);
            param.Add("@COMPANY", d.COMPANY);

            string cmd = "Delete From STPERMISSIONS  " +
                " Where MENUCODE = @MENUCODE and COMPANY =@COMPANY ;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int DeleteAllByGroup(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@GUCODE", d.GUCODE);
            param.Add("@COMPANY", d.COMPANY);

            string cmd = "Delete From STPERMISSIONS  " +
                " Where GUCODE = @GUCODE and COMPANY =@COMPANY ;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public List<STPERMISSIONS> Get(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "Select * from STPERMISSIONS";
            sql += " Where 1 = 1";

            if (!String.IsNullOrEmpty(d.MENUCODE))
            {
                sql += " and MENUCODE = @MENUCODE ";
                param.Add("@MENUCODE", d.MENUCODE);
            }

            if (!String.IsNullOrEmpty(d.GUCODE))
            {
                sql += " and GUCODE = @GUCODE ";
                param.Add("@GUCODE", d.GUCODE);
            }

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and COMPANY = @COMPANY ";
                param.Add("@COMPANY", d.COMPANY);
            }

            if (!String.IsNullOrEmpty(d.P_ACCESS))
            {
                sql += " and P_ACCESS = @P_ACCESS ";
                param.Add("@P_ACCESS", d.P_ACCESS);
            }

            if (!String.IsNullOrEmpty(d.P_MANAGE))
            {
                sql += " and P_MANAGE = @P_MANAGE ";
                param.Add("@P_MANAGE", d.P_MANAGE);
            }

            if (!String.IsNullOrEmpty(d.P_DELETE))
            {
                sql += " and P_DELETE = @P_DELETE ";
                param.Add("@P_DELETE", d.P_DELETE);
            }

            if (!String.IsNullOrEmpty(d.P_APPROVE))
            {
                sql += " and P_APPROVE = @P_APPROVE ";
                param.Add("@P_APPROVE", d.P_APPROVE);
            }

            if (!String.IsNullOrEmpty(d.P_EXPORT))
            {
                sql += " and P_EXPORT = @P_EXPORT ";
                param.Add("@P_EXPORT", d.P_EXPORT);
            }

            var res = Query<STPERMISSIONS>(sql, param).ToList();
            return res;

        }


        public List<STPERMISSIONSRes> Valid(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "Select * from STPERMISSIONS  as P ";
            sql += " left outer join STMENU as M on M.MENUCODE = P.MENUCODE";
            sql += " Where 1 = 1";

            if (!String.IsNullOrEmpty(d.MENUCODE))
            {
                sql += " and P.MENUCODE = @MENUCODE ";
                param.Add("@MENUCODE", d.MENUCODE);
            }

            if (!String.IsNullOrEmpty(d.GUCODE))
            {
                sql += " and GUCODE = @GUCODE ";
                param.Add("@GUCODE", d.GUCODE);
            }

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                var comp = "";
                comp = "'" + d.COMPANY.Replace(",", "','") + "'";
                sql += " and P.COMPANY in (" + comp + ") ";
                //sql += " and COMPANY = @COMPANY ";
                //param.Add("@COMPANY", d.COMPANY);
            }

            if (!String.IsNullOrEmpty(d.P_ACCESS))
            {
                sql += " and P_ACCESS = @P_ACCESS ";
                param.Add("@P_ACCESS", d.P_ACCESS);
            }

            if (!String.IsNullOrEmpty(d.P_MANAGE))
            {
                sql += " and P_MANAGE = @P_MANAGE ";
                param.Add("@P_MANAGE", d.P_MANAGE);
            }

            if (!String.IsNullOrEmpty(d.P_DELETE))
            {
                sql += " and P_DELETE = @P_DELETE ";
                param.Add("@P_DELETE", d.P_DELETE);
            }

            if (!String.IsNullOrEmpty(d.P_APPROVE))
            {
                sql += " and P_APPROVE = @P_APPROVE ";
                param.Add("@P_APPROVE", d.P_APPROVE);
            }

            if (!String.IsNullOrEmpty(d.P_EXPORT))
            {
                sql += " and P_EXPORT = @P_EXPORT ";
                param.Add("@P_EXPORT", d.P_EXPORT);
            }

            var res = Query<STPERMISSIONSRes>(sql, param).ToList();
            return res;

        }

        public List<STPERMISSIONSRes> Search(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();


            sql = "Select G.GUCODE , G.GUNAME , M.MENUCODE , M.MENUNAME , P.COMPANY";
            sql += " ,P.P_ACCESS ,P.P_MANAGE , P.P_DELETE , P.P_APPROVE, P_EXPORT";
            sql += " from STPERMISSIONS as P ";
            sql += " right outer join STMENU as M on M.MENUCODE = P.MENUCODE";
            sql += " right outer join [FT_STGROUPUSER]() as G  on G.GUCODE = P.GUCODE  and G.COMPANY =  P.COMPANY";
            sql += " Where 1 = 1";

            if (!String.IsNullOrEmpty(d.MENUCODE))
            {
                sql += " and M.MENUCODE = @MENUCODE ";
                param.Add("@MENUCODE", d.MENUCODE);
            }

            if (!String.IsNullOrEmpty(d.GUCODE))
            {
                sql += " and G.GUCODE = @GUCODE ";
                param.Add("@GUCODE", d.GUCODE);
            }

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and P.COMPANY = @COMPANY ";
                param.Add("@COMPANY", d.COMPANY);
            }

            if (!String.IsNullOrEmpty(d.P_ACCESS))
            {
                sql += " and P_ACCESS = @P_ACCESS ";
                param.Add("@P_ACCESS", d.P_ACCESS);
            }

            if (!String.IsNullOrEmpty(d.P_MANAGE))
            {
                sql += " and P_MANAGE = @P_MANAGE ";
                param.Add("@P_MANAGE", d.P_MANAGE);
            }

            if (!String.IsNullOrEmpty(d.P_DELETE))
            {
                sql += " and P_DELETE = @P_DELETE ";
                param.Add("@P_DELETE", d.P_DELETE);
            }

            if (!String.IsNullOrEmpty(d.P_APPROVE))
            {
                sql += " and P_APPROVE = @P_APPROVE ";
                param.Add("@P_APPROVE", d.P_APPROVE);
            }

            if (!String.IsNullOrEmpty(d.P_EXPORT))
            {
                sql += " and P_EXPORT = @P_EXPORT ";
                param.Add("@P_EXPORT", d.P_EXPORT);
            }

            var res = Query<STPERMISSIONSRes>(sql, param).ToList();
            return res;
        }

        public List<STPERMISSIONSRes> ListActive(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();


            sql = "Select G.GUCODE , G.GUNAME , M.MENUCODE , M.MENUNAME , P.COMPANY";
            sql += " ,P.P_ACCESS ,P.P_MANAGE , P.P_DELETE , P.P_APPROVE, P_EXPORT";
            sql += " from STPERMISSIONS as P ";
            sql += " right outer join STMENU as M on M.MENUCODE = P.MENUCODE";
            sql += " right outer join [FT_STGROUPUSER]() as G  on G.GUCODE = P.GUCODE  and G.COMPANY =  P.COMPANY";
            sql += " Where M.FLAG = '1' ";

            if (!String.IsNullOrEmpty(d.MENUCODE))
            {
                sql += " and M.MENUCODE = @MENUCODE ";
                param.Add("@MENUCODE", d.MENUCODE);
            }

            if (!String.IsNullOrEmpty(d.GUCODE))
            {
                sql += " and G.GUCODE = @GUCODE ";
                param.Add("@GUCODE", d.GUCODE);
            }

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and P.COMPANY = @COMPANY ";
                param.Add("@COMPANY", d.COMPANY);
            }

            if (!String.IsNullOrEmpty(d.P_ACCESS))
            {
                sql += " and P_ACCESS = @P_ACCESS ";
                param.Add("@P_ACCESS", d.P_ACCESS);
            }

            if (!String.IsNullOrEmpty(d.P_MANAGE))
            {
                sql += " and P_MANAGE = @P_MANAGE ";
                param.Add("@P_MANAGE", d.P_MANAGE);
            }

            if (!String.IsNullOrEmpty(d.P_DELETE))
            {
                sql += " and P_DELETE = @P_DELETE ";
                param.Add("@P_DELETE", d.P_DELETE);
            }

            if (!String.IsNullOrEmpty(d.P_APPROVE))
            {
                sql += " and P_APPROVE = @P_APPROVE ";
                param.Add("@P_APPROVE", d.P_APPROVE);
            }

            if (!String.IsNullOrEmpty(d.P_EXPORT))
            {
                sql += " and P_EXPORT = @P_EXPORT ";
                param.Add("@P_EXPORT", d.P_EXPORT);
            }

            var res = Query<STPERMISSIONSRes>(sql, param).ToList();
            return res;
        }

        public List<STPERMISSIONSRes> GroupPermissions(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();


            sql = "Select M.MENUCODE , M.MENUNAME , P.COMPANY, max(P.INPDT) as INPDT,max(P.gucode) as gucode,max(G.guname) as guname";
            sql += " ,P.P_ACCESS ,P.P_MANAGE , P.P_DELETE , P.P_APPROVE, P_EXPORT";
            sql += " ,(max(P.INPID) + ' : ' + (select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= max(P.INPID)) ) as INPID";
            sql += " from STPERMISSIONS as P ";
            sql += " right outer join STMENU as M on M.MENUCODE = P.MENUCODE";
            sql += " right outer join [FT_STGROUPUSER]() as G  on G.GUCODE = P.GUCODE  and G.COMPANY =  P.COMPANY";
            sql += " Where M.FLAG = '1' ";

            if (!String.IsNullOrEmpty(d.MENUCODE))
            {
                sql += " and M.MENUCODE = @MENUCODE ";
                param.Add("@MENUCODE", d.MENUCODE);
            }

            if (!String.IsNullOrEmpty(d.GUCODE))
            {
                sql += " and G.GUCODE = @GUCODE ";
                param.Add("@GUCODE", d.GUCODE);
            }

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and P.COMPANY = @COMPANY ";
                param.Add("@COMPANY", d.COMPANY);
            }

            if (!String.IsNullOrEmpty(d.P_ACCESS))
            {
                sql += " and P_ACCESS = @P_ACCESS ";
                param.Add("@P_ACCESS", d.P_ACCESS);
            }

            if (!String.IsNullOrEmpty(d.P_MANAGE))
            {
                sql += " and P_MANAGE = @P_MANAGE ";
                param.Add("@P_MANAGE", d.P_MANAGE);
            }

            if (!String.IsNullOrEmpty(d.P_DELETE))
            {
                sql += " and P_DELETE = @P_DELETE ";
                param.Add("@P_DELETE", d.P_DELETE);
            }

            if (!String.IsNullOrEmpty(d.P_APPROVE))
            {
                sql += " and P_APPROVE = @P_APPROVE ";
                param.Add("@P_APPROVE", d.P_APPROVE);
            }

            if (!String.IsNullOrEmpty(d.P_EXPORT))
            {
                sql += " and P_EXPORT = @P_EXPORT ";
                param.Add("@P_EXPORT", d.P_EXPORT);
            }

            sql += "group by M.MENUCODE , M.MENUNAME , P.COMPANY ,P.P_ACCESS ,P.P_MANAGE , P.P_DELETE , P.P_APPROVE, P_EXPORT ";

            var res = Query<STPERMISSIONSRes>(sql, param).ToList();
            return res;
        }

        public List<STPERMISSIONSRes> GroupUserActive(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();


            sql = "Select G.GUCODE , G.GUNAME , P.COMPANY , max(P.INPDT) as INPDT ";
            sql += " ,(max(P.INPID) + ' : ' + (select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= max(P.INPID)) ) as INPID";
            sql += " from STPERMISSIONS as P ";
            sql += " right outer join STMENU as M on M.MENUCODE = P.MENUCODE";
            sql += " right outer join [FT_STGROUPUSER]() as G  on G.GUCODE = P.GUCODE  and G.COMPANY =  P.COMPANY";
            sql += " Where M.FLAG = '1' ";


            if (!String.IsNullOrEmpty(d.GUCODE))
            {
                sql += " and G.GUCODE = @GUCODE ";
                param.Add("@GUCODE", d.GUCODE);
            }

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and P.COMPANY = @COMPANY ";
                param.Add("@COMPANY", d.COMPANY);
            }

            sql += "group by G.GUCODE , G.GUNAME , P.COMPANY";

            var res = Query<STPERMISSIONSRes>(sql, param).ToList();
            return res;
        }

        public string getGUCODE(STPERMISSIONS d)
        {
            string GUCODE = null;
            var user = ASSETKKF_ADO.Mssql.Asset.STUSERASSETAdo.GetInstant().Search(new ASSETKKF_MODEL.Data.Mssql.Asset.STUSERASSET() { UCODE = d.INPID.Trim() });
            if (user != null) 
            {
                var obj = user.FirstOrDefault();
                GUCODE = obj.GUCODE;
            }


            return GUCODE;

        }

        public List<STGROUPUSER> getGROUPUSER(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "select gucode,guname from [FT_STGROUPUSER]() where 1 =1 ";

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and COMPANY = @COMPANY ";
                param.Add("@COMPANY", d.COMPANY);
            }

            sql += " group by gucode,guname";

            var res = Query<STGROUPUSER>(sql, param).ToList();
            return res;
        }

        public List<STMENU> ListMenu(STPERMISSIONS d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "Select * from STMENU Where FLAG = '1' ";
            sql += " and MENUCODE not in (select MENUCODE from STPERMISSIONS where 1 =1";

            if (!String.IsNullOrEmpty(d.COMPANY))
            {
                sql += " and COMPANY = @COMPANY ";
                param.Add("@COMPANY", d.COMPANY);
            }

            if (!String.IsNullOrEmpty(d.GUCODE))
            {
                sql += " and GUCODE = @GUCODE ";
                param.Add("@GUCODE", d.GUCODE);
            }
            sql += " )";

            var res = Query<STMENU>(sql, param).ToList();
            return res;
        }

    }
}
