using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Asset
{
    public class AuditPostReq
    {
        public string COMPANY { get; set; }
        public string YEAR { get; set; }
        public string MN { get; set; }


        public string DEPMST { get; set; }
        public string SQNO { get; set; }
        public string cutdt { get; set; }

        public string DEPCODEOL { get; set; }
        public string OFFICECODE { get; set; }
        public string TYPECODE { get; set; }

        public string GASTCODE { get; set; }

        public string ASSETNO { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREACODE { get; set; }
        public string AREANAME { get; set; }
        public string IMGPATH { get; set; }
        public string UCODE { get; set; }
        public IFormFile FileToUpload { get; set; }
        public string AREA { get; set; }
        public string orderby { get; set; }
        public string mode { get; set; }

        public string poth { get; set; }
        public string snnstdt { get; set; }
        public string expstdt { get; set; }

        public string FINDY { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string MEMO1 { get; set; }
        public string INPID { get; set; }
        public string FLAG { get; set; }
        public string filter { get; set; }
        public string depy { get; set; }

        public string ASSETNAME { get; set; }

        public string OFNAME { get; set; }
        public string ASSETNONEW { get; set; }
        public string ACTION { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }

        public string COMPANY_TRN { get; set; }
        public string SQNO_TRN { get; set; }
        public string MEMO_TRN { get; set; }
        public string INPID_TRN { get; set; }

    }

        public class AuditPostCheckReq
    {
        public string SQNO { get; set; }
        public string COMPANY { get; set; }
        public string DEPCODEOL { get; set; }
        public string ASSETNO { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREANAME { get; set; }
        public string AREACODE { get; set; }
        public string UCODE { get; set; }
        public string MODE { get; set; }
        public string PFLAG { get; set; }
        public string status { get; set; }

        public string OFFICECODE { get; set; }
        public string TYPECODE { get; set; }
        public string GASTCODE { get; set; }
        public string IMGPATH { get; set; }

        public string YEAR { get; set; }
        public string MN { get; set; }


        public string DEPMST { get; set; }
        public string cutdt { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
    }

    public class AUDITPOSTMSTReq
    {
        public string SQNO { get; set; }
        public string DEPCODEOL { get; set; }
        public string COMPANY { get; set; }
        public string LEADERCODE { get; set; }
        public string LEADERNAME { get; set; }
        public string AREACODE { get; set; }
        public string AREANAME { get; set; }
        public string ASSETNO { get; set; }
        public string FINDY { get; set; }
        public string PCODE { get; set; }
        public string PNAME { get; set; }
        public string MEMO1 { get; set; }
        public string UCODE { get; set; }
        public string IMGPATH { get; set; }
        public string MODE { get; set; }
        public string PFLAG { get; set; }

        public IFormFile FileToUpload { get; set; }
        public string AREA { get; set; }

        public string poth { get; set; }
        public string snnstdt { get; set; }
        public string expstdt { get; set; }
        public string INPID { get; set; }
        public string FLAG { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
    }
}
