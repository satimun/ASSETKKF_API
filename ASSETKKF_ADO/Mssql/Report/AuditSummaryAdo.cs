using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Report;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Report
{
    public class AuditSummaryAdo : Base
    {
        private static AuditSummaryAdo instant;
        public static AuditSummaryAdo GetInstant()
        {
            if (instant == null) instant = new AuditSummaryAdo();
            return instant;
        }



        private AuditSummaryAdo()
        {
            
        }

        public SummaryFIXEDASSET getFIXEDASSET(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [RP_AuditSummary_FIXEDASSET] (" + QuoteStr(d.Company) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryFIXEDASSET>(sql, param, conStr).FirstOrDefault();
            return res;
        }

        public List<SummaryASSETOWNER> getASSETOWNER(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [RP_AuditSummary_ASSETOWNER] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryASSETOWNER>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryRANKDEPMSTTOP3> getRANKDEPMSTTOP3(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [RP_AuditSummary_RANKDEPMSTTOP3] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + ") as P";

            var res = Query<SummaryRANKDEPMSTTOP3>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryRANKDEPCODEOLTOP3> getRANKDEPCODEOLTOP3(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [RP_AuditSummary_RANKDEPCODEOLTOP3] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryRANKDEPCODEOLTOP3>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryMONTHDEPMST> getMONTHDEPMST(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [RP_AuditSummary_MONTHDEPMST] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + ") as P";

            var res = Query<SummaryMONTHDEPMST>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryMONTHDEPCODEOL> getMONTHDEPCODEOL(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [RP_AuditSummary_MONTHDEPCODEOL] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryMONTHDEPCODEOL>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryMONTHOFFICECODE> getMONTHOFFICECODE(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [RP_AuditSummary_MONTHOFFICECODE] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + "," + QuoteStr(d.DEPCODEOL) + ") as P";

            var res = Query<SummaryMONTHOFFICECODE>(sql, param, conStr).ToList();
            return res;
        }

        

        public SummaryYearTotal getYearTotal(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_YearTotal] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + ") as P";

            var res = Query<SummaryYearTotal>(sql, param, conStr).FirstOrDefault();
            return res;
        }

        public List<SummaryYearRank> getYearRank(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_YearRank] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + ") as P";

            var res = Query<SummaryYearRank>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryYearOfficecode> getYearOfficecode(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_YearOfficecode] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + ") as P";

            var res = Query<SummaryYearOfficecode>(sql, param, conStr).ToList();
            return res;
        }


        public List<SummaryYearDEPMST> getYearDEPMST(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_YearDEPMST] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + ") as P";

            var res = Query<SummaryYearDEPMST>(sql, param, conStr).ToList();
            return res;
        }

        public SummaryDEPMSTTotal getDEPMSTTotal(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_DEPMSTTotal] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryDEPMSTTotal>(sql, param, conStr).FirstOrDefault();
            return res;
        }

        public List<SummaryDEPMSTRank> getDEPMSTRank(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_DEPMSTRank] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryDEPMSTRank>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryDEPMSTOfficecode> getDEPMSTOfficecode(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_DEPMSTOfficecode] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryDEPMSTOfficecode>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryDEPMSTDataOfficecode> getDEPMSTDataOfficecode(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_DEPMSTDataOfficecode] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryDEPMSTDataOfficecode>(sql, param, conStr).ToList();
            return res;
        }

        public List<SummaryDEPMSTDataDepcodeol> getDEPMSTDataDepcodeol(AuditSummaryReq d,  SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " Select * from  [FT_SummaryAudit_DEPMSTDataDepcodeol] (" + QuoteStr(d.Company) + "," + QuoteStr(d.year) + "," + QuoteStr(d.depmst) + ") as P";

            var res = Query<SummaryDEPMSTDataDepcodeol>(sql, param, conStr).ToList();
            return res;
        }

        public List<AsFixedAsset> getFixedAsset(AuditSummaryReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "SELECT * FROM [FT_ASFIXEDASSET] (" + QuoteStr("") + ") where 1 = 1";
            if (!String.IsNullOrEmpty(d.Company))
            {
                sql += " and  company = " + QuoteStr(d.Company);
            }
            if (!String.IsNullOrEmpty(d.depmst))
            {
                sql += " and  stcode = " + QuoteStr(d.depmst);
            }
            var res = Query<AsFixedAsset>(sql, param, conStr).ToList();
            return res;
        }




    }
}
