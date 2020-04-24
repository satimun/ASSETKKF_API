using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class msWorkStationGrpAdo : Base
    {
        private static msWorkStationGrpAdo instant;

        public static msWorkStationGrpAdo GetInstant()
        {
            if (instant == null) instant = new msWorkStationGrpAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private msWorkStationGrpAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.msWorkStationGrp ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp> GetData(ASSETKKF_MODEL.Request.Mcis.msWorkStationGrpReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@ExtGrp_Flag", d.ExtGrp_Flag);
            param.Add("@MulEmp_flag", d.MulEmp_flag);
            /*
            param.Add("@WorkStationGrpNm", d.WorkStationGrpNm);
            param.Add("@DLCost", d.DLCost);
            param.Add("@FOHCost", d.FOHCost);
            param.Add("@MDCost", d.MDCost);
            param.Add("@WorkStationCD", d.WorkStationCD);
            param.Add("@LastUpdUsr", d.LastUpdUsr);
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@UnionDLCost", d.UnionDLCost);
            param.Add("@WorkStationNM", d.WorkStationNM);
            param.Add("@MulEmp_flag", d.MulEmp_flag);
            
           param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "SELECT * FROM mcis.dbo.msWorkStationGrp " +
            $"WHERE   ISNULL(WorkStationGrpCd,'') = ISNULL(@WorkStationGrpCd,'')  " +
            $"  AND (@ExtGrp_Flag IS NULL OR ExtGrp_Flag = @ExtGrp_Flag) " +
            $"  AND (@MulEmp_flag IS NULL OR MulEmp_flag = @MulEmp_flag) " +
            /*$"  AND (@WorkStationGrpNm IS NULL OR WorkStationGrpNm = @WorkStationGrpNm) " +
            $"  AND (@DLCost IS NULL OR DLCost = @DLCost) " +
            $"  AND (@FOHCost IS NULL OR FOHCost = @FOHCost) " +
            $"  AND (@MDCost IS NULL OR MDCost = @MDCost) " +
            $"  AND (@WorkStationCD IS NULL OR WorkStationCD = @WorkStationCD) " +
            $"  AND (@LastUpdUsr IS NULL OR LastUpdUsr = @LastUpdUsr) " +
            $"  AND (@LastUpdDt IS NULL OR LastUpdDt = @LastUpdDt) " +
            $"  AND (@UnionDLCost IS NULL OR UnionDLCost = @UnionDLCost) " +
            $"  AND (@WorkStationNM IS NULL OR WorkStationNM = @WorkStationNM) " +
            
           
            //$"AND (WorkStationGrpCd LIKE @txtSearch OR WorkStationGrpCd LIKE @txtSearch) " +  
            */
            "ORDER BY  WorkStationGrpCd;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp> Search(ASSETKKF_MODEL.Request.Mcis.msWorkStationGrpReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@WorkStationGrpCdIsNull", d.WorkStationGrpCd.ListNull()); 
            param.Add("@WorkStationGrpNmIsNull", d.WorkStationGrpNm.ListNull()); 
            param.Add("@DLCostIsNull", d.DLCost.ListNull()); 
            param.Add("@FOHCostIsNull", d.FOHCost.ListNull()); 
            param.Add("@MDCostIsNull", d.MDCost.ListNull()); 
            param.Add("@WorkStationCDIsNull", d.WorkStationCD.ListNull()); 
            param.Add("@LastUpdUsrIsNull", d.LastUpdUsr.ListNull()); 
            param.Add("@LastUpdDtIsNull", d.LastUpdDt.ListNull()); 
            param.Add("@UnionDLCostIsNull", d.UnionDLCost.ListNull()); 
            param.Add("@WorkStationNMIsNull", d.WorkStationNM.ListNull()); 
            param.Add("@MulEmp_flagIsNull", d.MulEmp_flag.ListNull()); 
            param.Add("@ExtGrp_FlagIsNull", d.ExtGrp_Flag.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.msWorkStationGrp " +
            $"WHERE (@WorkStationGrpCdIsNull IS NULL OR WorkStationGrpCd IN ('{ d.WorkStationGrpCd.Join("','") }')) " +
            $"AND (@WorkStationGrpNmIsNull IS NULL OR WorkStationGrpNm IN ('{ d.WorkStationGrpNm.Join("','") }')) " +
            $"AND (@DLCostIsNull IS NULL OR DLCost IN ('{ d.DLCost.Join("','") }')) " +
            $"AND (@FOHCostIsNull IS NULL OR FOHCost IN ('{ d.FOHCost.Join("','") }')) " +
            $"AND (@MDCostIsNull IS NULL OR MDCost IN ('{ d.MDCost.Join("','") }')) " +
            $"AND (@WorkStationCDIsNull IS NULL OR WorkStationCD IN ('{ d.WorkStationCD.Join("','") }')) " +
            $"AND (@LastUpdUsrIsNull IS NULL OR LastUpdUsr IN ('{ d.LastUpdUsr.Join("','") }')) " +
            $"AND (@LastUpdDtIsNull IS NULL OR LastUpdDt IN ('{ d.LastUpdDt.Join("','") }')) " +
            $"AND (@UnionDLCostIsNull IS NULL OR UnionDLCost IN ('{ d.UnionDLCost.Join("','") }')) " +
            $"AND (@WorkStationNMIsNull IS NULL OR WorkStationNM IN ('{ d.WorkStationNM.Join("','") }')) " +
            $"AND (@MulEmp_flagIsNull IS NULL OR MulEmp_flag IN ('{ d.MulEmp_flag.Join("','") }')) " +
            $"AND (@ExtGrp_FlagIsNull IS NULL OR ExtGrp_Flag IN ('{ d.ExtGrp_Flag.Join("','") }')) " +
            $"AND (WorkStationGrpCd LIKE @txtSearch OR WorkStationGrpCd LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd);
            param.Add("@WorkStationGrpNm", d.WorkStationGrpNm);
            param.Add("@DLCost", d.DLCost);
            param.Add("@FOHCost", d.FOHCost);
            param.Add("@MDCost", d.MDCost);
            param.Add("@WorkStationCD", d.WorkStationCD);
            param.Add("@LastUpdUsr", d.LastUpdUsr);
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@UnionDLCost", d.UnionDLCost);
            param.Add("@WorkStationNM", d.WorkStationNM);
            param.Add("@MulEmp_flag", d.MulEmp_flag);
            param.Add("@ExtGrp_Flag", d.ExtGrp_Flag);
            string cmd = "INSERT INTO mcis.dbo.msWorkStationGrp " +
            $"      (WorkStationGrpCd, WorkStationGrpNm, DLCost, FOHCost, MDCost, WorkStationCD, LastUpdUsr, LastUpdDt, UnionDLCost, WorkStationNM, MulEmp_flag, ExtGrp_Flag) " +
            $"VALUES(@WorkStationGrpCd, @WorkStationGrpNm, @DLCost, @FOHCost, @MDCost, @WorkStationCD, @LastUpdUsr, @LastUpdDt, @UnionDLCost, @WorkStationNM, @MulEmp_flag, @ExtGrp_Flag); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@WorkStationGrpNm", d.WorkStationGrpNm.GetValue());
            param.Add("@DLCost", d.DLCost);
            param.Add("@FOHCost", d.FOHCost);
            param.Add("@MDCost", d.MDCost);
            param.Add("@WorkStationCD", d.WorkStationCD.GetValue());
            param.Add("@LastUpdUsr", d.LastUpdUsr.GetValue());
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@UnionDLCost", d.UnionDLCost);
            param.Add("@WorkStationNM", d.WorkStationNM.GetValue());
            param.Add("@MulEmp_flag", d.MulEmp_flag.GetValue());
            param.Add("@ExtGrp_Flag", d.ExtGrp_Flag.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.msWorkStationGrp "+
            "SET WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " , WorkStationGrpNm = @.WorkStationGrpNm "+ 
            " , DLCost = @.DLCost "+ 
            " , FOHCost = @.FOHCost "+ 
            " , MDCost = @.MDCost "+ 
            " , WorkStationCD = @.WorkStationCD "+ 
            " , LastUpdUsr = @.LastUpdUsr "+ 
            " , LastUpdDt = @.LastUpdDt "+ 
            " , UnionDLCost = @.UnionDLCost "+ 
            " , WorkStationNM = @.WorkStationNM "+ 
            " , MulEmp_flag = @.MulEmp_flag "+ 
            " , ExtGrp_Flag = @.ExtGrp_Flag "+ 
            "WHERE WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND WorkStationGrpNm = @.WorkStationGrpNm "+ 
            " AND DLCost = @.DLCost "+ 
            " AND FOHCost = @.FOHCost "+ 
            " AND MDCost = @.MDCost "+ 
            " AND WorkStationCD = @.WorkStationCD "+ 
            " AND LastUpdUsr = @.LastUpdUsr "+ 
            " AND LastUpdDt = @.LastUpdDt "+ 
            " AND UnionDLCost = @.UnionDLCost "+ 
            " AND WorkStationNM = @.WorkStationNM "+ 
            " AND MulEmp_flag = @.MulEmp_flag "+ 
            " AND ExtGrp_Flag = @.ExtGrp_Flag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStationGrp d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkStationGrpCd", d.WorkStationGrpCd.GetValue());
            param.Add("@WorkStationGrpNm", d.WorkStationGrpNm.GetValue());
            param.Add("@DLCost", d.DLCost);
            param.Add("@FOHCost", d.FOHCost);
            param.Add("@MDCost", d.MDCost);
            param.Add("@WorkStationCD", d.WorkStationCD.GetValue());
            param.Add("@LastUpdUsr", d.LastUpdUsr.GetValue());
            param.Add("@LastUpdDt", d.LastUpdDt);
            param.Add("@UnionDLCost", d.UnionDLCost);
            param.Add("@WorkStationNM", d.WorkStationNM.GetValue());
            param.Add("@MulEmp_flag", d.MulEmp_flag.GetValue());
            param.Add("@ExtGrp_Flag", d.ExtGrp_Flag.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.msWorkStationGrp "+
            "WHERE WorkStationGrpCd = @.WorkStationGrpCd "+ 
            " AND WorkStationGrpNm = @.WorkStationGrpNm "+ 
            " AND DLCost = @.DLCost "+ 
            " AND FOHCost = @.FOHCost "+ 
            " AND MDCost = @.MDCost "+ 
            " AND WorkStationCD = @.WorkStationCD "+ 
            " AND LastUpdUsr = @.LastUpdUsr "+ 
            " AND LastUpdDt = @.LastUpdDt "+ 
            " AND UnionDLCost = @.UnionDLCost "+ 
            " AND WorkStationNM = @.WorkStationNM "+ 
            " AND MulEmp_flag = @.MulEmp_flag "+ 
            " AND ExtGrp_Flag = @.ExtGrp_Flag "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }




    }
}
