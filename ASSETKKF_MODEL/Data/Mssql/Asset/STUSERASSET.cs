using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class STUSERASSET
    {
        public string UCODE { get; set; }
        public string XPCODE { get; set; }
        public string PCODE { get; set; }
        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string A_Review { get; set; }
        public string A_Add { get; set; }
        public string A_Edit { get; set; }
        public string A_Approv { get; set; }
        public string A_Store { get; set; }
        public decimal? BAMOUNT { get; set; }
        public decimal? EAMOUNT { get; set; }
        public string GUCODE { get; set; }
        public string INPID { get; set; }
        public DateTime? INPDT { get; set; }
        public string IPADR { get; set; }
        public string DEPCODEEOL { get; set; }
        public string NAMCENTTHA { get; set; }
        public string INPNAME { get; set; }
        public string COSPOS { get; set; }
        public string STAEMP { get; set; }
        public string TYPEMP { get; set; }
        public string COMPANY { get; set; }       
        public string CODCOMP { get; set; }
        public string CODPOSNAME { get; set; }

        public bool M_Review { get; set; }
        public bool M_ADD { get; set; }
        public bool M_EDIT { get; set; }
        public bool M_APPROV { get; set; }
        public bool M_Store { get; set; }

        public bool Menu1 { get; set; }
        public bool Menu2 { get; set; }
        public bool Menu3 { get; set; }
        public bool Menu4 { get; set; }

        public string DEPCODELST { get; set; }
        public string COMPANAYNAME { get; set; }
        public string GUNAME { get; set; }
    }

}
