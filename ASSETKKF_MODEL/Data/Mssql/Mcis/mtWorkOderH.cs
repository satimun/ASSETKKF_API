using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Mcis
{
    public class mtWorkOderH
    {
        public string WorkOrderID;            //char(9),>
        public string MchProjectID;            //char(8),>
        public string DrawingCd;            //varchar(20),>
        public string WorkOrderDesc;            //varchar(250),>
        public decimal? QtyAmt;            //decimal(18,0),>
        public string FMDeptCd;            //char(10),>
        public DateTime? CreatedDate;            //datetime,>
        public string WorkOrderStatus;            //char(1),>
        public string FstRcvrWs;            //char(5),>
        public string PrintFlg;            //char(1),>
        public decimal? AmtUnit;            //decimal(18,0),>
        public string DocListNo;            //char(7),>
        public string MainWS;            //char(5),>
        public string SendStock;            //char(1),>
        public string USER_ID;            //varchar(15),>
        public DateTime? USER_DATE;            //datetime,>
        public DateTime? Date_Open;            //datetime,>
        public DateTime? PDate_Cls;            //datetime,>
        public DateTime? RDate_cls;            //datetime,>
        public string CreateSPAuto;            //char(1),>)

    }
}
