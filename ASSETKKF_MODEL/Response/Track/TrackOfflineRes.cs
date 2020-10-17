using System;
using System.Collections.Generic;
using System.Text;
using ASSETKKF_MODEL.Response.Asset;

namespace ASSETKKF_MODEL.Response.Track
{
    public class TrackOfflineRes
    {
        public string sqno { get; set; }
        public string yr { get; set; }
        public string mn { get; set; }
        public string yrmn { get; set; }
        public ProblemList problem { get; set; }
        public List<TrackHDRes> lstTrackHD { get; set; }
        public List<TrackPostMSTRes> lstTrackPostMST { get; set; }
        public List<TrackPostTRNRes> lstTrackPostTRN { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }
}
