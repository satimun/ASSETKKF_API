using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class AsFixedAsset
    {
        public string COMPANY;
        public string ASSETNO;
        public string ASSETNAME;
        public DateTime? ASSETDT;


        public string UNITCODE;
        public string UNITNAME;

        public float COST;
        public float COSTASSET;
        public float COST_Remains;
        public float Year_span;

        public string TYPECODE;
        public string TYPENAME;

        public string GASTCODE;
        public string GASTNAME;

        public string STASSET;
        public string STMAIN;

        public string STCODE;
        public string STNAME;


        public string OFFICECODE;
        public string OFNAME;
        public string POSITCODE;
        public string POSITNAME;

        public string REGNO;
        public string INPID;
        public DateTime? INPDT;
        public string FLAG;

        public string LSTCODE;
        public string LOFFICECODE;




        public float CURRENTCOST;
        public float DEPRECIATION;
        public float DEPRE_DAY;
        public float DEPRE_YEAR;
        public float SALEPRICE;
        public float PROFITANDLOST;


        public string LVOUCHNO;

        public DateTime? LVOUCHDT;

        public float PERCEN_Remain;
        public string PERCEN;

        public DateTime? EXPDT;

        public string MASSETNO;
        public string MASNAME;
        public string Assetnumber;
        public string Assetgroup;




        public string IPADR;
        public string STFLAG;
        public string PAYNO;
        public string DEPCODE;


        public string DEPCODEOL;
        public string REFNO;
        public string STORENO;
        public DateTime? STOREDT;
        public DateTime? STOPDT;

        public string STOPID;
        public string STOPTYPE;

        public DateTime? PAYDT;
        public DateTime? SALEDT;

        public string TRNFLAG;
        public string scode;
        public string DEPCODEDOS;

        public DateTime? STARTDT;


        public int LSDAY;

        public float LDEPRE;
        public float LDEPRE_DAY;

        public string OFFOUT;

        public DateTime? OFFDT;



        public string OFFBOUT;

        public DateTime? CUTDT;


        public string OFFCUT;
        public string OFFCUTNM;

        public string DEPCUT;
        public string DEPCUTNM;


        public string POSITCUT;
        public string POSITCUTNM;

        public string AUDITCM;
        public string DEPCT;

        public string UPFLAG;
        public string LABEL_ID;

        public string LABEL_DESC;
        public string ASSETLABEL;

        public string REC_NEW;

        public List<AsFixedAsset> AsFixedAssetLST { get; set; }
    }
}
