using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Core.Util;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class ViewWorkOrderDAdo : Base
    {
        private static ViewWorkOrderDAdo instant;

        public static ViewWorkOrderDAdo GetInstant()
        {
            if (instant == null) instant = new ViewWorkOrderDAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private ViewWorkOrderDAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.ViewWorkOrderD> ListActive()
        {
            string cmd = " SELECT H.QtyAmt,R.ItemCode,R.ItemName,D.* FROM vWorkOrderD D " +
                         $"INNER JOIN mtWorkOrderH H " +
                         $"ON D.WorkOrderID = H.WorkOrderID " +
                         $"LEFT JOIN stksamw.dbo.rmItem R " +
                         $"ON D.MatItemCd = R.ItemCode " +

                         $"order by d.matType,d.MatItemCd ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.ViewWorkOrderD>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.ViewWorkOrderD> GetData(ASSETKKF_MODEL.Request.Mcis.ViewWorkOrderDReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkOrderID", d.WorkOrderID);
            /*
            param.Add("@QtyAmt", d.QtyAmt);
            param.Add("@ItemCode", d.ItemCode);
            param.Add("@ItemName", d.ItemName);
            param.Add("@WorkOrderID", d.WorkOrderID);
            param.Add("@MatItemCd", d.MatItemCd);
            param.Add("@MatType", d.MatType);
            param.Add("@MatUnitCd", d.MatUnitCd);
            param.Add("@MatSize1", d.MatSize1);
            param.Add("@MatSize2", d.MatSize2);
            param.Add("@MatQty", d.MatQty);
            param.Add("@CrtReqDocFlg", d.CrtReqDocFlg);
            param.Add("@AppvNo", d.AppvNo);
            param.Add("@AppvSeqNo", d.AppvSeqNo);
            param.Add("@SumQty", d.SumQty);
            param.Add("@PUR_FLAG", d.PUR_FLAG);
            param.Add("@NOTPUR_FLAG", d.NOTPUR_FLAG);
            param.Add("@CALCOST_FLAG", d.CALCOST_FLAG);
            param.Add("@NOTCALCOST_FLAG", d.NOTCALCOST_FLAG);
            param.Add("@USER_ID", d.USER_ID);
            param.Add("@USER_DATE", d.USER_DATE);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");*/

            string cmd = " SELECT H.QtyAmt,R.ItemCode,R.ItemName,D.* FROM vWorkOrderD D " +
                         $"INNER JOIN mtWorkOrderH H " +
                         $"ON D.WorkOrderID = H.WorkOrderID " +
                         $"LEFT JOIN stksamw.dbo.rmItem R " +
                         $"ON D.MatItemCd = R.ItemCode " +                   

       
            $"  WHERE (@WorkOrderID IS NULL OR D.WorkOrderID = @WorkOrderID) " +
            /*
        $"WHERE (@QtyAmt IS NULL OR QtyAmt = @QtyAmt) " +
        $"  AND (@ItemCode IS NULL OR ItemCode = @ItemCode) " +
        $"  AND (@ItemName IS NULL OR ItemName = @ItemName) " +
        $"  AND (@WorkOrderID IS NULL OR WorkOrderID = @WorkOrderID) " +
        $"  AND (@MatItemCd IS NULL OR MatItemCd = @MatItemCd) " +
        $"  AND (@MatType IS NULL OR MatType = @MatType) " +
        $"  AND (@MatUnitCd IS NULL OR MatUnitCd = @MatUnitCd) " +
        $"  AND (@MatSize1 IS NULL OR MatSize1 = @MatSize1) " +
        $"  AND (@MatSize2 IS NULL OR MatSize2 = @MatSize2) " +
        $"  AND (@MatQty IS NULL OR MatQty = @MatQty) " +
        $"  AND (@CrtReqDocFlg IS NULL OR CrtReqDocFlg = @CrtReqDocFlg) " +
        $"  AND (@AppvNo IS NULL OR AppvNo = @AppvNo) " +
        $"  AND (@AppvSeqNo IS NULL OR AppvSeqNo = @AppvSeqNo) " +
        $"  AND (@SumQty IS NULL OR SumQty = @SumQty) " +
        $"  AND (@PUR_FLAG IS NULL OR PUR_FLAG = @PUR_FLAG) " +
        $"  AND (@NOTPUR_FLAG IS NULL OR NOTPUR_FLAG = @NOTPUR_FLAG) " +
        $"  AND (@CALCOST_FLAG IS NULL OR CALCOST_FLAG = @CALCOST_FLAG) " +
        $"  AND (@NOTCALCOST_FLAG IS NULL OR NOTCALCOST_FLAG = @NOTCALCOST_FLAG) " +
        $"  AND (@USER_ID IS NULL OR USER_ID = @USER_ID) " +
        $"  AND (@USER_DATE IS NULL OR USER_DATE = @USER_DATE) " +*/
            //$"AND (QtyAmt LIKE @txtSearch OR QtyAmt LIKE @txtSearch) " +  
            $"order by d.matType,d.MatItemCd ";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.ViewWorkOrderD>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.ViewWorkOrderD> Search(ASSETKKF_MODEL.Request.Mcis.ViewWorkOrderDReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@QtyAmtIsNull", d.QtyAmt.ListNull()); 
            param.Add("@ItemCodeIsNull", d.ItemCode.ListNull()); 
            param.Add("@ItemNameIsNull", d.ItemName.ListNull()); 
            param.Add("@WorkOrderIDIsNull", d.WorkOrderID.ListNull()); 
            param.Add("@MatItemCdIsNull", d.MatItemCd.ListNull()); 
            param.Add("@MatTypeIsNull", d.MatType.ListNull()); 
            param.Add("@MatUnitCdIsNull", d.MatUnitCd.ListNull()); 
            param.Add("@MatSize1IsNull", d.MatSize1.ListNull()); 
            param.Add("@MatSize2IsNull", d.MatSize2.ListNull()); 
            param.Add("@MatQtyIsNull", d.MatQty.ListNull()); 
            param.Add("@CrtReqDocFlgIsNull", d.CrtReqDocFlg.ListNull()); 
            param.Add("@AppvNoIsNull", d.AppvNo.ListNull()); 
            param.Add("@AppvSeqNoIsNull", d.AppvSeqNo.ListNull()); 
            param.Add("@SumQtyIsNull", d.SumQty.ListNull()); 
            param.Add("@PUR_FLAGIsNull", d.PUR_FLAG.ListNull()); 
            param.Add("@NOTPUR_FLAGIsNull", d.NOTPUR_FLAG.ListNull()); 
            param.Add("@CALCOST_FLAGIsNull", d.CALCOST_FLAG.ListNull()); 
            param.Add("@NOTCALCOST_FLAGIsNull", d.NOTCALCOST_FLAG.ListNull()); 
            param.Add("@USER_IDIsNull", d.USER_ID.ListNull()); 
            param.Add("@USER_DATEIsNull", d.USER_DATE.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.vWorkOrderD_FAR " +
            $"WHERE (@QtyAmtIsNull IS NULL OR QtyAmt IN ('{ d.QtyAmt.Join("','") }')) " +
            $"AND (@ItemCodeIsNull IS NULL OR ItemCode IN ('{ d.ItemCode.Join("','") }')) " +
            $"AND (@ItemNameIsNull IS NULL OR ItemName IN ('{ d.ItemName.Join("','") }')) " +
            $"AND (@WorkOrderIDIsNull IS NULL OR WorkOrderID IN ('{ d.WorkOrderID.Join("','") }')) " +
            $"AND (@MatItemCdIsNull IS NULL OR MatItemCd IN ('{ d.MatItemCd.Join("','") }')) " +
            $"AND (@MatTypeIsNull IS NULL OR MatType IN ('{ d.MatType.Join("','") }')) " +
            $"AND (@MatUnitCdIsNull IS NULL OR MatUnitCd IN ('{ d.MatUnitCd.Join("','") }')) " +
            $"AND (@MatSize1IsNull IS NULL OR MatSize1 IN ('{ d.MatSize1.Join("','") }')) " +
            $"AND (@MatSize2IsNull IS NULL OR MatSize2 IN ('{ d.MatSize2.Join("','") }')) " +
            $"AND (@MatQtyIsNull IS NULL OR MatQty IN ('{ d.MatQty.Join("','") }')) " +
            $"AND (@CrtReqDocFlgIsNull IS NULL OR CrtReqDocFlg IN ('{ d.CrtReqDocFlg.Join("','") }')) " +
            $"AND (@AppvNoIsNull IS NULL OR AppvNo IN ('{ d.AppvNo.Join("','") }')) " +
            $"AND (@AppvSeqNoIsNull IS NULL OR AppvSeqNo IN ('{ d.AppvSeqNo.Join("','") }')) " +
            $"AND (@SumQtyIsNull IS NULL OR SumQty IN ('{ d.SumQty.Join("','") }')) " +
            $"AND (@PUR_FLAGIsNull IS NULL OR PUR_FLAG IN ('{ d.PUR_FLAG.Join("','") }')) " +
            $"AND (@NOTPUR_FLAGIsNull IS NULL OR NOTPUR_FLAG IN ('{ d.NOTPUR_FLAG.Join("','") }')) " +
            $"AND (@CALCOST_FLAGIsNull IS NULL OR CALCOST_FLAG IN ('{ d.CALCOST_FLAG.Join("','") }')) " +
            $"AND (@NOTCALCOST_FLAGIsNull IS NULL OR NOTCALCOST_FLAG IN ('{ d.NOTCALCOST_FLAG.Join("','") }')) " +
            $"AND (@USER_IDIsNull IS NULL OR USER_ID IN ('{ d.USER_ID.Join("','") }')) " +
            $"AND (@USER_DATEIsNull IS NULL OR USER_DATE IN ('{ d.USER_DATE.Join("','") }')) " +
            $"AND (QtyAmt LIKE @txtSearch OR QtyAmt LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.ViewWorkOrderD>(cmd, param).ToList();
            return res;
        }

         

         
         

    }
}
