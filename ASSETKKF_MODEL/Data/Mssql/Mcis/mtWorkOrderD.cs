using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Mcis
{
    public class mtWorkOrderD
    {
        public string WorkOrderID;            //char(9),>
        public string MatItemCd;            //char(10),>
        public string MatType;            //char(2),>
        public string MatUnitCd;            //char(3),>
        public decimal? MatSize1;            //decimal(18,2),>
        public decimal? MatSize2;            //decimal(18,2),>
        public decimal? MatQty;            //decimal(18,2),>
        public string CrtReqDocFlg;            //char(1),>
        public string AppvNo;            //char(8),>
        public string AppvSeqNo;            //char(2),>
        public string USER_ID;            //varchar(15),>
        public DateTime? USER_DATE;            //datetime,>
        public decimal? MatQtyPay;            //decimal(18,2),>
        public string PUR_FLAG;            //char(1),>
        public string NOTPUR_FLAG;            //char(1),>
        public string CALCOST_FLAG;            //char(1),>
        public string NOTCALCOST_FLAG;            //char(1),>
        public DateTime? MATDAYWANT;            //datetime,>)
    }
}
