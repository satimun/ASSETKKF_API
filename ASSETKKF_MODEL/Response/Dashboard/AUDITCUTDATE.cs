using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Dashboard
{
    public class AUDITCUTDATE
    {
        public string COMPANY { get; set; }
        public int YR { get; set; }
        public int MN { get; set; }
        public int YRMN { get; set; }
        public string DEPMST { get; set; }        
        public string DEPNM { get; set; }
        public string DEPCODEOL { get; set; }
        public string STNAME { get; set; }
        public string SQNO { get; set; }
        public string AUDIT_NO { get; set; }
        public DateTime? INPDT { get; set; }
        public DateTime? CUTDT { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string TYPECODE { get; set; }
        public string TYPENAME { get; set; }
        public string GASTCODE { get; set; }
        public string GASTNAME { get; set; }
        public int QTY { get; set; }
    }
}
