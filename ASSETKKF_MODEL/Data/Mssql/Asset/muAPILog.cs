﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Asset
{
    public class muAPILog
    {
        public int? ID;
        public string RefID;
        public string Token;
        public string APIName;
        public string Status;
        public string StatusMessage;
        public DateTime StartDate;
        public DateTime EndDate;
        public string ServerName;
        public string Input;
        public string Output;
        public string Remark;
    }
}
