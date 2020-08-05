using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Request.Line
{
    public class NotifyPushMessage
    {
        public string type { get; set; }
        public string message { get; set; }
        public string imageFullsize { get; set; }
        public string imageThumbnail { get; set; }
        public string token { get; set; }
    }
}
