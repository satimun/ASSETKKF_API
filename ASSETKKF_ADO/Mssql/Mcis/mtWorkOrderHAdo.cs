using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Core.Util;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class mtWorkOrderHAdo : Base
    {
        private static mtWorkOrderHAdo instant;

        public static mtWorkOrderHAdo GetInstant()
        {
            if (instant == null) instant = new mtWorkOrderHAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private mtWorkOrderHAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.mtWorkOrderH ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH> GetData(ASSETKKF_MODEL.Request.Mcis.mtWorkOrderHReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkOrderID", d.WorkOrderID);
            param.Add("@WorkOrderStatus", d.WorkOrderStatus);

            /*
            param.Add("@MchProjectID", d.MchProjectID);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@WorkOrderDesc", d.WorkOrderDesc);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@FMDeptCd", d.FMDeptCd);
            param.Add("@CreatedDate", d.CreatedDate);
           
            param.Add("@FstRcvrWs", d.FstRcvrWs);
            param.Add("@PrintFlg", d.PrintFlg);
            param.Add("@AmtUnit", d.AmtUnit);
            param.Add("@DocListNo", d.DocListNo);
            param.Add("@MainWS", d.MainWS);
            param.Add("@SendStock", d.SendStock);
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@Date_Open", d.Date_Open);
            param.Add("@PDate_Cls", d.PDate_Cls);
            param.Add("@RDate_cls", d.RDate_cls);
            param.Add("@CreateSPAuto", d.CreateSPAuto);
            param.Add("@DM_COST", d.DM_COST);
            param.Add("@DM_FOH", d.DM_FOH);
            param.Add("@SEQUENCE_COST", d.SEQUENCE_COST);
            param.Add("@SEQUENCE_PROFIT", d.SEQUENCE_PROFIT);
            param.Add("@TOTPRC", d.TOTPRC);
            param.Add("@TOTCOST", d.TOTCOST);
            param.Add("@DM_COST_VERSION", d.DM_COST_VERSION);
            param.Add("@DEL_FLAG", d.DEL_FLAG);
            param.Add("@MATDAYWANT", d.MATDAYWANT);
            param.Add("@SEQUENCE_ACT", d.SEQUENCE_ACT);
            param.Add("@TOTCOST_ACT", d.TOTCOST_ACT);
            param.Add("@SEQUENCE_DIF", d.SEQUENCE_DIF);
            param.Add("@PROFIT_BEG", d.PROFIT_BEG);
            param.Add("@STATION_GRP", d.STATION_GRP);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "SELECT * FROM mcis.dbo.mtWorkOrderH " +
            $"WHERE  WorkOrderID = @WorkOrderID  " +
            $"  AND  WorkOrderStatus = @WorkOrderStatus  " +
            /*
            $"  AND (@MchProjectID IS NULL OR MchProjectID = @MchProjectID) " +
            $"  AND (@DrawingCd IS NULL OR DrawingCd = @DrawingCd) " +
            $"  AND (@WorkOrderDesc IS NULL OR WorkOrderDesc = @WorkOrderDesc) " +
            $"  AND (@QtyAmt IS NULL OR QtyAmt = @QtyAmt) " +
            $"  AND (@FMDeptCd IS NULL OR FMDeptCd = @FMDeptCd) " +
            $"  AND (@CreatedDate IS NULL OR CreatedDate = @CreatedDate) " +
            
            $"  AND (@FstRcvrWs IS NULL OR FstRcvrWs = @FstRcvrWs) " +
            $"  AND (@PrintFlg IS NULL OR PrintFlg = @PrintFlg) " +
            $"  AND (@AmtUnit IS NULL OR AmtUnit = @AmtUnit) " +
            $"  AND (@DocListNo IS NULL OR DocListNo = @DocListNo) " +
            $"  AND (@MainWS IS NULL OR MainWS = @MainWS) " +
            $"  AND (@SendStock IS NULL OR SendStock = @SendStock) " +
            $"  AND (@USER_ID IS NULL OR USER_ID = @USER_ID) " +
            $"  AND (@USER_DATE IS NULL OR USER_DATE = @USER_DATE) " +
            $"  AND (@Date_Open IS NULL OR Date_Open = @Date_Open) " +
            $"  AND (@PDate_Cls IS NULL OR PDate_Cls = @PDate_Cls) " +
            $"  AND (@RDate_cls IS NULL OR RDate_cls = @RDate_cls) " +
            $"  AND (@CreateSPAuto IS NULL OR CreateSPAuto = @CreateSPAuto) " +
            $"  AND (@DM_COST IS NULL OR DM_COST = @DM_COST) " +
            $"  AND (@DM_FOH IS NULL OR DM_FOH = @DM_FOH) " +
            $"  AND (@SEQUENCE_COST IS NULL OR SEQUENCE_COST = @SEQUENCE_COST) " +
            $"  AND (@SEQUENCE_PROFIT IS NULL OR SEQUENCE_PROFIT = @SEQUENCE_PROFIT) " +
            $"  AND (@TOTPRC IS NULL OR TOTPRC = @TOTPRC) " +
            $"  AND (@TOTCOST IS NULL OR TOTCOST = @TOTCOST) " +
            $"  AND (@DM_COST_VERSION IS NULL OR DM_COST_VERSION = @DM_COST_VERSION) " +
            $"  AND (@DEL_FLAG IS NULL OR DEL_FLAG = @DEL_FLAG) " +
            $"  AND (@MATDAYWANT IS NULL OR MATDAYWANT = @MATDAYWANT) " +
            $"  AND (@SEQUENCE_ACT IS NULL OR SEQUENCE_ACT = @SEQUENCE_ACT) " +
            $"  AND (@TOTCOST_ACT IS NULL OR TOTCOST_ACT = @TOTCOST_ACT) " +
            $"  AND (@SEQUENCE_DIF IS NULL OR SEQUENCE_DIF = @SEQUENCE_DIF) " +
            $"  AND (@PROFIT_BEG IS NULL OR PROFIT_BEG = @PROFIT_BEG) " +
            $"  AND (@STATION_GRP IS NULL OR STATION_GRP = @STATION_GRP) " +
            //$"AND (WorkOrderID LIKE @txtSearch OR WorkOrderID LIKE @txtSearch) " +  
            */
             "ORDER BY  WorkOrderID;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH> Search(ASSETKKF_MODEL.Request.Mcis.mtWorkOrderHReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@WorkOrderIDIsNull", d.WorkOrderID.ListNull()); 
            param.Add("@MchProjectIDIsNull", d.MchProjectID.ListNull()); 
            param.Add("@DrawingCdIsNull", d.DrawingCd.ListNull()); 
            param.Add("@WorkOrderDescIsNull", d.WorkOrderDesc.ListNull()); 
            param.Add("@QtyAmtIsNull", d.QtyAmt.ListNull()); 
            param.Add("@FMDeptCdIsNull", d.FMDeptCd.ListNull()); 
            param.Add("@CreatedDateIsNull", d.CreatedDate.ListNull()); 
            param.Add("@WorkOrderStatusIsNull", d.WorkOrderStatus.ListNull()); 
            param.Add("@FstRcvrWsIsNull", d.FstRcvrWs.ListNull()); 
            param.Add("@PrintFlgIsNull", d.PrintFlg.ListNull()); 
            param.Add("@AmtUnitIsNull", d.AmtUnit.ListNull()); 
            param.Add("@DocListNoIsNull", d.DocListNo.ListNull()); 
            param.Add("@MainWSIsNull", d.MainWS.ListNull()); 
            param.Add("@SendStockIsNull", d.SendStock.ListNull()); 
            param.Add("@USER_IDIsNull", d.USER_ID.ListNull()); 
            param.Add("@USER_DATEIsNull", d.USER_DATE.ListNull()); 
            param.Add("@Date_OpenIsNull", d.Date_Open.ListNull()); 
            param.Add("@PDate_ClsIsNull", d.PDate_Cls.ListNull()); 
            param.Add("@RDate_clsIsNull", d.RDate_cls.ListNull()); 
            param.Add("@CreateSPAutoIsNull", d.CreateSPAuto.ListNull()); 
            param.Add("@DM_COSTIsNull", d.DM_COST.ListNull()); 
            param.Add("@DM_FOHIsNull", d.DM_FOH.ListNull()); 
            param.Add("@SEQUENCE_COSTIsNull", d.SEQUENCE_COST.ListNull()); 
            param.Add("@SEQUENCE_PROFITIsNull", d.SEQUENCE_PROFIT.ListNull()); 
            param.Add("@TOTPRCIsNull", d.TOTPRC.ListNull()); 
            param.Add("@TOTCOSTIsNull", d.TOTCOST.ListNull()); 
            param.Add("@DM_COST_VERSIONIsNull", d.DM_COST_VERSION.ListNull()); 
            param.Add("@DEL_FLAGIsNull", d.DEL_FLAG.ListNull()); 
            param.Add("@MATDAYWANTIsNull", d.MATDAYWANT.ListNull()); 
            param.Add("@SEQUENCE_ACTIsNull", d.SEQUENCE_ACT.ListNull()); 
            param.Add("@TOTCOST_ACTIsNull", d.TOTCOST_ACT.ListNull()); 
            param.Add("@SEQUENCE_DIFIsNull", d.SEQUENCE_DIF.ListNull()); 
            param.Add("@PROFIT_BEGIsNull", d.PROFIT_BEG.ListNull()); 
            param.Add("@STATION_GRPIsNull", d.STATION_GRP.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.mtWorkOrderH " +
            $"WHERE (@WorkOrderIDIsNull IS NULL OR WorkOrderID IN ('{ d.WorkOrderID.Join("','") }')) " +
            $"AND (@MchProjectIDIsNull IS NULL OR MchProjectID IN ('{ d.MchProjectID.Join("','") }')) " +
            $"AND (@DrawingCdIsNull IS NULL OR DrawingCd IN ('{ d.DrawingCd.Join("','") }')) " +
            $"AND (@WorkOrderDescIsNull IS NULL OR WorkOrderDesc IN ('{ d.WorkOrderDesc.Join("','") }')) " +
            $"AND (@QtyAmtIsNull IS NULL OR QtyAmt IN ('{ d.QtyAmt.Join("','") }')) " +
            $"AND (@FMDeptCdIsNull IS NULL OR FMDeptCd IN ('{ d.FMDeptCd.Join("','") }')) " +
            $"AND (@CreatedDateIsNull IS NULL OR CreatedDate IN ('{ d.CreatedDate.Join("','") }')) " +
            $"AND (@WorkOrderStatusIsNull IS NULL OR WorkOrderStatus IN ('{ d.WorkOrderStatus.Join("','") }')) " +
            $"AND (@FstRcvrWsIsNull IS NULL OR FstRcvrWs IN ('{ d.FstRcvrWs.Join("','") }')) " +
            $"AND (@PrintFlgIsNull IS NULL OR PrintFlg IN ('{ d.PrintFlg.Join("','") }')) " +
            $"AND (@AmtUnitIsNull IS NULL OR AmtUnit IN ('{ d.AmtUnit.Join("','") }')) " +
            $"AND (@DocListNoIsNull IS NULL OR DocListNo IN ('{ d.DocListNo.Join("','") }')) " +
            $"AND (@MainWSIsNull IS NULL OR MainWS IN ('{ d.MainWS.Join("','") }')) " +
            $"AND (@SendStockIsNull IS NULL OR SendStock IN ('{ d.SendStock.Join("','") }')) " +
            $"AND (@USER_IDIsNull IS NULL OR USER_ID IN ('{ d.USER_ID.Join("','") }')) " +
            $"AND (@USER_DATEIsNull IS NULL OR USER_DATE IN ('{ d.USER_DATE.Join("','") }')) " +
            $"AND (@Date_OpenIsNull IS NULL OR Date_Open IN ('{ d.Date_Open.Join("','") }')) " +
            $"AND (@PDate_ClsIsNull IS NULL OR PDate_Cls IN ('{ d.PDate_Cls.Join("','") }')) " +
            $"AND (@RDate_clsIsNull IS NULL OR RDate_cls IN ('{ d.RDate_cls.Join("','") }')) " +
            $"AND (@CreateSPAutoIsNull IS NULL OR CreateSPAuto IN ('{ d.CreateSPAuto.Join("','") }')) " +
            $"AND (@DM_COSTIsNull IS NULL OR DM_COST IN ('{ d.DM_COST.Join("','") }')) " +
            $"AND (@DM_FOHIsNull IS NULL OR DM_FOH IN ('{ d.DM_FOH.Join("','") }')) " +
            $"AND (@SEQUENCE_COSTIsNull IS NULL OR SEQUENCE_COST IN ('{ d.SEQUENCE_COST.Join("','") }')) " +
            $"AND (@SEQUENCE_PROFITIsNull IS NULL OR SEQUENCE_PROFIT IN ('{ d.SEQUENCE_PROFIT.Join("','") }')) " +
            $"AND (@TOTPRCIsNull IS NULL OR TOTPRC IN ('{ d.TOTPRC.Join("','") }')) " +
            $"AND (@TOTCOSTIsNull IS NULL OR TOTCOST IN ('{ d.TOTCOST.Join("','") }')) " +
            $"AND (@DM_COST_VERSIONIsNull IS NULL OR DM_COST_VERSION IN ('{ d.DM_COST_VERSION.Join("','") }')) " +
            $"AND (@DEL_FLAGIsNull IS NULL OR DEL_FLAG IN ('{ d.DEL_FLAG.Join("','") }')) " +
            $"AND (@MATDAYWANTIsNull IS NULL OR MATDAYWANT IN ('{ d.MATDAYWANT.Join("','") }')) " +
            $"AND (@SEQUENCE_ACTIsNull IS NULL OR SEQUENCE_ACT IN ('{ d.SEQUENCE_ACT.Join("','") }')) " +
            $"AND (@TOTCOST_ACTIsNull IS NULL OR TOTCOST_ACT IN ('{ d.TOTCOST_ACT.Join("','") }')) " +
            $"AND (@SEQUENCE_DIFIsNull IS NULL OR SEQUENCE_DIF IN ('{ d.SEQUENCE_DIF.Join("','") }')) " +
            $"AND (@PROFIT_BEGIsNull IS NULL OR PROFIT_BEG IN ('{ d.PROFIT_BEG.Join("','") }')) " +
            $"AND (@STATION_GRPIsNull IS NULL OR STATION_GRP IN ('{ d.STATION_GRP.Join("','") }')) " +
            $"AND (WorkOrderID LIKE @txtSearch OR WorkOrderID LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkOrderID", d.WorkOrderID);
            param.Add("@MchProjectID", d.MchProjectID);
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@WorkOrderDesc", d.WorkOrderDesc);
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@FMDeptCd", d.FMDeptCd);
            param.Add("@CreatedDate", d.CreatedDate);
            param.Add("@WorkOrderStatus", d.WorkOrderStatus);
            param.Add("@FstRcvrWs", d.FstRcvrWs);
            param.Add("@PrintFlg", d.PrintFlg);
            param.Add("@AmtUnit", d.AmtUnit);
            param.Add("@DocListNo", d.DocListNo);
            param.Add("@MainWS", d.MainWS);
            param.Add("@SendStock", d.SendStock);
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@Date_Open", d.Date_Open);
            param.Add("@PDate_Cls", d.PDate_Cls);
            param.Add("@RDate_cls", d.RDate_cls);
            param.Add("@CreateSPAuto", d.CreateSPAuto);
            param.Add("@DM_COST", d.DM_COST);
            param.Add("@DM_FOH", d.DM_FOH);
            param.Add("@SEQUENCE_COST", d.SEQUENCE_COST);
            param.Add("@SEQUENCE_PROFIT", d.SEQUENCE_PROFIT);
            param.Add("@TOTPRC", d.TOTPRC);
            param.Add("@TOTCOST", d.TOTCOST);
            param.Add("@DM_COST_VERSION", d.DM_COST_VERSION);
            param.Add("@DEL_FLAG", d.DEL_FLAG);
            param.Add("@MATDAYWANT", d.MATDAYWANT);
            param.Add("@SEQUENCE_ACT", d.SEQUENCE_ACT);
            param.Add("@TOTCOST_ACT", d.TOTCOST_ACT);
            param.Add("@SEQUENCE_DIF", d.SEQUENCE_DIF);
            param.Add("@PROFIT_BEG", d.PROFIT_BEG);
            param.Add("@STATION_GRP", d.STATION_GRP);
            string cmd = "INSERT INTO mcis.dbo.mtWorkOrderH " +
            $"      (WorkOrderID, MchProjectID, DrawingCd, WorkOrderDesc, QtyAmt, FMDeptCd, CreatedDate, WorkOrderStatus, FstRcvrWs, PrintFlg, AmtUnit, DocListNo, MainWS, SendStock, USER_ID, USER_DATE, Date_Open, PDate_Cls, RDate_cls, CreateSPAuto, DM_COST, DM_FOH, SEQUENCE_COST, SEQUENCE_PROFIT, TOTPRC, TOTCOST, DM_COST_VERSION, DEL_FLAG, MATDAYWANT, SEQUENCE_ACT, TOTCOST_ACT, SEQUENCE_DIF, PROFIT_BEG, STATION_GRP) " +
            $"VALUES(@WorkOrderID, @MchProjectID, @DrawingCd, @WorkOrderDesc, @QtyAmt, @FMDeptCd, @CreatedDate, @WorkOrderStatus, @FstRcvrWs, @PrintFlg, @AmtUnit, @DocListNo, @MainWS, @SendStock, @USER_ID, @USER_DATE, @Date_Open, @PDate_Cls, @RDate_cls, @CreateSPAuto, @DM_COST, @DM_FOH, @SEQUENCE_COST, @SEQUENCE_PROFIT, @TOTPRC, @TOTCOST, @DM_COST_VERSION, @DEL_FLAG, @MATDAYWANT, @SEQUENCE_ACT, @TOTCOST_ACT, @SEQUENCE_DIF, @PROFIT_BEG, @STATION_GRP); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkOrderID", d.WorkOrderID.GetValue());
            param.Add("@MchProjectID", d.MchProjectID.GetValue());
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@WorkOrderDesc", d.WorkOrderDesc.GetValue());
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@FMDeptCd", d.FMDeptCd.GetValue());
            param.Add("@CreatedDate", d.CreatedDate);
            param.Add("@WorkOrderStatus", d.WorkOrderStatus.GetValue());
            param.Add("@FstRcvrWs", d.FstRcvrWs.GetValue());
            param.Add("@PrintFlg", d.PrintFlg.GetValue());
            param.Add("@AmtUnit", d.AmtUnit);
            param.Add("@DocListNo", d.DocListNo.GetValue());
            param.Add("@MainWS", d.MainWS.GetValue());
            param.Add("@SendStock", d.SendStock.GetValue());
            param.Add("@USER_ID", d.USER_ID.GetValue());
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@Date_Open", d.Date_Open);
            param.Add("@PDate_Cls", d.PDate_Cls);
            param.Add("@RDate_cls", d.RDate_cls);
            param.Add("@CreateSPAuto", d.CreateSPAuto.GetValue());
            param.Add("@DM_COST", d.DM_COST);
            param.Add("@DM_FOH", d.DM_FOH);
            param.Add("@SEQUENCE_COST", d.SEQUENCE_COST);
            param.Add("@SEQUENCE_PROFIT", d.SEQUENCE_PROFIT);
            param.Add("@TOTPRC", d.TOTPRC);
            param.Add("@TOTCOST", d.TOTCOST);
            param.Add("@DM_COST_VERSION", d.DM_COST_VERSION.GetValue());
            param.Add("@DEL_FLAG", d.DEL_FLAG.GetValue());
            param.Add("@MATDAYWANT", d.MATDAYWANT);
            param.Add("@SEQUENCE_ACT", d.SEQUENCE_ACT);
            param.Add("@TOTCOST_ACT", d.TOTCOST_ACT);
            param.Add("@SEQUENCE_DIF", d.SEQUENCE_DIF);
            param.Add("@PROFIT_BEG", d.PROFIT_BEG);
            param.Add("@STATION_GRP", d.STATION_GRP.GetValue());
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.mtWorkOrderH "+
            "SET WorkOrderID = @.WorkOrderID "+ 
            " , MchProjectID = @.MchProjectID "+ 
            " , DrawingCd = @.DrawingCd "+ 
            " , WorkOrderDesc = @.WorkOrderDesc "+ 
            " , QtyAmt = @.QtyAmt "+ 
            " , FMDeptCd = @.FMDeptCd "+ 
            " , CreatedDate = @.CreatedDate "+ 
            " , WorkOrderStatus = @.WorkOrderStatus "+ 
            " , FstRcvrWs = @.FstRcvrWs "+ 
            " , PrintFlg = @.PrintFlg "+ 
            " , AmtUnit = @.AmtUnit "+ 
            " , DocListNo = @.DocListNo "+ 
            " , MainWS = @.MainWS "+ 
            " , SendStock = @.SendStock "+ 
            " , USER_ID = @.USER_ID "+ 
            " , USER_DATE = @.USER_DATE "+ 
            " , Date_Open = @.Date_Open "+ 
            " , PDate_Cls = @.PDate_Cls "+ 
            " , RDate_cls = @.RDate_cls "+ 
            " , CreateSPAuto = @.CreateSPAuto "+ 
            " , DM_COST = @.DM_COST "+ 
            " , DM_FOH = @.DM_FOH "+ 
            " , SEQUENCE_COST = @.SEQUENCE_COST "+ 
            " , SEQUENCE_PROFIT = @.SEQUENCE_PROFIT "+ 
            " , TOTPRC = @.TOTPRC "+ 
            " , TOTCOST = @.TOTCOST "+ 
            " , DM_COST_VERSION = @.DM_COST_VERSION "+ 
            " , DEL_FLAG = @.DEL_FLAG "+ 
            " , MATDAYWANT = @.MATDAYWANT "+ 
            " , SEQUENCE_ACT = @.SEQUENCE_ACT "+ 
            " , TOTCOST_ACT = @.TOTCOST_ACT "+ 
            " , SEQUENCE_DIF = @.SEQUENCE_DIF "+ 
            " , PROFIT_BEG = @.PROFIT_BEG "+ 
            " , STATION_GRP = @.STATION_GRP "+ 
            "WHERE WorkOrderID = @.WorkOrderID "+ 
            " AND MchProjectID = @.MchProjectID "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND WorkOrderDesc = @.WorkOrderDesc "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND FMDeptCd = @.FMDeptCd "+ 
            " AND CreatedDate = @.CreatedDate "+ 
            " AND WorkOrderStatus = @.WorkOrderStatus "+ 
            " AND FstRcvrWs = @.FstRcvrWs "+ 
            " AND PrintFlg = @.PrintFlg "+ 
            " AND AmtUnit = @.AmtUnit "+ 
            " AND DocListNo = @.DocListNo "+ 
            " AND MainWS = @.MainWS "+ 
            " AND SendStock = @.SendStock "+ 
            " AND USER_ID = @.USER_ID "+ 
            " AND USER_DATE = @.USER_DATE "+ 
            " AND Date_Open = @.Date_Open "+ 
            " AND PDate_Cls = @.PDate_Cls "+ 
            " AND RDate_cls = @.RDate_cls "+ 
            " AND CreateSPAuto = @.CreateSPAuto "+ 
            " AND DM_COST = @.DM_COST "+ 
            " AND DM_FOH = @.DM_FOH "+ 
            " AND SEQUENCE_COST = @.SEQUENCE_COST "+ 
            " AND SEQUENCE_PROFIT = @.SEQUENCE_PROFIT "+ 
            " AND TOTPRC = @.TOTPRC "+ 
            " AND TOTCOST = @.TOTCOST "+ 
            " AND DM_COST_VERSION = @.DM_COST_VERSION "+ 
            " AND DEL_FLAG = @.DEL_FLAG "+ 
            " AND MATDAYWANT = @.MATDAYWANT "+ 
            " AND SEQUENCE_ACT = @.SEQUENCE_ACT "+ 
            " AND TOTCOST_ACT = @.TOTCOST_ACT "+ 
            " AND SEQUENCE_DIF = @.SEQUENCE_DIF "+ 
            " AND PROFIT_BEG = @.PROFIT_BEG "+ 
            " AND STATION_GRP = @.STATION_GRP "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.mtWorkOrderH d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@WorkOrderID", d.WorkOrderID.GetValue());
            param.Add("@MchProjectID", d.MchProjectID.GetValue());
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@WorkOrderDesc", d.WorkOrderDesc.GetValue());
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@FMDeptCd", d.FMDeptCd.GetValue());
            param.Add("@CreatedDate", d.CreatedDate);
            param.Add("@WorkOrderStatus", d.WorkOrderStatus.GetValue());
            param.Add("@FstRcvrWs", d.FstRcvrWs.GetValue());
            param.Add("@PrintFlg", d.PrintFlg.GetValue());
            param.Add("@AmtUnit", d.AmtUnit);
            param.Add("@DocListNo", d.DocListNo.GetValue());
            param.Add("@MainWS", d.MainWS.GetValue());
            param.Add("@SendStock", d.SendStock.GetValue());
            param.Add("@USER_ID", d.USER_ID.GetValue());
            param.Add("@USER_DATE", d.USER_DATE);
            param.Add("@Date_Open", d.Date_Open);
            param.Add("@PDate_Cls", d.PDate_Cls);
            param.Add("@RDate_cls", d.RDate_cls);
            param.Add("@CreateSPAuto", d.CreateSPAuto.GetValue());
            param.Add("@DM_COST", d.DM_COST);
            param.Add("@DM_FOH", d.DM_FOH);
            param.Add("@SEQUENCE_COST", d.SEQUENCE_COST);
            param.Add("@SEQUENCE_PROFIT", d.SEQUENCE_PROFIT);
            param.Add("@TOTPRC", d.TOTPRC);
            param.Add("@TOTCOST", d.TOTCOST);
            param.Add("@DM_COST_VERSION", d.DM_COST_VERSION.GetValue());
            param.Add("@DEL_FLAG", d.DEL_FLAG.GetValue());
            param.Add("@MATDAYWANT", d.MATDAYWANT);
            param.Add("@SEQUENCE_ACT", d.SEQUENCE_ACT);
            param.Add("@TOTCOST_ACT", d.TOTCOST_ACT);
            param.Add("@SEQUENCE_DIF", d.SEQUENCE_DIF);
            param.Add("@PROFIT_BEG", d.PROFIT_BEG);
            param.Add("@STATION_GRP", d.STATION_GRP.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.mtWorkOrderH "+
            "WHERE WorkOrderID = @.WorkOrderID "+ 
            " AND MchProjectID = @.MchProjectID "+ 
            " AND DrawingCd = @.DrawingCd "+ 
            " AND WorkOrderDesc = @.WorkOrderDesc "+ 
            " AND QtyAmt = @.QtyAmt "+ 
            " AND FMDeptCd = @.FMDeptCd "+ 
            " AND CreatedDate = @.CreatedDate "+ 
            " AND WorkOrderStatus = @.WorkOrderStatus "+ 
            " AND FstRcvrWs = @.FstRcvrWs "+ 
            " AND PrintFlg = @.PrintFlg "+ 
            " AND AmtUnit = @.AmtUnit "+ 
            " AND DocListNo = @.DocListNo "+ 
            " AND MainWS = @.MainWS "+ 
            " AND SendStock = @.SendStock "+ 
            " AND USER_ID = @.USER_ID "+ 
            " AND USER_DATE = @.USER_DATE "+ 
            " AND Date_Open = @.Date_Open "+ 
            " AND PDate_Cls = @.PDate_Cls "+ 
            " AND RDate_cls = @.RDate_cls "+ 
            " AND CreateSPAuto = @.CreateSPAuto "+ 
            " AND DM_COST = @.DM_COST "+ 
            " AND DM_FOH = @.DM_FOH "+ 
            " AND SEQUENCE_COST = @.SEQUENCE_COST "+ 
            " AND SEQUENCE_PROFIT = @.SEQUENCE_PROFIT "+ 
            " AND TOTPRC = @.TOTPRC "+ 
            " AND TOTCOST = @.TOTCOST "+ 
            " AND DM_COST_VERSION = @.DM_COST_VERSION "+ 
            " AND DEL_FLAG = @.DEL_FLAG "+ 
            " AND MATDAYWANT = @.MATDAYWANT "+ 
            " AND SEQUENCE_ACT = @.SEQUENCE_ACT "+ 
            " AND TOTCOST_ACT = @.TOTCOST_ACT "+ 
            " AND SEQUENCE_DIF = @.SEQUENCE_DIF "+ 
            " AND PROFIT_BEG = @.PROFIT_BEG "+ 
            " AND STATION_GRP = @.STATION_GRP "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

    }
}
