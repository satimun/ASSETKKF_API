using System;
using System.Collections.Generic;
using System.Text;

namespace ASSETKKF_MODEL.Data.Mssql.bsicpers
{
    public class rmEmployee
    {
        public string EmployeeID;            //char(7),>
        public string EmploType;             //char(1),>
        public string Status;                //char(1),>
        public string Shift;                 //char(1),>
        public string Weekend;               //char(3),>
        public string DepCode;               //varchar(10),>
        public string Position;              //char(6),>
        public string TitleName;             //char(12),>
        public string FirstName;             //varchar(30),>
        public string LastName;              //varchar(25),>
        public DateTime? StartDate;          //datetime,>
        public DateTime? EmploDate;          //datetime,>
        public DateTime? EndDate;            //datetime,>
        public DateTime? EditDate;           //datetime,>)
    }
}
