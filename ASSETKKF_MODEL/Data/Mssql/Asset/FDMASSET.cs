using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class FDMASSET
    {
        public string UCODE { get; set; }
        public string OFCODE { get; set; }
        public string OFNAME { get; set; }
        public string GUCODE { get; set; }
        public bool A_Review { get; set; }
        public bool A_EDIT { get; set; }
        public bool A_ADD { get; set; }
        public bool A_APPROV { get; set; }
        public bool A_Store { get; set; }
        public DateTime? CRDT { get; set; }
        public bool Menu1 { get; set; }
        public bool Menu2 { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }
        public string DVTB { get; set; }
        public string STCODE { get; set; }
    }
}
