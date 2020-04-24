using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Mcis
{
    public class ViewWorkOrderD
    {
        public decimal? QtyAmt;
        public string ItemCode;
        public string ItemName;
        public string WorkOrderID;
        public string MatItemCd;
        public string MatType;
        public string MatUnitCd;
        public decimal? MatSize1;
        public decimal? MatSize2;
        public decimal? MatQty;
        public string CrtReqDocFlg;
        public string AppvNo;
        public string AppvSeqNo;
        public decimal? SumQty;
        public string PUR_FLAG;
        public string NOTPUR_FLAG;
        public string CALCOST_FLAG;
        public string NOTCALCOST_FLAG;
        public string USER_ID;
        public DateTime? USER_DATE;
    }
}
