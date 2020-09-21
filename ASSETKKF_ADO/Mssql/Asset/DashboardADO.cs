using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class DashboardADO : Base
    {
        private static DashboardADO instant;

        public static DashboardADO GetInstant()
        {
            if (instant == null) instant = new DashboardADO();
            return instant;
        }

        private string conectStr { get; set; }

        private DashboardADO()
        {
        }

        public List<DashboardInspection> getInspection(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = "  EXEC [SP_AUDITCUTDATE] ";
            sql += " @COMPANY = " + QuoteStr(d.Company) + ",@YR = " + QuoteStr(d.year) + ",@MN = " + QuoteStr(d.mn) + ",@YRMN = " + QuoteStr(d.yrmn);
            sql += ",@DEPMST = " + QuoteStr(d.depmst) + ",@SQNO = " + QuoteStr(d.sqno) + ",";

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += "@DEPCODEOL = " + QuoteStr(d.DEPCODEOL);
            }
            else
            {
                sql += "@DEPCODEOL = " + QuoteStr("");
            }


            sql += ",@TYPECODE = " + QuoteStr(d.TYPECODE) + ",@GASTCODE = " + QuoteStr(d.GASTCODE) + ",@OFFICECODE = " + QuoteStr(d.OFFICECODE);


            var lst = Query<DashboardInspection>(sql, param).ToList();

            return lst;

        }

        public List<DashboardInspection> getInspectionByDEPMST(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = @"Select COMPANY,YR,MN,YRMN,DEPMST,DEPNM,SQNO,Flag,SUM(QTY_TOTAL) as QTY_TOTAL,SUM(QTY_CHECKED) as QTY_CHECKED
,SUM(QTY_PROBLEM) as QTY_PROBLEM,SUM(QTY_NOPROBLEM) as QTY_NOPROBLEM,SUM(QTY_TRN) as QTY_TRN,SUM(QTY_WAIT) as QTY_WAIT,MIN(MIN_INPDT) as StartDT
,MAX(MAX_INPDT) as LastDT
 ,  CAST(((CAST(SUM(QTY_CHECKED) as DECIMAL(9,2))/CAST(SUM(QTY_Total) as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS";

            sql += " from [AuditSummary_company](" + QuoteStr(d.Company) + "," + d.year + "," + d.mn + ")";

            sql += " where 1 =1";

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                sql += " and YRMN =" + QuoteStr(d.yrmn);
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and DEPMST =" + QuoteStr(d.depmst);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " AND SQNO =" + QuoteStr(d.sqno);
            }


            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " AND TYPECODE =" + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " AND GASTCODE =" + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);
            }


            sql += " Group BY COMPANY,YR,MN,YRMN,DEPMST,DEPNM,SQNO,Flag";


            

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByDEPCODEOL(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = @"Select COMPANY,YR,MN,YRMN,DEPMST,SQNO,Flag,DEPCODEOL,STNAME,SUM(QTY_TOTAL) as QTY_TOTAL,SUM(QTY_CHECKED) as QTY_CHECKED
,SUM(QTY_PROBLEM) as QTY_PROBLEM,SUM(QTY_NOPROBLEM) as QTY_NOPROBLEM,SUM(QTY_TRN) as QTY_TRN,SUM(QTY_WAIT) as QTY_WAIT,MIN(MIN_INPDT) as StartDT
,MAX(MAX_INPDT) as LastDT
 ,  CAST(((CAST(SUM(QTY_CHECKED) as DECIMAL(9,2))/CAST(SUM(QTY_Total) as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS";

            sql += " from [AuditSummary_company](" + QuoteStr(d.Company) + "," + d.year + "," + d.mn + ")";

            sql += " where 1 =1";

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                sql += " and YRMN =" + QuoteStr(d.yrmn);
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and DEPMST =" + QuoteStr(d.depmst);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " AND SQNO =" + QuoteStr(d.sqno);
            }


            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " AND TYPECODE =" + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " AND GASTCODE =" + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);
            }


            sql += " Group BY COMPANY,YR,MN,YRMN,DEPMST,SQNO,Flag,DEPCODEOL,STNAME";

            

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }


        public List<DashboardInspection> getInspectionByOFFICECODE(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = @"Select COMPANY,YR,MN,DEPMST,SQNO,Flag,DEPCODEOL,OFFICECODE,OFNAME,SUM(QTY_TOTAL) as QTY_TOTAL,SUM(QTY_CHECKED) as QTY_CHECKED
,SUM(QTY_PROBLEM) as QTY_PROBLEM,SUM(QTY_NOPROBLEM) as QTY_NOPROBLEM,SUM(QTY_TRN) as QTY_TRN,SUM(QTY_WAIT) as QTY_WAIT,MIN(MIN_INPDT) as StartDT
,MAX(MAX_INPDT) as LastDT
 ,  CAST(((CAST(SUM(QTY_CHECKED) as DECIMAL(9,2))/CAST(SUM(QTY_Total) as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS";

            sql += " from [AuditSummary_company](" + QuoteStr(d.Company) + "," + d.year + "," + d.mn + ")";

            sql += " where 1 =1";

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                sql += " and YRMN =" + QuoteStr(d.yrmn);
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and DEPMST =" + QuoteStr(d.depmst);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " AND SQNO =" + QuoteStr(d.sqno);
            }


            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " AND TYPECODE =" + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " AND GASTCODE =" + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);
            }


            sql += " Group BY  COMPANY,YR,MN,DEPMST,SQNO,Flag,DEPCODEOL,OFFICECODE,OFNAME";

            

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByTYPECODE(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " select *, (QTY_Total - QTY_CHECKED) as QTY_WAIT , ";
            sql += " CAST(((CAST(QTY_CHECKED as DECIMAL(9,2))/CAST(QTY_Total as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS from (";
            sql += " SELECT COMPANY,YR,MN,TYPECODE,TYPENAME,max(C.flag) as flag,MIN(INPDT) as StartDT,SUM(QTY) as QTY_Total ";
            sql += " ,(	select  COUNT(ASSETNO) from ( select  ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  )  and P.TYPECODE = C.TYPECODE and 'Y' <> ISNULL(SNDST,'')";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " group by ASSETNO ) as P)  as QTY_CHECKED  ";

            sql += " ,(	select  COUNT(P.ASSETNO) from ( select  P.ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " left outer join  [dbo].[FT_ASAUDITPOSTMST_PHONE] () AS PM ";
            sql += " on PM.SQNO = P.SQNO and PM.Company = P.Company  and PM.ASSETNO = P.ASSETNO  and PM.INPDT = P.INPDT";
            sql += " where  P.FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and 'Y' = ISNULL(PFLAG,'')  and P.TYPECODE = C.TYPECODE and 'Y' <> ISNULL(SNDST,'') ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " group by P.ASSETNO ) as P)  as QTY_PROBLEM  ";

            sql += " ,(	select  COUNT(P.ASSETNO) from ( select  P.ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " left outer join  [dbo].[FT_ASAUDITPOSTMST_PHONE] () AS PM ";
            sql += " on PM.SQNO = P.SQNO and PM.Company = P.Company  and PM.ASSETNO = P.ASSETNO  and PM.INPDT = P.INPDT";
            sql += " where  P.FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and 'Y' <> ISNULL(PFLAG,'')  and P.TYPECODE = C.TYPECODE and 'Y' <> ISNULL(SNDST,'') ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " group by P.ASSETNO ) as P)  as QTY_NOPROBLEM  ";


            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.TYPECODE = C.TYPECODE and 'Y' <> ISNULL(SNDST,'')";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " ) as LastDT";
            sql += " ,( select COUNT(ASSETNO) from FT_ASAUDITPOSTTRN() as P where P.TYPECODE = C.TYPECODE and 'Y' <> ISNULL(SNDST,'')";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            sql += "  ) as QTY_TRN";
            sql += " FROM [dbo].[FT_AUDITCUTDATE] (";
            sql += QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn);
            sql += " ) as C where 1 = 1";

            if (String.IsNullOrEmpty(d.yrmn))
            {
               // sql += " and YRMN = (select max(YRMN) from FT_ASAUDITCUTDATE() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " and YRMN =" + QuoteStr(d.yrmn);
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and DEPMST =" + QuoteStr(d.depmst);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " AND SQNO =" + QuoteStr(d.sqno);
            }


            if ((!d.Menu3 && !d.Menu4))
            {
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " AND DEPCODEOL =" + QuoteStr(d.DEPCODEOL);
                }
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " AND TYPECODE =" + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " AND GASTCODE =" + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);
            }


            sql += " group by COMPANY,YR,MN,TYPECODE,TYPENAME ) as D";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByGASTCODE(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " select *, (QTY_Total - QTY_CHECKED) as QTY_WAIT , ";
            sql += " CAST(((CAST(QTY_CHECKED as DECIMAL(9,2))/CAST(QTY_Total as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS from (";
            sql += " SELECT COMPANY,YR,MN,GASTCODE,GASTNAME,TYPECODE,TYPENAME,max(C.flag) as flag,MIN(INPDT) as StartDT,SUM(QTY) as QTY_Total ";
            sql += " ,(	select  COUNT(ASSETNO) from ( select  ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  )  and P.GASTCODE = C.GASTCODE and 'Y' <> ISNULL(SNDST,'')";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " group by ASSETNO ) as P)  as QTY_CHECKED  ";

            sql += " ,(	select  COUNT(P.ASSETNO) from ( select  P.ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " left outer join  [dbo].[FT_ASAUDITPOSTMST_PHONE] () AS PM ";
            sql += " on PM.SQNO = P.SQNO and PM.Company = P.Company  and PM.ASSETNO = P.ASSETNO  and PM.INPDT = P.INPDT";
            sql += " where  P.FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and 'Y' = ISNULL(PFLAG,'')  and P.GASTCODE = C.GASTCODE and 'Y' <> ISNULL(SNDST,'') ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " group by P.ASSETNO ) as P)  as QTY_PROBLEM  ";

            sql += " ,(	select  COUNT(P.ASSETNO) from ( select  P.ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " left outer join  [dbo].[FT_ASAUDITPOSTMST_PHONE] () AS PM ";
            sql += " on PM.SQNO = P.SQNO and PM.Company = P.Company  and PM.ASSETNO = P.ASSETNO  and PM.INPDT = P.INPDT";
            sql += " where  P.FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and 'Y' <> ISNULL(PFLAG,'')  and P.GASTCODE = C.GASTCODE and 'Y' <> ISNULL(SNDST,'') ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " group by P.ASSETNO ) as P)  as QTY_NOPROBLEM  ";

            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.GASTCODE = C.GASTCODE and 'Y' <> ISNULL(SNDST,'')";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            sql += " ) as LastDT";
            sql += " ,( select COUNT(ASSETNO) from FT_ASAUDITPOSTTRN() as P where P.GASTCODE = C.GASTCODE and 'Y' <> ISNULL(SNDST,'')";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            sql += "  ) as QTY_TRN";
            sql += " FROM [dbo].[FT_AUDITCUTDATE] (";
            sql += QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn);
            sql += " ) as C where 1 = 1";

            if (String.IsNullOrEmpty(d.yrmn))
            {
                //sql += " and YRMN = (select max(YRMN) from FT_ASAUDITCUTDATE() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " and YRMN =" + QuoteStr(d.yrmn);
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and DEPMST =" + QuoteStr(d.depmst);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " AND SQNO =" + QuoteStr(d.sqno);
            }


            if ((!d.Menu3 && !d.Menu4))
            {
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " AND DEPCODEOL =" + QuoteStr(d.DEPCODEOL);
                }
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " AND TYPECODE =" + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " AND GASTCODE =" + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);
            }


            sql += " group by COMPANY,YR,MN,GASTCODE,GASTNAME,TYPECODE,TYPENAME ) as D";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByASSETNO(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = @"Select * 

                ,case when PROGRESS =  0 Then 'fa fa-check-square-o' else 'fa fa-check-square-o' end ICONFAFA
                ,case when PROGRESS =  0 Then '#A93226' else '#138D75' end ICONCOLOR

                from (
                Select COMPANY,YR,MN,SQNO
                ,ASSETNO,ASSETNAME, GASTCODE,GASTNAME,TYPECODE,TYPENAME 
                ,MIN(PCODE) as PCODE,MIN(PNAME) as PNAME
                ,MIN(INPDT) AS STARTDT ,MAX(INPDT) AS LASTDT
                ,SUM(QTY) as QTY_TOTAL,SUM(QTY_CHECKED) as QTY_CHECKED

                ,
                ( select COUNT(AssETNO) from FT_ASAUDITPOSTTRN() where SQNO = t.SQNO and COMPANY = t.COMPANY and ASSETNO = t.ASSETNO) as QTY_TRN


                ,SUM(QTY_WAIT) AS QTY_WAIT
                , (Case when sum(QTY) > 0 then CAST(((CAST(sum(QTY_CHECKED) as DECIMAL(9,2)) /CAST(sum(QTY) as DECIMAL(9,2)))*100) as DECIMAL(9,2)) else 0 end ) as PROGRESS  
                FROM (
                select Z.*

                , m.AUDIT_NO,m.YR,m.MN,m.YRMN,m.CUTDT ,m.DEPMST,m.DEPNM
                ,p.PCODE,p.PNAME,p.INPID,p.INPDT 
                , case when isnull(p.PCODE,'') ='' Then 0 else 1 end QTY_CHECKED 
                , case when isnull(p.PCODE,'')<> '' Then 0 else 1 end QTY_WAIT
                from (
                Select d.COMPANY,d.SQNO,d.ASSETNO,d.ASSETNAME,d.DEPCODEOL,d.STNAME,1 As QTY, d.GASTCODE,d.GASTNAME,d.TYPECODE,d.TYPENAME 
                from FT_ASAUDITCUTDATE() as D ";
            sql += "   where   d.COMPANY = case when ISNULL(" + QuoteStr(d.Company) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.Company) + ",'') else d.COMPANY end ";
            sql += @"   and Exists (select * from FT_ASAUDITCUTDATEMST() as  M where
                 m.COMPANY = d.COMPANY and m.SQNO = d.SQNO  
                 and  m.FLAG not in ('C','X') ";
            sql += @"     and YR = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                sql += @"     and YRMN = " + QuoteStr(d.yrmn);
            }

            sql += @"      ) ";
            sql += @"      and   OFFICECODE = case when ISNULL(" + QuoteStr(d.OFFICECODE) + ",'') <> '' THEN    ISNULL(" + QuoteStr(d.OFFICECODE) + ",'') else INPID end ";


            sql += @"    ) Z 
                left join [dbo].[FT_ASAUDITPOSTMST] () as P  on p.COMPANY = z.COMPANY and p.sqno = z.sqno and p.ASSETNO = z.ASSETNO and isnull(p.PCODE,'') <> ''
                left join FT_ASAUDITCUTDATEMST() as  M on m.COMPANY = Z.COMPANY and m.SQNO = Z.SQNO  
                ) t group by COMPANY,YR,MN,SQNO,ASSETNO,ASSETNAME, GASTCODE,GASTNAME,TYPECODE,TYPENAME 
                ) Z   ";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getAuditOFFICECODE(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = @"Select COMPANY,YR,MN,DEPMST,SQNO,Flag,DEPCODEOL,OFFICECODE,OFNAME,SUM(QTY_TOTAL) as QTY_TOTAL,SUM(QTY_CHECKED) as QTY_CHECKED
,SUM(QTY_PROBLEM) as QTY_PROBLEM,SUM(QTY_NOPROBLEM) as QTY_NOPROBLEM,SUM(QTY_TRN) as QTY_TRN,SUM(QTY_WAIT) as QTY_WAIT,MIN(MIN_INPDT) as StartDT
,MAX(MAX_INPDT) as LastDT
 ,  CAST(((CAST(SUM(QTY_CHECKED) as DECIMAL(9,2))/CAST(SUM(QTY_Total) as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS";

            sql += " from [AuditSummary_company](" + QuoteStr(d.Company) + "," + d.year + "," + d.mn + ")";

            sql += " where 1 =1";

            if ((!d.Menu3 && !d.Menu4))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(d.DEPCODEOL))
                {
                    sql += " DEPCODEOL = '" + d.DEPCODEOL + "'";
                }
                if (d.DEPTCODELST != null && d.DEPTCODELST.Length > 0)
                {
                    var arrDept = d.DEPTCODELST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                sql += " )";
            }

            if (!String.IsNullOrEmpty(d.yrmn))
            {
                sql += " and YRMN =" + QuoteStr(d.yrmn);
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and DEPMST =" + QuoteStr(d.depmst);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " AND SQNO =" + QuoteStr(d.sqno);
            }


            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " AND TYPECODE =" + QuoteStr(d.TYPECODE);
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " AND GASTCODE =" + QuoteStr(d.GASTCODE);
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);
            }


            sql += " Group BY  COMPANY,YR,MN,DEPMST,SQNO,Flag,DEPCODEOL,OFFICECODE,OFNAME";

            
            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }





    }
}
