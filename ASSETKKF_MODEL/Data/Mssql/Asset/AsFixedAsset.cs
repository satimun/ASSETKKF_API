using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class AsFixedAsset
    {
        public string COMPANY { get; set; }
        public string SQNO { get; set; }
        public string ASSETNO { get; set; }
        public string ASSETNAME { get; set; }
        public DateTime? ASSETDT { get; set; }


        public string UNITCODE { get; set; }
        public string UNITNAME { get; set; }

        public float COST { get; set; }
        public float COSTASSET { get; set; }
        public float COST_Remains { get; set; }
        public float Year_span { get; set; }

        public string TYPECODE { get; set; }
        public string TYPENAME { get; set; }

        public string GASTCODE { get; set; }
        public string GASTNAME { get; set; }

        public string STASSET { get; set; }
        public string STMAIN { get; set; }

        public string STCODE { get; set; }
        public string STNAME { get; set; }


        public string OFFICECODE { get; set; }
        public string OFNAME { get; set; }
        public string POSITCODE { get; set; }
        public string POSITNAME { get; set; }

        public string REGNO { get; set; }
        public string INPID { get; set; }
        public DateTime? INPDT { get; set; }
        public string FLAG { get; set; }

        public string LSTCODE { get; set; }
        public string LOFFICECODE { get; set; }




        public float CURRENTCOST { get; set; }
        public float DEPRECIATION { get; set; }
        public float DEPRE_DAY { get; set; }
        public float DEPRE_YEAR { get; set; }
        public float SALEPRICE { get; set; }
        public float PROFITANDLOST { get; set; }


        public string LVOUCHNO { get; set; }

        public DateTime? LVOUCHDT { get; set; }

        public float PERCEN_Remain { get; set; }
        public string PERCEN { get; set; }

        public DateTime? EXPDT { get; set; }

        public string MASSETNO { get; set; }
        public string MASNAME { get; set; }
        public string Assetnumber { get; set; }
        public string Assetgroup { get; set; }




        public string IPADR { get; set; }
        public string STFLAG { get; set; }
        public string PAYNO { get; set; }
        public string DEPCODE { get; set; }


        public string DEPCODEOL { get; set; }
        public string REFNO { get; set; }
        public string STORENO { get; set; }
        public DateTime? STOREDT { get; set; }
        public DateTime? STOPDT { get; set; }

        public string STOPID { get; set; }
        public string STOPTYPE { get; set; }

        public DateTime? PAYDT { get; set; }
        public DateTime? SALEDT { get; set; }

        public string TRNFLAG { get; set; }
        public string scode { get; set; }
        public string DEPCODEDOS { get; set; }

        public DateTime? STARTDT { get; set; }


        public int LSDAY { get; set; }

        public float LDEPRE { get; set; }
        public float LDEPRE_DAY { get; set; }

        public string OFFOUT { get; set; }

        public DateTime? OFFDT { get; set; }



        public string OFFBOUT { get; set; }

        public DateTime? CUTDT { get; set; }


        public string OFFCUT { get; set; }
        public string OFFCUTNM { get; set; }

        public string DEPCUT { get; set; }
        public string DEPCUTNM { get; set; }


        public string POSITCUT { get; set; }
        public string POSITCUTNM { get; set; }

        public string AUDITCM { get; set; }
        public string DEPCT { get; set; }

        public string UPFLAG { get; set; }
        public string LABEL_ID { get; set; }

        public string LABEL_DESC { get; set; }
        public string ASSETLABEL { get; set; }

        public string REC_NEW { get; set; }

        public List<AsFixedAsset> AsFixedAssetLST { get; set; }
    }

    public class TaskAudit
    {
        public string COMPANY { get; set; }
        public string SQNO { get; set; }
        public string AUDIT_NO { get; set; }
        public int YR { get; set; }
        public int MN { get; set; }
        public int YRMN { get; set; }
        public DateTime? CUTDT { get; set; }
        public string DEPMST { get; set; }
        public string DEPCODEOL { get; set; }
        public int QTY_TOTAL { get; set; }
        public int QTY_CHECKED { get; set; }
        public int QTY_WAIT { get; set; }
        public int QTY_TRN { get; set; }
        public float PROGRESS { get; set; }
        public DateTime? STARTDT { get; set; }
        public DateTime? LASTDT { get; set; }
    }
}
