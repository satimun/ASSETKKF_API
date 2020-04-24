using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Mcis
{
    public class msUserSequenceRes
    {
        public string USERCODE;
        public DateTime? STDATE;
        public DateTime? ENDATE;
        public string USER_ID;
        public DateTime? USER_DATE;
        public string EDIT_TYPE;

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
