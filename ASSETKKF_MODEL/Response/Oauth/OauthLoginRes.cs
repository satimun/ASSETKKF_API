using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Oauth
{
    public class OauthLoginRes
    {
        public string usercode { get; set; }
        public string username { get; set; }
        public string token { get; set; }
        public string deptcode { get; set; }
        public string deptname { get; set; }
        public string codcomp { get; set; }
        public string codposname { get; set; }
        public string cospos { get; set; }
        public List<string> COMPANYLST { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
