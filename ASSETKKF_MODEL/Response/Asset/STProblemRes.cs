using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.Asset
{
    #region Response Data
    public class STProblemRes
    {
        public List<ProblemList> problemLst { get; set; }
        public ResultDataResponse _result = new ResultDataResponse();
    }

    #endregion

    #region Model
    public class ProblemList
    {
        public string id { get; set; }
        public string descriptions { get; set; }
        public string Pcode { get; set; }
        public string Pname { get; set; }
        public string FINDY { get; set; }
        public string PFLAG { get; set; }        
    }
    #endregion
}
