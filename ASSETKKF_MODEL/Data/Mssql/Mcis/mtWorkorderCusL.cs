using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.Mcis
{
    public class mtWorkorderCusL
    {
        public int? SEQD;            //int,>
        public string ORDERFLAG;            //char(1),>
        public string FORTASK;            //varchar(50),>
        public string DATASOURCE;            //varchar(50),>
        public string ATTACHDOC;            //varchar(50),>
        public string USUALFLAG;            //char(1),>
        public decimal? QUANTITYPERMN;            //numeric(12,2),>
        public string UNITCODE;            //char(3),>
        public string SUBSTPRODUCT;            //varchar(50),>
        public string CONTACTNAME;            //varchar(50),>
        public string CONTACTPHONE;            //varchar(30),>
        public string MEMO;            //varchar(200),>)
    }
}
