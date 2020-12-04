using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Request.Track;
using ASSETKKF_MODEL.Response.Asset;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class AuditCutADO : Base
    {
        private static AuditCutADO instant;

        public static AuditCutADO GetInstant()
        {
            if (instant == null) instant = new AuditCutADO();
            return instant;
        }

        private string conectStr { get; set; }

        private AuditCutADO()
        {
           
        }

        public List<ASSETKKF_MODEL.Response.Asset.AuditCutList> GetAuditCutLists(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null, string conStr = null)
        {
            var obj = getAuditCutNoList(d, transac, conStr);
            
            return obj;
        }

        public List<ASSETKKF_MODEL.Response.Asset.AuditCutList> getAuditDepList(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT M.Company,max(M.SQNO) as SQNO,min(isnull(DEPCODEOL,'')) as DEPCODEOL,max(isnull(M.Audit_NO,'')) as Audit_NO";
            cmd += " ,M.DEPMST as id,DEPNM,max(M.YR) as YR,max(M.MN) as MN,max(M.YRMN) as YRMN,max(D.DEPCODE) as DEPCODE,max(D.CUTDT) as CUTDT,max(D.STNAME) as STNAME";
            cmd += "  ,(isnull(M.DEPNM,'') + ' : ' +M.DEPMST ) as Descriptions ";
            //cmd += " from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
            cmd += " FROM  FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.Company) + ") M ,FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.Company) + ") D";
            cmd += " where D.SQNO = M.SQNO ";
            cmd += " and D.Company = M.Company ";
            cmd += " and M.Audit_NO is not null";
            if (String.IsNullOrEmpty(d.MODE))
            {
                cmd += " and  M.FLAG not in ('X','C')";
            }
            else
            {
                cmd += " and  M.FLAG not in ('X')";
            }
            
            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and M.COMPANY in (" + comp + ") ";
            }

            if ((!d.Menu3 && !d.Menu4) && ((!String.IsNullOrEmpty(d.DeptCode)) || d.DeptLST != null))
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    cmd += " DEPCODEOL = '" + d.DeptCode + "'";
                }
                if (d.DeptLST != null && d.DeptLST.Length > 0)
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
            }

            if (d.isdept)
            {
                cmd += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else 
            {
                cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }

            //cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";

            if (!String.IsNullOrEmpty(d.YR))
            {
                cmd += " and M.YR = " + QuoteStr(d.YR);
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                cmd += " and M.MN = " + QuoteStr(d.MN);
            }



            cmd += " group by M.Company,M.DEPMST,DEPNM";
            

            var res = Query<ASSETKKF_MODEL.Response.Asset.AuditCutList>(cmd, param, conStr).ToList();
            return res;

        }

        public List<ASSETKKF_MODEL.Response.Asset.AuditCutList> getAuditDepList2(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT M.DEPMST as id,DEPNM ,(isnull(M.DEPNM,'') + ' : ' +M.DEPMST ) as Descriptions ";
            cmd += " FROM  FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.Company) + ") M ";
            cmd += " WHERE M.Audit_NO is not null";
            cmd += " and  M.FLAG not in ('X')";
            cmd += " AND exists (select * from FT_ASAUDITPOSTMSTTODEP_COMPANY(" + QuoteStr(d.Company) + ") D where D.SQNO = M.SQNO)";

            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and M.COMPANY in (" + comp + ") ";
            }


            if (d.isdept)
            {
                cmd += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else 
            {
                cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }
            //cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";

            if (!String.IsNullOrEmpty(d.YR))
            {
                cmd += " and M.YR = " + QuoteStr(d.YR);
            }


            cmd += " group by M.Company,M.DEPMST,DEPNM";


            var res = Query<ASSETKKF_MODEL.Response.Asset.AuditCutList>(cmd, param, conStr).ToList();
            return res;

        }

        public List<ASSETKKF_MODEL.Response.Asset.AuditCutList> getAuditCutNoList(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT M.Company,M.SQNO as id,min(isnull(DEPCODEOL,'')) as DEPCODEOL,isnull(M.Audit_NO,'') as Audit_NO";
            cmd += " ,M.DEPMST,DEPNM,M.YR,M.MN,max(M.YRMN) as YRMN,max(D.DEPCODE) as DEPCODE,max(D.CUTDT) as CUTDT,max(D.STNAME) as STNAME";
            cmd += " ,(isnull(M.Audit_NO,'') + ' : ' + M.SQNO + ' ( ' + CONVERT(varchar,max(D.CUTDT), 103) + ' ) ') as Descriptions";
            //cmd += " from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
            cmd += " FROM  FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.Company) + ") M ,FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.Company) + ") D";
            cmd += " where D.SQNO = M.SQNO ";
            cmd += " and D.Company = M.Company ";
            cmd += " and M.Audit_NO is not null";

            if (!String.IsNullOrEmpty(d.SQNO))
            {
                cmd += " and  M.SQNO = " + QuoteStr(d.SQNO);
            }

            if (String.IsNullOrEmpty(d.MODE))
            {
                cmd += " and  M.FLAG not in ('X','C')";
            }
            else
            {
                cmd += " and  M.FLAG not in ('X')";
            }
            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and M.COMPANY in (" + comp + ") ";
            }

            if ((!d.Menu3 && !d.Menu4) && ((!String.IsNullOrEmpty(d.DeptCode)) || d.DeptLST != null))
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(d.DeptCode))
                {
                    cmd += " DEPCODEOL = '" + d.DeptCode + "'";
                }
                if (d.DeptLST != null && d.DeptLST.Length > 0)
                {
                    var arrDept = d.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like ' " + s + "%'";
                    }

                }
                cmd += " )";
            }

            if (d.isdept)
            {
                cmd += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else 
            {
                cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }
            //cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                cmd += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                cmd += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.Company) + ") ";
                cmd += " where DEPMST = '" + d.DEPMST + "'";
                cmd += " and company = '" + d.Company + "'";
                cmd += " and assetno = d.assetno";
                if (!String.IsNullOrEmpty(d.SQNO))
                {
                    cmd += " and  SQNO = " + QuoteStr(d.SQNO);
                }
                cmd += " group by[DEPCODEOL])";
            }

            if (!String.IsNullOrEmpty(d.YR))
            {
                cmd += " and M.YR = " + QuoteStr(d.YR);
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                cmd += " and M.MN = " + QuoteStr(d.MN);
            }


            cmd += " group by M.Company,M.SQNO,M.Audit_NO,M.DEPMST,DEPNM,M.YR,M.MN";
            cmd += " order by max(D.CUTDT) desc,M.SQNO desc";

            var res = Query<ASSETKKF_MODEL.Response.Asset.AuditCutList>(cmd, param, conStr).ToList();
            return res;
            
         }

        public List<ASSETKKF_MODEL.Response.Asset.DEPTList> getDeptLst(AuditCutInfoReq dataReq, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = "Select  DEPCODEOL,MAX(STNAME) AS STNAME,DEPCODE,M.COMPANY  ";
            cmd += " FROM  FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(dataReq.Company) + ") M ,FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(dataReq.Company) + ") D";
            cmd += " where D.SQNO = M.SQNO ";
            cmd += " and D.Company = M.Company ";
            cmd += " and M.Audit_NO is not null";

            if (!String.IsNullOrEmpty(dataReq.Company))
            {
                var comp = "";
                comp = "'" + dataReq.Company.Replace(",", "','") + "'";
                cmd += " and M.COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(dataReq.sqno))
            {
                cmd += " and M.SQNO = '" + dataReq.sqno + "'";
            }

            if (!String.IsNullOrEmpty(dataReq.DEPMST))
            {
                cmd += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                cmd += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(dataReq.Company) + ") ";
                cmd += " where DEPMST = '" + dataReq.DEPMST + "'";
                cmd += " and company = '" + dataReq.Company + "'";
                cmd += " and assetno = d.assetno";
                if (!String.IsNullOrEmpty(dataReq.sqno))
                {
                    cmd += " and SQNO = '" + dataReq.sqno + "'";
                }
                cmd += " group by[DEPCODEOL])";
            }

            
            if ((!dataReq.Menu3 && !dataReq.Menu4) && ((!String.IsNullOrEmpty(dataReq.DeptCode)) || dataReq.DeptLST != null))
            {
                cmd += " and (";
                if (!String.IsNullOrEmpty(dataReq.DeptCode))
                {
                    cmd += " DEPCODEOL like (case when isnull('" + dataReq.DeptCode + "','') <> '' then   SUBSTRING('" + dataReq.DeptCode + "',1,1) else '' end + '%')";
                }
                if ((dataReq.DeptLST != null && dataReq.DeptLST.Length > 0) && (dataReq.DeptLST != "null"))
                {
                    var arrDept = dataReq.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        cmd += " or DEPCODEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                cmd += " )";
            }
            else if (!String.IsNullOrEmpty(dataReq.DEPCODEOL))
            {
                cmd += " and (";
                cmd += " DEPCODEOL like (case when isnull('" + dataReq.DEPCODEOL + "','') <> '' then   SUBSTRING('" + dataReq.DEPCODEOL + "',1,1) else '' end + '%')";
                cmd += " )";
            }

            if (!String.IsNullOrEmpty(dataReq.YR))
            {
                cmd += " and M.YR = " + QuoteStr(dataReq.YR);
            }

            if (!String.IsNullOrEmpty(dataReq.MN))
            {
                cmd += " and M.MN = " + QuoteStr(dataReq.MN);
            }

            if (dataReq.isdept)
            {
                cmd += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else
            {
                cmd += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }

            cmd += " GROUP BY DEPCODEOL,DEPCODE,M.COMPANY order by  DEPCODEOL";
            var obj = Query<ASSETKKF_MODEL.Request.Asset.DEPTList>(cmd, param, conStr).ToList();

            List<ASSETKKF_MODEL.Response.Asset.DEPTList> res = new List<ASSETKKF_MODEL.Response.Asset.DEPTList>();

            if(obj != null && obj.Count > 0)
            {
                obj.ForEach(x => {
                    res.Add(new ASSETKKF_MODEL.Response.Asset.DEPTList
                    {
                        id = x.DEPCODEOL,
                        name = x.STNAME,
                        descriptions = x.DEPCODEOL + " : " + x.STNAME
                    });
                });
            }
            

            return res;
        }

        public List<ASSETKKF_MODEL.Response.Asset.LeaderList> getLeaderCentralLst(AuditCutInfoReq dataReq, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "select distinct [CODEMPID] as OFFICECODE,[NAMEMPT] as OFNAME from  [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where 1 = 1";
            if (!String.IsNullOrEmpty(dataReq.Company))
            {
                sql += " and (  ";

                var arrComp = dataReq.Company.Split(",");
                var n = arrComp.Length;
                for(int i =0; i < n ;i++)
                {
                    sql += " [CODCOMP] like '" + arrComp[i] + "%'";
                    if(i < (n - 1))
                    {
                        sql += " or ";
                    }
                }


                sql += " )";


            }

            if ((!dataReq.Menu3 && !dataReq.Menu4) && ((!String.IsNullOrEmpty(dataReq.DeptCode)) || dataReq.DeptLST != null))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(dataReq.DeptCode))
                {
                    sql += " DEPCODEOL like (case when isnull('" + dataReq.DeptCode + "','') <> '' then   SUBSTRING('" + dataReq.DeptCode + "',1,1) else '' end + '%')";
                }
                if ((dataReq.DeptLST != null && dataReq.DeptLST.Length > 0) && (dataReq.DeptLST != "null"))
                {
                    var arrDept = dataReq.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                sql += " )";
            }
            else if(!String.IsNullOrEmpty(dataReq.DEPCODEOL))
            {
                sql += " and (";
                sql += " DEPCODEOL like (case when isnull('" + dataReq.DEPCODEOL + "','') <> '' then   SUBSTRING('" + dataReq.DEPCODEOL + "',1,1) else '' end + '%')";
                sql += " )";
            }

            if (!String.IsNullOrEmpty(dataReq.DEPMST))
            {
                sql += " and CODCOMP = '" + dataReq.DEPMST + "'";
            }

            if (!String.IsNullOrEmpty(dataReq.sqno))
            {
                sql += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(dataReq.Company) + ") ";
                sql += " where SQNO = '" + dataReq.sqno + "'";
                sql += " group by[DEPCODEOL])";
            }

            sql += " order by CODEMPID";

            var obj = Query<ASSETKKF_MODEL.Request.Asset.Leader>(sql, param, conStr).ToList();
            List<LeaderList> res = new List<LeaderList>();


            if (obj != null && obj.Count > 0)
            {
                obj.ForEach(x => {
                    res.Add(new LeaderList
                    {
                        id = x.OFFICECODE,
                        name = x.OFNAME,
                        descriptions = x.OFFICECODE + " : " + x.OFNAME,
                    });
                    
                });
            }
                

            return res;
        }

        public List<ASSETKKF_MODEL.Response.Asset.LeaderList> getLeaderLst(AuditCutInfoReq dataReq, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "select distinct OFFICECODE as id,OFNAME as name ,(OFFICECODE + ' ' + OFNAME) as  descriptions from  FT_UserAsset('') where 1 = 1";
            if (!String.IsNullOrEmpty(dataReq.Company))
            {
                var comp = "";
                comp = "'" + dataReq.Company.Replace(",", "','") + "'";
                sql += " and COMPANY in (" + comp + ") ";
            }

            if ((!dataReq.Menu3 && !dataReq.Menu4) && ((!String.IsNullOrEmpty(dataReq.DeptCode)) || dataReq.DeptLST != null))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(dataReq.DeptCode))
                {
                    sql += " DEPCODEEOL like (case when isnull('" + dataReq.DeptCode + "','') <> '' then   SUBSTRING('" + dataReq.DeptCode + "',1,1) else '' end + '%')";
                }
                if ((dataReq.DeptLST != null && dataReq.DeptLST.Length > 0) && (dataReq.DeptLST != "null"))
                {
                    var arrDept = dataReq.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                sql += " )";
            }
            else if (!String.IsNullOrEmpty(dataReq.DEPCODEOL))
            {
                sql += " and (";
                sql += " DEPCODEOL like (case when isnull('" + dataReq.DEPCODEOL + "','') <> '' then   SUBSTRING('" + dataReq.DEPCODEOL + "',1,1) else '' end + '%')";
                sql += " )";
            }

            if (!String.IsNullOrEmpty(dataReq.sqno))
            {
                sql += " and SQNO = '" + dataReq.sqno + "'";
            }



            sql += " order by OFFICECODE";

            var res = Query<LeaderList>(sql, param, conStr).ToList();


            return res;
        }

        public List<String> getDepLikeList(AuditCutInfoReq dataReq, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " SELECT case when isnull(DEPCODEOL,'') <> '' then   SUBSTRING(DEPCODEOL,1,2) else '' end as DEPLike FROM [dbo].FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(dataReq.Company) + ") D  where 1 = 1";
            if (!String.IsNullOrEmpty(dataReq.sqno))
            {
                sql += " and SQNO = '" + dataReq.sqno + "'";
            }
            if (!String.IsNullOrEmpty(dataReq.Company))
            {
                var comp = "";
                comp = "'" + dataReq.Company.Replace(",", "','") + "'";
                sql += " and COMPANY in (" + comp + ") ";
            }

            if ((!dataReq.Menu3 && !dataReq.Menu4) && ((!String.IsNullOrEmpty(dataReq.DeptCode)) || dataReq.DeptLST != null))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(dataReq.DeptCode))
                {
                    sql += " DEPCODEOL like (case when isnull('" + dataReq.DeptCode + "','') <> '' then   SUBSTRING('" + dataReq.DeptCode + "',1,1) else '' end + '%')";
                }
                if ((dataReq.DeptLST != null && dataReq.DeptLST.Length > 0) && (dataReq.DeptLST != "null"))
                {
                    var arrDept = dataReq.DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                sql += " )";
            }
            else if (!String.IsNullOrEmpty(dataReq.DEPCODEOL))
            {
                sql += " and (";
                sql += " DEPCODEOL like (case when isnull('" + dataReq.DEPCODEOL + "','') <> '' then   SUBSTRING('" + dataReq.DEPCODEOL + "',1,1) else '' end + '%')";
                sql += " )";
            }

            if (!String.IsNullOrEmpty(dataReq.DEPMST))
            {
                // sql += " and DEPMST = '" + dataReq.DEPMST + "'";
                sql += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(dataReq.Company) + ") ";
                sql += " where DEPMST = '" + dataReq.DEPMST + "'";
                sql += " and company = '" + dataReq.Company + "'";
                if (!String.IsNullOrEmpty(dataReq.sqno))
                {
                    sql += " and SQNO = '" + dataReq.sqno + "'";
                }
                sql += " and assetno = d.assetno";
                sql += " group by[DEPCODEOL])";
            }

            sql += " group by case when isnull(DEPCODEOL,'') <> '' then   SUBSTRING(DEPCODEOL,1,2) else '' end";

            var res = Query<String>(sql, param, conStr).ToList();
            return res;
        }

        public ASAUDITCUTDATEMST getAUDITCUTDATEMST(AuditPostReq d, SqlTransaction transac = null, string conStr = null)            

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select distinct M.* FROM  FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.COMPANY) + ") M ,FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") D ";
            sql += " where D.SQNO = M.SQNO ";
            sql += " and D.Company = M.Company ";            
            sql += " and M.COMPANY = '" + d.COMPANY + "'";
            sql += " and M.Audit_NO is not null";

            if (!String.IsNullOrEmpty(d.SQNO))
            {
                sql += " and M.SQNO = '" + d.SQNO + "'";
            }

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and M.YR = '" + d.YEAR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and M.MN = '" + d.MN + "'";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                //sql += " and M.DEPMST = '" + d.DEPMST + "'";
                sql += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
                if (!String.IsNullOrEmpty(d.SQNO))
                {
                    sql += " and SQNO = '" + d.SQNO + "'";
                }
                sql += " and assetno = d.assetno";
                sql += " group by[DEPCODEOL])";
            }

            if (!String.IsNullOrEmpty(d.cutdt))
            {
                //sql += " and M.CUTDT = '" + d.cutdt + "'";
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, m.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and D.OFFICECODE = '" + d.OFFICECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and D.TYPECODE = '" + d.TYPECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and D.GASTCODE = '" + d.GASTCODE + "'";
            }

            if (d.isdept)
            {
                sql += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else
            {
                sql += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }



            sql += " and  M.FLAG not in ('X','C')";

            var res = Query<ASAUDITCUTDATEMST>(sql, param, conStr).FirstOrDefault();
            return res;
        }

        public List<ASAUDITCUTDATE> getAUDITCUTDATE(AuditPostReq d, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select D.* FROM  FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.COMPANY) + ") M ,FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") D ";
            sql += " where D.SQNO = M.SQNO ";
            sql += " and D.Company = M.Company ";
            sql += " and M.SQNO = '" + d.SQNO + "'";
            sql += " and M.COMPANY = '" + d.COMPANY + "'";
            sql += " and M.Audit_NO is not null";


            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and M.YR = '" + d.YEAR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and M.MN = '" + d.MN + "'";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                //sql += " and M.DEPMST = '" + d.DEPMST + "'";
                sql += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
                sql += " and SQNO = '" + d.SQNO + "'";
                sql += " and assetno = d.assetno";
                sql += " group by[DEPCODEOL])";
            }

            if (!String.IsNullOrEmpty(d.cutdt))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, m.cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and D.OFFICECODE = '" + d.OFFICECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and D.TYPECODE = '" + d.TYPECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and D.GASTCODE = '" + d.GASTCODE + "'";
            }

            if (d.isdept)
            {
                sql += " and isnull(M.Audit_NO,'') like 'DU%' ";
            }
            else
            {
                sql += " and isnull(M.Audit_NO,'') like 'AU%' ";
            }

            sql += " and  M.FLAG not in ('X','C')";

            var res = Query<ASAUDITCUTDATE>(sql, param, conStr).ToList();
            return res;
        }

        public List<ASAUDITPOSTMST> getAUDITPOSTMST(AuditPostReq d,string flag = null, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select P.*,PM.PFlag ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME";
            sql += " from  [FT_ASAUDITPOSTMST_COMPANY] (" + QuoteStr(d.COMPANY) + ") as P ";
            sql += " left outer join  [dbo].[FT_ASAUDITPOSTMST_PHONE] () AS PM ";
            sql += " on PM.SQNO = P.SQNO and PM.Company = P.Company  and PM.ASSETNO = P.ASSETNO   and PM.INPID = P.INPID";
            sql += " where P.SQNO = '" + d.SQNO + "'";
            sql += " and P.COMPANY = '" + d.COMPANY + "'";
            sql += " and  P.INPID = '" + d.UCODE + "'";


            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and P.DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and P.POSITCODE = '" + d.AREACODE + "'";
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and P.ASSETNO = '" + d.ASSETNO + "'";
            }

            if (flag == "")
            {
                sql += " and isnull(PCODE,'') = '' ";
            }

            if(!String.IsNullOrEmpty(flag))
            {
                sql += " and isnull(PCODE,'') <> '' ";
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and YR = '" + d.YEAR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and MN = '" + d.MN + "'";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
               // sql += " and DEPMST = '" + d.DEPMST + "'";
                sql += " and DEPCODEOL in (SELECT [DEPCODEOL] ";
                sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
                sql += " and SQNO = '" + d.SQNO + "'";
                sql += " and assetno = p.assetno";
                sql += " group by[DEPCODEOL])";
            }

            if (!String.IsNullOrEmpty(d.cutdt))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and OFFICECODE = '" + d.OFFICECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and TYPECODE = '" + d.TYPECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and GASTCODE = '" + d.GASTCODE + "'";
            }

            sql += " and exists (select * from FT_ASAUDITCUTDATEMST_COMPANY(P.company) M where m.SQNO = p.sqno";

            if (d.isdept)
            {
                sql += " and isnull(Audit_NO,'') like 'DU%' ";
            }
            else
            {
                sql += " and isnull(Audit_NO,'') like 'AU%' ";
            }

            sql += " )";

            sql += " order by (case when P.INPID = '" + d.UCODE + "' then 1 else 0 end) desc, P.INPDT desc";
            var res = Query<ASAUDITPOSTMST>(sql, param, conStr).ToList();
            return res;
        }

        public List<ASAUDITPOSTMST> getASAUDITPOSTMSTPHONE(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from   FT_ASAUDITPOSTMST_PHONE_COMPANY("+QuoteStr(d.COMPANY) + ")  ";
            sql += " where SQNO = '" + d.SQNO + "'";
            sql += " and ASSETNO = '" + d.ASSETNO + "'";
            sql += " and INPID = '" + d.UCODE + "'";
            
            sql += " order by (case when INPID = '" + d.UCODE + "' then 1 else 0 end) desc";
            var res = Query<ASAUDITPOSTMST>(sql, param, conStr).ToList();
            return res;
        }

        public  List<ASAUDITPOSTMST> checkAUDITAssetNo(AuditPostCheckReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select a.*,b.pflag,b.imgpath,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= a.INPID) as INPNAME  ";
            sql += " from  [FT_ASAUDITPOSTMST_COMPANY] (" + QuoteStr(d.COMPANY) + ") as a";
            sql += " left outer join [FT_ASAUDITPOSTMST_PHONE_COMPANY] (" + QuoteStr(d.COMPANY) + ") as b";
            sql += " on b.SQNO = a.SQNO and a.COMPANY = b.COMPANY and b.ASSETNO = a.ASSETNO  and a.INPID = b.INPID";
            sql += " where a.SQNO = '" + d.SQNO + "'";
            sql += " and a.COMPANY = '" + d.COMPANY + "'";
            sql += " and a.ASSETNO = '" + d.ASSETNO + "'";
            //sql += " and a.INPID = '" + d.UCODE + "'";

            //if (!String.IsNullOrEmpty(d.DEPCODEOL))
            //{
            //    sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            //}

            sql += "  order by a.INPDT desc";

            var res = Query<ASAUDITPOSTMST>(sql, param, conStr).ToList();
             return res;
        }

        public int addAUDITPOSTMST(AuditPostReq d, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [SP_AUDITCUTPOST]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@DEPCODEOL = '" + d.DEPCODEOL + "'";
            sql += " ,@LEADERCODE = '" + d.LEADERCODE + "'";
            sql += " ,@LEADERNAME = '" + d.LEADERNAME + "'";
            sql += " ,@AREACODE = '" + d.AREACODE + "'";
            sql += " ,@AREANAME = '" + d.AREANAME + "'";
            sql += " ,@IMGPATH = '" + d.IMGPATH + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@AREA = '" + d.AREA + "'";
            sql += " ,@OFFICECODE = '" + d.OFFICECODE + "'";
            sql += " ,@TYPECODE = '" + d.TYPECODE + "'";
            sql += " ,@GASTCODE = '" + d.GASTCODE + "'";


            var res = ExecuteNonQuery(sql, param, conStr);
            return res;
        }

        public int updateAUDITPOSTMST(AUDITPOSTMSTReq d, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [dbo].[SP_AUDITPOSTMST]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@FINDY = '" + d.FINDY + "'";
            sql += " ,@PCODE= '" + d.PCODE + "'";
            sql += " ,@PNAME= '" + d.PNAME + "'";
            sql += " ,@LEADERCODE = '" + d.LEADERCODE + "'";
            sql += " ,@LEADERNAME = '" + d.LEADERNAME + "'";
            sql += " ,@AREANAME = '" + d.AREANAME + "'";
            sql += " ,@AREACODE = '" + d.AREACODE + "'";
            sql += " ,@MEMO1 = '" + d.MEMO1 + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@DEPCODEOL = '" + d.DEPCODEOL + "'";
            sql += " ,@MODE = '" + d.MODE + "'";
            sql += " ,@PFLAG = '" + d.PFLAG + "'";


            var res = ExecuteNonQuery(sql, param, conStr);
            return res;
        }

        public int updateAUDITPOSTMSTPHONE(AUDITPOSTMSTReq d, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [dbo].[SP_AUDITPOSTMSTPHONE]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@FINDY = '" + d.FINDY + "'";
            sql += " ,@PCODE= '" + d.PCODE + "'";
            sql += " ,@PNAME= '" + d.PNAME + "'";
            sql += " ,@LEADERCODE = '" + d.LEADERCODE + "'";
            sql += " ,@LEADERNAME = '" + d.LEADERNAME + "'";
            sql += " ,@AREANAME = '" + d.AREANAME + "'";
            sql += " ,@AREACODE = '" + d.AREACODE + "'";
            sql += " ,@MEMO1 = '" + d.MEMO1 + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";
            sql += " ,@DEPCODEOL = '" + d.DEPCODEOL + "'";
            sql += " ,@MODE = '" + d.MODE + "'";
            sql += " ,@PFLAG = '" + d.PFLAG + "'";
            sql += " ,@AREA = '" + d.AREA + "'";


            var res = ExecuteNonQuery(sql, param, conStr);
            return res;
        }

        public List<ASAUDITPOSTTRN> getAUDITPOSTTRN(AuditPostReq d, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= a.INPID) as INPNAME";
            sql += " from  [FT_ASAUDITPOSTTRN_COMPANY] (" + QuoteStr(d.COMPANY) + ") as a ";
            sql += " left outer join [FT_ASAUDITPOSTTRN_PHONE] () as b";
            sql += " on b.SQNO = a.SQNO and a.COMPANY = b.COMPANY and b.ASSETNO = a.ASSETNO  and a.INPID = b.INPID";
            sql += " where a.SQNO = '" + d.SQNO + "'";
            sql += " and a.COMPANY = '" + d.COMPANY + "'";
            sql += " and  a.INPID = '" + d.UCODE + "'";

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and a.DEPCODEOL = '" + d.DEPCODEOL + "'";
            }
            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and a.POSITCODE = '" + d.AREACODE + "'";
            }
            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and YR = '" + d.YEAR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and MN = '" + d.MN + "'";
            }

            //if (!String.IsNullOrEmpty(d.DEPMST))
            //{
            //    sql += "and DEPCODE in (SELECT [DEPCODE] ";
            //    sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") ";
            //    sql += " where DEPMST = '" + d.DEPMST + "'";
            //    sql += " and company = '" + d.COMPANY + "'";
            //    sql += " and SQNO = '" + d.SQNO + "'";
            //    sql += " group by[DEPCODE])";
            //}

            sql += " order by (case when a.INPID = '" + d.UCODE + "' then 1 else 0 end) desc";

            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).ToList();
            return res;
        }

        public int UpdateAUDITPOSTMSTImage(AUDITPOSTMSTReq d, SqlTransaction transac = null, string conStr = null)
        {
            if(d == null && ( d!= null && (d.SQNO == null || d.ASSETNO == null))) { return -1; }
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [dbo].[SP_AUDITPOSTMSTIMG]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@IMGPATH = '" + d.IMGPATH + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";;


            var res = ExecuteNonQuery(sql, param, conStr);
            return res;
        }

        public List<ASSETKKF_MODEL.Response.Asset.LeaderList> getCentralOfficerLst(AuditCutInfoReq dataReq, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            
            //var lst = getAuditCutNoList(new ASSETKKF_MODEL.Request.Asset.AuditCutReq() { Company = dataReq.Company , SQNO = dataReq.sqno });
            var depcodeol = "";

            /* if (lst != null && lst.Count > 0)
             {
                 var obj = lst.FirstOrDefault();
                 depcodeol = obj != null ? obj.DEPCODEOL : null;
             }
             else
             {
                 depcodeol = String.IsNullOrEmpty(dataReq.DEPCODEOL) ? dataReq.DeptCode : dataReq.DEPCODEOL;
             }*/

            if (!String.IsNullOrEmpty(dataReq.DEPCODEOL))
            {
                depcodeol = dataReq.DEPCODEOL;
            }
            else if (!String.IsNullOrEmpty(dataReq.DeptCode))
            {
                AuditCutReq req = new AuditCutReq()
                {
                    Company = dataReq.Company,
                    SQNO = dataReq.sqno,
                    Menu3 = dataReq.Menu3,
                    Menu4 = dataReq.Menu4,
                    DEPMST = dataReq.DEPMST,
                    DeptCode = dataReq.DeptCode,
                    DeptLST = dataReq.DeptLST
                };
              var  lst = getAuditCutNoList(req);
                if (lst != null && lst.Count > 0)
                {
                    var obj = lst.FirstOrDefault();
                    depcodeol = obj != null ? obj.DEPCODEOL : null;
                }
            }

            


            param.Add("@COMPANY", dataReq.Company);
            param.Add("@DEPCODEOL", depcodeol);

            // sql = "select OFFICECODE as id,OFFICECODE + ' : ' + OFNAME as descriptions,OFNAME as name from  FT_CentralOfficer(@COMPANY,@DEPCODEOL)";
            sql = "select OFFICECODE as id,OFFICECODE + ' : ' + OFNAME as descriptions,OFNAME as name from  FT_CentralOfficer(";
            sql += QuoteStr(dataReq.Company) + "," + QuoteStr(depcodeol) + ")";



            var res = Query<ASSETKKF_MODEL.Response.Asset.LeaderList>(sql, param, conStr).ToList();            

            return res;
        }

        public ASAUDITCUTDATEMST getASAUDITCUTDATEMST(TrackOfflineReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from FT_ASAUDITCUTDATEMST_COMPANY(" + QuoteStr(d.company) + ")";
            sql += " where Audit_NO = " + QuoteStr(d.audit_no);
            sql += " and Audit_NO is not null";
            sql += " and FLAG not in ('X')";
            var res = Query<ASAUDITCUTDATEMST>(sql, param, conStr).FirstOrDefault();

            return res;
        }

        public ASAUDITPOSTMST getASAUDITPOSTMST(TrackOfflineReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from FT_ASAUDITPOSTMST_COMPANY(" + QuoteStr(d.company) + ")";
            sql += " where sqno = " + QuoteStr(d.sqno);
            sql += " and inpid = " + QuoteStr(d.inpid);
            sql += " and assetno = " + QuoteStr(d.assetno);
            var res = Query<ASAUDITPOSTMST>(sql, param, conStr).FirstOrDefault();

            return res;
        }

        public ASAUDITPOSTTRN getASAUDITPOSTTRN(TrackOfflineReq d, SqlTransaction transac = null, string conStr = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from FT_ASAUDITPOSTTRN_COMPANY(" + QuoteStr(d.company) + ")";
            sql += " where sqno = " + QuoteStr(d.sqno);
            sql += " and inpid = " + QuoteStr(d.inpid);
            sql += " and assetno = " + QuoteStr(d.assetno);
            var res = Query<ASAUDITPOSTTRN>(sql, param, conStr).FirstOrDefault();

            return res;
        }

        public List<ASAUDITPOSTMST> getAUDITPOSTMST_WAIT(AuditPostReq d, string flag = null, SqlTransaction transac = null, string conStr = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select P.*,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME";
            sql += " from  [FT_ASAUDITCUTDATE_COMPANY] (" + QuoteStr(d.COMPANY) + ") as P ";
            
            sql += " where P.SQNO = '" + d.SQNO + "'";
            sql += " and P.COMPANY = '" + d.COMPANY + "'";

            sql += " AND NOT  EXISTS (SELECT  *  FROM  FT_ASAUDITPOSTMST_COMPANY(" + QuoteStr(d.COMPANY) + ")  WHERE SQNO=p.SQNO ";
            sql += " AND ASSETNO = P.ASSETNO and isnull(pcode,'') <> '' ) ";


            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and P.DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and P.POSITCODE = '" + d.AREACODE + "'";
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and P.ASSETNO = '" + d.ASSETNO + "'";
            }

            if (flag == "")
            {
                sql += " and isnull(PCODE,'') = '' ";
            }

            if (!String.IsNullOrEmpty(flag))
            {
                sql += " and isnull(PCODE,'') <> '' ";
            }

            if (!String.IsNullOrEmpty(d.YEAR))
            {
                sql += " and YR = '" + d.YEAR + "'";
            }

            if (!String.IsNullOrEmpty(d.MN))
            {
                sql += " and MN = '" + d.MN + "'";
            }

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                // sql += " and DEPMST = '" + d.DEPMST + "'";
                sql += " and DEPCODEOL in (SELECT [DEPCODEOL] ";
                sql += " FROM FT_ASAUDITCUTDATE_COMPANY(" + QuoteStr(d.COMPANY) + ") ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
                sql += " and SQNO = '" + d.SQNO + "'";
                sql += " and assetno = p.assetno";
                sql += " group by[DEPCODEOL])";
            }

            if (!String.IsNullOrEmpty(d.cutdt))
            {
                sql += " and DATEADD(dd, 0, DATEDIFF(dd, 0, cutdt)) = DATEADD(dd, 0, DATEDIFF(dd, 0, " + QuoteStr(d.cutdt) + "))";
            }

            if (!String.IsNullOrEmpty(d.OFFICECODE))
            {
                sql += " and OFFICECODE = '" + d.OFFICECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.TYPECODE))
            {
                sql += " and TYPECODE = '" + d.TYPECODE + "'";
            }

            if (!String.IsNullOrEmpty(d.GASTCODE))
            {
                sql += " and GASTCODE = '" + d.GASTCODE + "'";
            }

            sql += " and exists (select * from FT_ASAUDITCUTDATEMST_COMPANY(P.company) M where m.SQNO = p.sqno";

            if (d.isdept)
            {
                sql += " and isnull(Audit_NO,'') like 'DU%' ";
            }
            else
            {
                sql += " and isnull(Audit_NO,'') like 'AU%' ";
            }

            sql += " )";

            sql += " order by (case when P.INPID = '" + d.UCODE + "' then 1 else 0 end) desc, P.INPDT desc";
            var res = Query<ASAUDITPOSTMST>(sql, param, conStr).ToList();
            return res;
        }


    }
}
