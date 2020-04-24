using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class MsDrawingPathRes
    {
        public int? RowID;
        public string DrawingCd;
        public string DrawingNmTh;
        public string DrawingCdDesc;
        public int? Itemno;
        public string PathName;
        public string PathLink;
        public string EditUser;
        public DateTime? EditDate;
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
