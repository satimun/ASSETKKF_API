using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Report
{
    public class RptAuditSummaryRes
    {
        public SummaryFIXEDASSET SummaryFIXEDASSET { get; set; }
        public List<SummaryASSETOWNER> SummaryASSETOWNERLst { get; set; }
        public List<SummaryRANKDEPMSTTOP3> SummaryRANKDEPMSTTOP3Lst { get; set; }
        public List<SummaryRANKDEPCODEOLTOP3> SummaryRANKDEPCODEOLTOP3Lst { get; set; }
        public List<SummaryMONTHDEPMST> SummaryMONTHDEPMSTLst { get; set; }
        public List<SummaryMONTHDEPCODEOL> SummaryMONTHDEPCODEOLLst { get; set; }
        public List<SummaryMONTHOFFICECODE> SummaryMONTHOFFICECODELst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }


    public class SummaryFIXEDASSET
    {
        public int qty_total { get; set; }
        public int qty_audit { get; set; }
        public int qty_noaudit { get; set; }
    }

    public class SummaryASSETOWNER
    {
        public string utype { get; set; }
        public string udesc { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
    }

    public class SummaryRANKDEPMSTTOP3
    {
        public string depmst { get; set; }
        public string depnm { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public float progress { get; set; }
    }

    public class SummaryRANKDEPCODEOLTOP3
    {
        public string depcodeol { get; set; }
        public string stname { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public float progress { get; set; }
    }


    public class SummaryMONTHDEPMST
    {
        public string depmst { get; set; }
        public string depnm { get; set; }
        public float progress1 { get; set; }
        public float progress2 { get; set; }
        public float progress3 { get; set; }
        public float progress4 { get; set; }
        public float progress5 { get; set; }
        public float progress6 { get; set; }
        public float progress7 { get; set; }
        public float progress8 { get; set; }
        public float progress9 { get; set; }
        public float progress10 { get; set; }
        public float progress11 { get; set; }
        public float progress12 { get; set; }

    }

    public class SummaryMONTHDEPCODEOL
    {
        public string depcodeol { get; set; }
        public string stname { get; set; }
        public float progress1 { get; set; }
        public float progress2 { get; set; }
        public float progress3 { get; set; }
        public float progress4 { get; set; }
        public float progress5 { get; set; }
        public float progress6 { get; set; }
        public float progress7 { get; set; }
        public float progress8 { get; set; }
        public float progress9 { get; set; }
        public float progress10 { get; set; }
        public float progress11 { get; set; }
        public float progress12 { get; set; }

    }

    public class SummaryMONTHOFFICECODE
    {
        public string officecode { get; set; }
        public string ofname { get; set; }
        public float progress1 { get; set; }
        public float progress2 { get; set; }
        public float progress3 { get; set; }
        public float progress4 { get; set; }
        public float progress5 { get; set; }
        public float progress6 { get; set; }
        public float progress7 { get; set; }
        public float progress8 { get; set; }
        public float progress9 { get; set; }
        public float progress10 { get; set; }
        public float progress11 { get; set; }
        public float progress12 { get; set; }

    }

    //--------- Search by year
    public class SummaryYearTotal
    {
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public int qty_wait { get; set; }
    }

    public class SummaryYearRank
    {
        public string depmst { get; set; }
        public string depnm { get; set; }
        public float progress_audit { get; set; }
    }

    public class SummaryYearOfficecode
    {
        public string utype { get; set; }
        public string udesc { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public int qty_wait { get; set; }
    }

    public class SummaryYearDEPMST
    {
        public string depmst { get; set; }
        public string depnm { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public int qty_wait { get; set; }
        public float progress_audit { get; set; }
        public float progress_wait { get; set; }
    }

    //--------- Search by year and depmst
    public class SummaryDEPMSTTotal
    {
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public int qty_wait { get; set; }
    }

    public class SummaryDEPMSTRank
    {
        public string officecode { get; set; }
        public string ofname { get; set; }
        public float progress_audit { get; set; }
    }

    public class SummaryDEPMSTOfficecode
    {
        public string utype { get; set; }
        public string udesc { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public int qty_wait { get; set; }
    }

    public class SummaryDEPMSTDataOfficecode
    {
        public string officecode { get; set; }
        public string ofname { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public int qty_wait { get; set; }
        public float progress_audit { get; set; }
        public float progress_wait { get; set; }
    }

    public class SummaryDEPMSTDataDepcodeol
    {
        public string depcodeol { get; set; }
        public string stname { get; set; }
        public int qty_asset { get; set; }
        public int qty_audit { get; set; }
        public int qty_wait { get; set; }
        public float progress_audit { get; set; }
        public float progress_wait { get; set; }
    }



}
