using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class ASAUDITCUTDATEMST
    {
        public string SQNO { get; set; }
        public string DEPMST { get; set; }
        public string DEPNM { get; set; }
        public int? YRMN { get; set; }
        public int? YR { get; set; }
        public int? MN { get; set; }
        public string INPID { get; set; }
        public DateTime? INPDT { get; set; }
        public DateTime? CUTDT { get; set; }
        public string FLAG { get; set; }
        public string CIR_DESC { get; set; }
        public string COMPANY { get; set; }
    }
}
