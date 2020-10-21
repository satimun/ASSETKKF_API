using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Oauth
{
    public class OauthLoginReq
    {
        public string usercode { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string twofactor { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
    }
}
