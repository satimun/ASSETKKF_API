using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Mcis
{
    public class msWorkStationGrp
    {
        public string WorkStationGrpCd;            //varchar(50),>
        public string WorkStationGrpNm;            //varchar(60),>
        public decimal? DLCost;            //numeric(18,2),>
        public decimal? FOHCost;            //numeric(18,2),>
        public decimal? MDCost;            //numeric(18,2),>
        public string WorkStationCD;            //char(5),>
        public string LastUpdUsr;            //varchar(20),>
        public DateTime? LastUpdDt;            //datetime,>
        public decimal? UnionDLCost;            //decimal(18,2),>
        public string WorkStationNM;            //varchar(50),>
        public string MulEmp_flag;            //varchar(1),>
        public string ExtGrp_Flag;            //varchar(1),>)

    }
}
