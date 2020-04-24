using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Core.Util;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class msWorkStationAdo : Base
    {
        private static msWorkStationAdo instant;

        private static msWorkStationAdo GetInstant()
        {
            if (instant == null) instant = new msWorkStationAdo();
            return instant;
        }

    private string conectStr { get; set; }

    private msWorkStationAdo() { }


    public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation> ListActive()
    {
        string cmd = " SELECT * FROM .dbo.msWorkStation ";

        var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation>(cmd, null).ToList();

        return res;
    }

    public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation> GetData(ASSETKKF_MODEL.Request.Mcis.msWorkStationReq d, SqlTransaction transac = null)
    {
        DynamicParameters param = new DynamicParameters();
        param.Add("@WorkStationCd", d.WorkStationCd);
        param.Add("@WorkStationNm", d.WorkStationNm);
        param.Add("@WSProfit", d.WSProfit);
        param.Add("@WSProfit_NM", d.WSProfit_NM);
        param.Add("@SelProfit", d.SelProfit);
        param.Add("@CANCELFLAG", d.CANCELFLAG);
      //  param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

        string cmd = "SELECT * FROM .dbo.msWorkStation " +
        $"WHERE (@WorkStationCd IS NULL OR WorkStationCd = d.WorkStationCd) " +
        $"  AND (@WorkStationNm IS NULL OR WorkStationNm = d.WorkStationNm) " +
        $"  AND (@WSProfit IS NULL OR WSProfit = d.WSProfit) " +
        $"  AND (@WSProfit_NM IS NULL OR WSProfit_NM = d.WSProfit_NM) " +
        $"  AND (@SelProfit IS NULL OR SelProfit = d.SelProfit) " +
        $"  AND (@CANCELFLAG IS NULL OR CANCELFLAG = d.CANCELFLAG) " +
      //  $"AND (WorkStationCd LIKE @txtSearch OR WorkStationCd LIKE @txtSearch) " +
        "ORDER BY aFieldFirstName;";
        var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation>(cmd, param).ToList();
        return res;
    }

    public List<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation> Search(ASSETKKF_MODEL.Request.Mcis.msWorkStationReq d, SqlTransaction transac = null)
    { 
        DynamicParameters param = new DynamicParameters();
            /*
              param.Add("@WorkStationCdIsNull", d.WorkStationCd.ListNull());
              param.Add("@WorkStationNmIsNull", d.WorkStationNm.ListNull());
              param.Add("@WSProfitIsNull", d.WSProfit.ListNull());
              param.Add("@WSProfit_NMIsNull", d.WSProfit_NM.ListNull());
              param.Add("@SelProfitIsNull", d.SelProfit.ListNull());
              param.Add("@CANCELFLAGIsNull", d.CANCELFLAG.ListNull());
              param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
              */
            string cmd = "";
            /*
        string cmd = "SELECT * FROM .dbo.msWorkStation " +
        $"WHERE (@WorkStationCdIsNull IS NULL OR WorkStationCd IN ('{ d.WorkStationCd.Join("','") }')) " +
        $"AND (@WorkStationNmIsNull IS NULL OR WorkStationNm IN ('{ d.WorkStationNm.Join("','") }')) " +
        $"AND (@WSProfitIsNull IS NULL OR WSProfit IN ('{ d.WSProfit.Join("','") }')) " +
        $"AND (@WSProfit_NMIsNull IS NULL OR WSProfit_NM IN ('{ d.WSProfit_NM.Join("','") }')) " +
        $"AND (@SelProfitIsNull IS NULL OR SelProfit IN ('{ d.SelProfit.Join("','") }')) " +
        $"AND (@CANCELFLAGIsNull IS NULL OR CANCELFLAG IN ('{ d.CANCELFLAG.Join("','") }')) " +
        $"AND (WorkStationCd LIKE @txtSearch OR WorkStationCd LIKE @txtSearch) " +
        "ORDER BY aFieldFirstName;";
        */
        var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation>(cmd, param).ToList();
        return res;
    }

    public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation d, string userID = "")
    {
        var param = new Dapper.DynamicParameters();
        param.Add("@WorkStationCd", d.WorkStationCd);
        param.Add("@WorkStationNm", d.WorkStationNm);
        param.Add("@WSProfit", d.WSProfit);
        param.Add("@WSProfit_NM", d.WSProfit_NM);
        param.Add("@SelProfit", d.SelProfit);
        param.Add("@CANCELFLAG", d.CANCELFLAG);
        string cmd = "INSERT INTO .dbo.msWorkStation " +
        $"      (WorkStationCd, WorkStationNm, WSProfit, WSProfit_NM, SelProfit, CANCELFLAG) " +
        $"VALUES(@WorkStationCd, @WorkStationNm, @WSProfit, @WSProfit_NM, @SelProfit, @CANCELFLAG); " +
        $"SELECT SCOPE_IDENTITY();";
        return ExecuteScalar<int>(cmd, param);
    }

    public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation d, string userID = "", SqlTransaction transac = null)
    {
        var param = new Dapper.DynamicParameters();
        param.Add("@WorkStationCd", d.WorkStationCd.GetValue());
        param.Add("@WorkStationNm", d.WorkStationNm.GetValue());
        param.Add("@WSProfit", d.WSProfit.GetValue());
        param.Add("@WSProfit_NM", d.WSProfit_NM.GetValue());
        param.Add("@SelProfit", d.SelProfit.GetValue());
        param.Add("@CANCELFLAG", d.CANCELFLAG.GetValue());
        string cmd = "";
        /*
               cmd = UPDATE .dbo.msWorkStation "+
        "SET WorkStationCd = @.WorkStationCd "+ 
        " , WorkStationNm = @.WorkStationNm "+ 
        " , WSProfit = @.WSProfit "+ 
        " , WSProfit_NM = @.WSProfit_NM "+ 
        " , SelProfit = @.SelProfit "+ 
        " , CANCELFLAG = @.CANCELFLAG "+ 
        "WHERE WorkStationCd = @.WorkStationCd "+ 
        " AND WorkStationNm = @.WorkStationNm "+ 
        " AND WSProfit = @.WSProfit "+ 
        " AND WSProfit_NM = @.WSProfit_NM "+ 
        " AND SelProfit = @.SelProfit "+ 
        " AND CANCELFLAG = @.CANCELFLAG "+ 
        " "; 
        */

        var res = ExecuteNonQuery(transac, cmd, param);
        return res;
    }

    public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.msWorkStation d, string userID = "", SqlTransaction transac = null)
    {
        var param = new Dapper.DynamicParameters();
        param.Add("@WorkStationCd", d.WorkStationCd.GetValue());
        param.Add("@WorkStationNm", d.WorkStationNm.GetValue());
        param.Add("@WSProfit", d.WSProfit.GetValue());
        param.Add("@WSProfit_NM", d.WSProfit_NM.GetValue());
        param.Add("@SelProfit", d.SelProfit.GetValue());
        param.Add("@CANCELFLAG", d.CANCELFLAG.GetValue());
        string cmd = "";
        /*
               cmd = DELETE FROM .dbo.msWorkStation "+
        "WHERE WorkStationCd = @.WorkStationCd "+ 
        " AND WorkStationNm = @.WorkStationNm "+ 
        " AND WSProfit = @.WSProfit "+ 
        " AND WSProfit_NM = @.WSProfit_NM "+ 
        " AND SelProfit = @.SelProfit "+ 
        " AND CANCELFLAG = @.CANCELFLAG "+ 
        " "; 
        */

        var res = ExecuteNonQuery(transac, cmd, param);
        return res;
    }












}
}
