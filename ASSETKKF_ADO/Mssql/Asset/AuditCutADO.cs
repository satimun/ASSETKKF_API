using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Asset;
using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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

        public List<ASSETKKF_MODEL.Response.Asset.AuditCutList> GetAuditCutLists(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null)
        {
            var obj = getAuditCutNoList(d, transac);
            
            return obj;
        }

        public List<ASSETKKF_MODEL.Response.Asset.AuditCutList> getAuditDepList(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT M.Company,max(M.SQNO) as SQNO,min(isnull(DEPCODEOL,'')) as DEPCODEOL,max(isnull(M.Audit_NO,'')) as Audit_NO";
            cmd += " ,M.DEPMST as id,DEPNM,max(M.YR) as YR,max(M.MN) as MN,max(M.YRMN) as YRMN,max(D.DEPCODE) as DEPCODE,max(D.CUTDT) as CUTDT,max(D.STNAME) as STNAME";
            cmd += "  ,(isnull(M.DEPNM,'') + ' : ' +M.DEPMST ) as Descriptions ";
            cmd += " from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
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

            if (String.IsNullOrEmpty(d.MODE))
            {
                cmd += " and M.YR = (SELECT YR from(";
                cmd += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITCUTDATEMST()";
                cmd += " where YR = (SELECT max(YR) FROM FT_ASAUDITCUTDATEMST() )";
                cmd += " group by YR    ) as a)";
                cmd += " and M.MN = (SELECT MN from(";
                cmd += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITCUTDATEMST()";
                cmd += " where YR = (SELECT max(YR) FROM FT_ASAUDITCUTDATEMST() )";
                cmd += " group by YR    ) as b)";
            }



            cmd += " group by M.Company,M.DEPMST,DEPNM";
            

            var res = Query<ASSETKKF_MODEL.Response.Asset.AuditCutList>(cmd, param).ToList();
            return res;

        }

        public List<ASSETKKF_MODEL.Response.Asset.AuditCutList> getAuditCutNoList(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = " SELECT M.Company,M.SQNO as id,min(isnull(DEPCODEOL,'')) as DEPCODEOL,isnull(M.Audit_NO,'') as Audit_NO";
            cmd += " ,M.DEPMST,DEPNM,M.YR,M.MN,max(M.YRMN) as YRMN,max(D.DEPCODE) as DEPCODE,max(D.CUTDT) as CUTDT,max(D.STNAME) as STNAME";
            cmd += " ,(isnull(M.Audit_NO,'') + ' : ' + M.SQNO + ' ( ' + CONVERT(varchar,max(D.CUTDT), 103) + ' ) ') as Descriptions";
            cmd += " from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
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

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                cmd += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                cmd += " FROM FT_ASAUDITCUTDATE() ";
                cmd += " where DEPMST = '" + d.DEPMST + "'";
                cmd += " and company = '" + d.Company + "'";
                cmd += " group by[DEPCODEOL])";
            }


            cmd += " group by M.Company,M.SQNO,M.Audit_NO,M.DEPMST,DEPNM,M.YR,M.MN";
            cmd += " order by max(D.CUTDT) desc,M.SQNO desc";

            var res = Query<ASSETKKF_MODEL.Response.Asset.AuditCutList>(cmd, param).ToList();
            return res;
            
         }

        public List<ASSETKKF_MODEL.Response.Asset.DEPTList> getDeptLst(AuditCutInfoReq dataReq, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = "Select  DEPCODEOL,MAX(STNAME) AS STNAME,DEPCODE,COMPANY  from  FT_ASAUDITCUTDATE() where 1 = 1";
            if (!String.IsNullOrEmpty(dataReq.Company))
            {
                var comp = "";
                comp = "'" + dataReq.Company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(dataReq.sqno))
            {
                cmd += " and SQNO = '" + dataReq.sqno + "'";
            }

            if (!String.IsNullOrEmpty(dataReq.DEPMST))
            {
                cmd += "and DEPCODEOL in (SELECT [DEPCODEOL] ";
                cmd += " FROM FT_ASAUDITCUTDATE() ";
                cmd += " where DEPMST = '" + dataReq.DEPMST + "'";
                cmd += " and company = '" + dataReq.Company + "'";
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

            cmd += " GROUP BY DEPCODEOL,DEPCODE,COMPANY order by  DEPCODEOL";
            var obj = Query<ASSETKKF_MODEL.Request.Asset.DEPTList>(cmd, param).ToList();

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

        public List<ASSETKKF_MODEL.Response.Asset.LeaderList> getLeaderCentralLst(AuditCutInfoReq dataReq, SqlTransaction transac = null)
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
                sql += " FROM[assetkkf].[dbo].[ASAUDITCUTDATE] ";
                sql += " where SQNO = '" + dataReq.sqno + "'";
                sql += " group by[DEPCODEOL])";
            }

            sql += " order by CODEMPID";

            var obj = Query<ASSETKKF_MODEL.Request.Asset.Leader>(sql, param).ToList();
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

        public List<ASSETKKF_MODEL.Response.Asset.LeaderList> getLeaderLst(AuditCutInfoReq dataReq, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "select distinct OFFICECODE,OFNAME from  FT_UserAsset('') where 1 = 1";
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

            var obj = Query<ASSETKKF_MODEL.Request.Asset.Leader>(sql, param).ToList();
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

        public List<String> getDepLikeList(AuditCutInfoReq dataReq, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " SELECT case when isnull(DEPCODEOL,'') <> '' then   SUBSTRING(DEPCODEOL,1,2) else '' end as DEPLike FROM [dbo].[FT_ASAUDITCUTDATE] () where 1 = 1";
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
                sql += " FROM FT_ASAUDITCUTDATE() ";
                sql += " where DEPMST = '" + dataReq.DEPMST + "'";
                sql += " and company = '" + dataReq.Company + "'";
                sql += " group by[DEPCODEOL])";
            }

            sql += " group by case when isnull(DEPCODEOL,'') <> '' then   SUBSTRING(DEPCODEOL,1,2) else '' end";

            var res = Query<String>(sql, param).ToList();
            return res;
        }

        public ASAUDITCUTDATEMST getAUDITCUTDATEMST(AuditPostReq d, SqlTransaction transac = null)            

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select distinct M.* from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
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
                sql += " FROM FT_ASAUDITCUTDATE() ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
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



            sql += " and  M.FLAG not in ('X','C')";

            var res = Query<ASAUDITCUTDATEMST>(sql, param).FirstOrDefault();
            return res;
        }

        public List<ASAUDITCUTDATE> getAUDITCUTDATE(AuditPostReq d, SqlTransaction transac = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select D.* from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
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
                sql += " FROM FT_ASAUDITCUTDATE() ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
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

            sql += " and  M.FLAG not in ('X','C')";

            var res = Query<ASAUDITCUTDATE>(sql, param).ToList();
            return res;
        }

        public List<ASAUDITPOSTMST> getAUDITPOSTMST(AuditPostReq d,string flag = null, SqlTransaction transac = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= P.INPID) as INPNAME";
            sql += " from  [FT_ASAUDITPOSTMST] () as P ";
            sql += " where SQNO = '" + d.SQNO + "'";
            sql += " and COMPANY = '" + d.COMPANY + "'";
            sql += " and  INPID = '" + d.UCODE + "'";


            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }

            if (!String.IsNullOrEmpty(d.ASSETNO))
            {
                sql += " and ASSETNO = '" + d.ASSETNO + "'";
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
                sql += " FROM FT_ASAUDITCUTDATE() ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
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

            sql += " order by (case when INPID = '" + d.UCODE + "' then 1 else 0 end) desc, INPDT desc";
            var res = Query<ASAUDITPOSTMST>(sql, param).ToList();
            return res;
        }

        public List<ASAUDITPOSTMST> getASAUDITPOSTMSTPHONE(AUDITPOSTMSTReq d, string flag = null, SqlTransaction transac = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  [FT_ASAUDITPOSTMST_PHONE] ()  ";
            sql += " where SQNO = '" + d.SQNO + "'";
            sql += " and COMPANY = '" + d.COMPANY + "'";
            sql += " and ASSETNO = '" + d.ASSETNO + "'";
            sql += " and INPID = '" + d.UCODE + "'";
            
            sql += " order by (case when INPID = '" + d.UCODE + "' then 1 else 0 end) desc";
            var res = Query<ASAUDITPOSTMST>(sql, param).ToList();
            return res;
        }

        public List<ASAUDITPOSTMST> checkAUDITAssetNo(AuditPostCheckReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  [FT_ASAUDITPOSTMST] () as a ";
            sql += " left outer join [FT_ASAUDITPOSTMST_PHONE] () as b";
            sql += " on b.SQNO = a.SQNO and a.COMPANY = b.COMPANY and b.ASSETNO = a.ASSETNO  and a.INPID = b.INPID";
            sql += " where a.SQNO = '" + d.SQNO + "'";
            sql += " and a.COMPANY = '" + d.COMPANY + "'";
            sql += " and a.ASSETNO = '" + d.ASSETNO + "'";
            sql += " and a.INPID = '" + d.UCODE + "'";

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            sql += "  order by a.INPDT desc";

            var res = Query<ASAUDITPOSTMST>(sql, param).ToList();
            return res;
        }

        public int addAUDITPOSTMST(AuditPostReq d, SqlTransaction transac = null)

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


            var res = ExecuteNonQuery(sql, param);
            return res;
        }

        public int updateAUDITPOSTMST(AUDITPOSTMSTReq d, SqlTransaction transac = null)

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


            var res = ExecuteNonQuery(sql, param);
            return res;
        }

        public int updateAUDITPOSTMSTPHONE(AUDITPOSTMSTReq d, SqlTransaction transac = null)

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


            var res = ExecuteNonQuery(sql, param);
            return res;
        }

        public List<ASAUDITPOSTTRN> getAUDITPOSTTRN(AuditPostReq d, SqlTransaction transac = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * ,(select NAMEMPT from [CENTRALDB].[centraldb].[dbo].[vTEMPLOY] where [CODEMPID]= a.INPID) as INPNAME";
            sql += " from  FT_ASAUDITPOSTTRN() as a ";
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

            if (!String.IsNullOrEmpty(d.DEPMST))
            {
                //sql += " and DEPMST = '" + d.DEPMST + "'";
                sql += "and DEPCODE in (SELECT [DEPCODE] ";
                sql += " FROM FT_ASAUDITCUTDATE() ";
                sql += " where DEPMST = '" + d.DEPMST + "'";
                sql += " and company = '" + d.COMPANY + "'";
                sql += " group by[DEPCODE])";
            }

            sql += " order by (case when a.INPID = '" + d.UCODE + "' then 1 else 0 end) desc";

            var res = Query<ASAUDITPOSTTRN>(sql, param).ToList();
            return res;
        }

        public int UpdateAUDITPOSTMSTImage(AUDITPOSTMSTReq d, SqlTransaction transac = null)
        {
            if(d == null && ( d!= null && (d.SQNO == null || d.ASSETNO == null))) { return -1; }
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [dbo].[SP_AUDITPOSTMSTIMG]  ";
            sql += " @SQNO  = '" + d.SQNO + "'";
            sql += " ,@COMPANY = '" + d.COMPANY + "'";
            sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
            sql += " ,@IMGPATH = '" + d.IMGPATH + "'";
            sql += " ,@USERID = '" + d.UCODE + "'";;


            var res = ExecuteNonQuery(sql, param);
            return res;
        }



    }
}
