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
        public string GUCODE { get; set; }

        public  List<OauthLoginRes> UserLst { get; set; }
        public List<UserGroup> UserGroupLst { get; set; }

        public ResultDataResponse _result = new ResultDataResponse();
    }

    public class UserGroup
    {
        public string company { get; set; }
        public string companyname { get; set; }
        public string gucode { get; set; }
        public string guname { get; set; }
        public string depcodeol { get; set; }
    }
}
