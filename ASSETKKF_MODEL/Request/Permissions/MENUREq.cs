using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Permissions
{
    public class MENUREq
    {
        public string MENUCODE { get; set; }
        public string MENUNAME { get; set; }
        public string FLAG { get; set; }
        public string INPID { get; set; }
        public string MODE { get; set; }
        public string DBMode { get; set; }
        public string ConnStr { get; set; }
    }
}
