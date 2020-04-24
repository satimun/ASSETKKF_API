using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Response.bsicpers
{
    public class rmEmployeeRes
    {
        public string EmployeeID;
        public string EmploType;
        public string Status;
        public string Shift;
        public string Weekend;
        public string DepCode;
        public string Position;
        public string TitleName;
        public string FirstName;
        public string LastName;
        public DateTime? StartDate;
        public DateTime? EmploDate;
        public DateTime? EndDate;
        public DateTime? EditDate;

        public ResultDataResponse _result = new ResultDataResponse();
    }
}
