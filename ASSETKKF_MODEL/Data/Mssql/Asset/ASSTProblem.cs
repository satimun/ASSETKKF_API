using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class ASSTProblem
    {
        public string Pcode { get; set; }
        public string Pname { get; set; }
        public string SACC { get; set; }
        public string PNO1 { get; set; }
        public string PNO2 { get; set; }
        public string PNO3 { get; set; }
        public string PNO4 { get; set; }
        public string PNO5 { get; set; }
        public string PNO6 { get; set; }
        public string INPID { get; set; }        
        public DateTime? INPDT { get; set; }
        public string IPADDR { get; set; }
        public string FINDY { get; set; }
        public string COMPANY { get; set; }
        public string PFLAG { get; set; }
    }
}
