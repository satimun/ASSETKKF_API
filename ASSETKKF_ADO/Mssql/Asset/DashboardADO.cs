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

            sql = " select *, (QTY_Total - QTY_CHECKED) as QTY_WAIT , ";
            sql += " CAST(((CAST(QTY_CHECKED as DECIMAL(9,2))/CAST(QTY_Total as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS from (";
            sql += " SELECT COMPANY,DEPMST,DEPNM,MIN(INPDT) as StartDT,SUM(QTY) as QTY_Total ";
            sql += " ,(	select  COUNT(ASSETNO) from ( select  ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  )  and P.DEPMST = C.DEPMST ";
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
            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.DEPMST = C.DEPMST";
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
            sql += " ,( select COUNT(ASSETNO) from FT_ASAUDITPOSTTRN() as P where P.DEPMST = C.DEPMST ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            sql += "  ) as QTY_TRN";
            sql += " FROM [dbo].[FT_AUDITCUTDATE] (";
            sql += QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn);
            sql += " ) as C";

            if (String.IsNullOrEmpty(d.yrmn))
            {
                sql += " where YRMN = (select max(YRMN) from FT_ASAUDITCUTDATE() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " where YRMN =" + QuoteStr(d.yrmn);
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


            sql += " group by COMPANY,DEPMST,DEPNM ) as D";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByDEPCODEOL(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " select *, (QTY_TOTAL - QTY_CHECKED) as QTY_WAIT , ";
            sql += " CAST(((CAST(QTY_CHECKED as DECIMAL(9,2))/CAST(QTY_Total as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS from (";
            sql += " SELECT COMPANY,DEPCODEOL,STNAME,MIN(INPDT) as StartDT,SUM(QTY) as QTY_TOTAL ";
            sql += " ,(	select  COUNT(ASSETNO) from ( select  ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  )  and P.DEPCODEOL = C.DEPCODEOL ";
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
            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.DEPCODEOL = C.DEPCODEOL";
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
            sql += " ,( select COUNT(ASSETNO) from FT_ASAUDITPOSTTRN() as P where P.DEPCODEOL = C.DEPCODEOL ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            sql += "  ) as QTY_TRN";
            sql += " FROM [dbo].[FT_AUDITCUTDATE] (";
            sql += QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn);
            sql += " ) as C";

            if (String.IsNullOrEmpty(d.yrmn))
            {
                sql += " where YRMN = (select max(YRMN) from FT_ASAUDITCUTDATE() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " where YRMN =" + QuoteStr(d.yrmn);
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

            sql += " group by COMPANY,DEPCODEOL,STNAME ) as D";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }


        public List<DashboardInspection> getInspectionByOFFICECODE(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " select *, (QTY_TOTAL - QTY_CHECKED) as QTY_WAIT , ";
            sql += " CAST(((CAST(QTY_CHECKED as DECIMAL(9,2))/CAST(QTY_Total as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS from (";
            sql += " SELECT COMPANY,OFFICECODE,OFNAME,MIN(INPDT) as StartDT,SUM(QTY) as QTY_TOTAL ";
            sql += " ,(	select  COUNT(ASSETNO) from ( select  ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  )  and P.OFFICECODE = C.OFFICECODE ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " AND DEPCODEOL =" + QuoteStr(d.DEPCODEOL);
            }

            sql += " group by ASSETNO ) as P)  as QTY_CHECKED  ";
            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.OFFICECODE = C.OFFICECODE";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " AND DEPCODEOL =" + QuoteStr(d.DEPCODEOL);
            }

            sql += " ) as LastDT";
            sql += " ,( select COUNT(ASSETNO) from FT_ASAUDITPOSTTRN() as P where P.OFFICECODE = C.OFFICECODE ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            sql += "  ) as QTY_TRN";
            sql += " FROM [dbo].[FT_AUDITCUTDATE] (";
            sql += QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn);
            sql += " ) as C";

            if (String.IsNullOrEmpty(d.yrmn))
            {
                sql += " where YRMN = (select max(YRMN) from FT_ASAUDITCUTDATE() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " where YRMN =" + QuoteStr(d.yrmn);
            }

            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and DEPMST =" + QuoteStr(d.depmst);
            }

            if (!String.IsNullOrEmpty(d.sqno))
            {
                sql += " AND SQNO =" + QuoteStr(d.sqno);
            }


            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " AND DEPCODEOL =" + QuoteStr(d.DEPCODEOL);
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

            sql += " group by COMPANY,OFFICECODE,OFNAME )as D";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByTYPECODE(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " select *, (QTY_Total - QTY_CHECKED) as QTY_WAIT , ";
            sql += " CAST(((CAST(QTY_CHECKED as DECIMAL(9,2))/CAST(QTY_Total as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS from (";
            sql += " SELECT COMPANY,TYPECODE,TYPENAME,MIN(INPDT) as StartDT,SUM(QTY) as QTY_Total ";
            sql += " ,(	select  COUNT(ASSETNO) from ( select  ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  )  and P.TYPECODE = C.TYPECODE ";
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
            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.TYPECODE = C.TYPECODE";
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
            sql += " ,( select COUNT(ASSETNO) from FT_ASAUDITPOSTTRN() as P where P.TYPECODE = C.TYPECODE ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            sql += "  ) as QTY_TRN";
            sql += " FROM [dbo].[FT_AUDITCUTDATE] (";
            sql += QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn);
            sql += " ) as C";

            if (String.IsNullOrEmpty(d.yrmn))
            {
                sql += " where YRMN = (select max(YRMN) from FT_ASAUDITCUTDATE() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " where YRMN =" + QuoteStr(d.yrmn);
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


            sql += " group by COMPANY,TYPECODE,TYPENAME ) as D";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByGASTCODE(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " select *, (QTY_Total - QTY_CHECKED) as QTY_WAIT , ";
            sql += " CAST(((CAST(QTY_CHECKED as DECIMAL(9,2))/CAST(QTY_Total as DECIMAL(9,2)))*100)as DECIMAL(9,2)) as PROGRESS from (";
            sql += " SELECT COMPANY,GASTCODE,GASTNAME,TYPECODE,TYPENAME,MIN(INPDT) as StartDT,SUM(QTY) as QTY_Total ";
            sql += " ,(	select  COUNT(ASSETNO) from ( select  ASSETNO from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  )  and P.GASTCODE = C.GASTCODE ";
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
            sql += " ,(select  MAX(P.INPDT) from FT_ASAUDITPOSTMST() P";
            sql += " where  FLAG  in ('P') and (PCODE is not null and PCODE  <> ''  ) and P.GASTCODE = C.GASTCODE";
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
            sql += " ,( select COUNT(ASSETNO) from FT_ASAUDITPOSTTRN() as P where P.GASTCODE = C.GASTCODE ";
            sql += " and P.COMPANY = " + QuoteStr(d.Company) + " and P.YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            sql += "  ) as QTY_TRN";
            sql += " FROM [dbo].[FT_AUDITCUTDATE] (";
            sql += QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.mn);
            sql += " ) as C";

            if (String.IsNullOrEmpty(d.yrmn))
            {
                sql += " where YRMN = (select max(YRMN) from FT_ASAUDITCUTDATE() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " where YRMN =" + QuoteStr(d.yrmn);
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


            sql += " group by COMPANY,GASTCODE,GASTNAME,TYPECODE,TYPENAME ) as D";

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }

        public List<DashboardInspection> getInspectionByASSETNO(AuditSummaryReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();

            sql = " SELECT DISTINCT A.ASSETNO,A.ASSETNAME, A.GASTCODE,A.GASTNAME,A.TYPECODE,A.TYPENAME,P.PCODE,P.PNAME,P.INPID,P.INPDT FROM (  ";
            sql += " SELECT SQNO, ASSETNO,ASSETNAME, GASTCODE,GASTNAME,TYPECODE,TYPENAME";
            sql += " FROM [dbo].[FT_ASAUDITCUTDATE] ()  as C";
            sql += " where FLAG <> 'C' ";
            sql += " and COMPANY = " + QuoteStr(d.Company) + " and YR  = " + QuoteStr(d.year) + " and MN = " + QuoteStr(d.mn);
            if (String.IsNullOrEmpty(d.yrmn))
            {
                sql += " and YRMN = (select max(YRMN) from FT_ASAUDITCUTDATEMST() T where T.COMPANY = " + QuoteStr(d.Company) + " and T.YR = " + QuoteStr(d.year) + " and T.MN = " + QuoteStr(d.mn) + " and T.DEPMST = C.DEPMST  and AUDIT_NO is not null)";
            }
            else
            {
                sql += " and YRMN =" + QuoteStr(d.yrmn);
            }
            
            sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);


            sql += " group by SQNO, ASSETNO,ASSETNAME, GASTCODE,GASTNAME,TYPECODE,TYPENAME ) as A";
            sql += " LEFT JOIN FT_ASAUDITPOSTMST() as P on P.SQNO = A.SQNO and COMPANY =  " + QuoteStr(d.Company);
            sql += " AND OFFICECODE =" + QuoteStr(d.OFFICECODE);

            var res = Query<DashboardInspection>(sql, param).ToList();
            return res;

        }





    }
}
