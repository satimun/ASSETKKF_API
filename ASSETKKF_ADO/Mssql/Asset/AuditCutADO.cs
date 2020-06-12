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
            List<ASSETKKF_MODEL.Response.Asset.AuditCutList> res = new List<ASSETKKF_MODEL.Response.Asset.AuditCutList>();

            if (obj != null && obj.Count > 0)
            {
                obj.ForEach(x => {
                    res.Add(new ASSETKKF_MODEL.Response.Asset.AuditCutList
                    {
                        id = x.SQNO,
                        descriptions = x.Audit_NO + " : " + x.SQNO + " ( " + x.Company + " )",
                        dept = x.DEPMST + " : " + x.DEPNM,
                        DEPCODEOL = x.DEPCODEOL,
                        COMPANY = x.Company,
                        audit_no = x.Audit_NO,
                        YR = x.YR,
                        MN = x.MN,
                        YRMN = x.YRMN,
                        DEPCODE = x.DEPCODE,
                        DEPMST = x.DEPMST,
                        CUTDT = x.CUTDT,
                        STNAME = x.STNAME,

                    }); ;
                });
            }

            
            return res;
        }

        public List<ASSETKKF_MODEL.Request.Asset.AuditCutList> getAuditCutNoList(ASSETKKF_MODEL.Request.Asset.AuditCutReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            //string cmd = "SELECT M.Company,DEPCODEOL,M.SQNO as ID,(isnull(M.Audit_NO,'') + ' : ' + M.SQNO + ' ( ' + M.Company + ' ) ') as Descriptions";
            //cmd += ",( M.DEPMST + ' : ' + DEPNM)  as Dept";
            string cmd = " SELECT M.Company,M.SQNO,max(isnull(DEPCODEOL,'')) as DEPCODEOL,isnull(M.Audit_NO,'') as Audit_NO";
            cmd += " ,M.DEPMST,DEPNM,M.YR,M.MN,M.YRMN,max(D.DEPCODE) as DEPCODE,max(D.CUTDT) as CUTDT,max(D.STNAME) as STNAME";
            cmd += " from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
            cmd += " where D.SQNO = M.SQNO ";
            cmd += " and D.Company = M.Company ";
            cmd += " and  M.FLAG not in ('X','C')";
            if (!String.IsNullOrEmpty(d.Company))
            {
                var comp = "";
                comp = "'" + d.Company.Replace(",", "','") + "'";
                cmd += " and M.COMPANY in (" + comp + ") ";
            }

            if (!d.Menu3 && ((!String.IsNullOrEmpty(d.DeptCode)) || d.DeptLST != null))
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

            cmd += " and M.YR = (SELECT YR from(";
            cmd += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITCUTDATEMST()";
            cmd += " where YR = (SELECT max(YR) FROM FT_ASAUDITCUTDATEMST() )";
            cmd += " group by YR    ) as a)";
            cmd += " and M.MN = (SELECT MN from(";
            cmd += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITCUTDATEMST()";
            cmd += " where YR = (SELECT max(YR) FROM FT_ASAUDITCUTDATEMST() )";
            cmd += " group by YR    ) as b)";
            cmd += " and M.YRMN = (SELECT YRMN from(";
            cmd += " SELECT YR, max(MN) as MN, max(YRMN) as YRMN  FROM FT_ASAUDITCUTDATEMST()";
            cmd += " where YR = (SELECT max(YR) FROM FT_ASAUDITCUTDATEMST() )";
            cmd += " group by YR    ) as c)";

            cmd += " group by M.Company,M.SQNO,M.Audit_NO,M.DEPMST,DEPNM,M.YR,M.MN,M.YRMN";

            var res = Query<ASSETKKF_MODEL.Request.Asset.AuditCutList>(cmd, param).ToList();
            return res;
            
         }

        public List<ASSETKKF_MODEL.Response.Asset.DEPTList> getDeptLst(String sqno, String company, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            string cmd = "Select  DEPCODEOL,MAX(STNAME) AS STNAME,DEPCODE,COMPANY  from  FT_ASAUDITCUTDATE() where 1 = 1";
            //cmd += " where SQNO = '" + sqno + "'";
            //cmd += " and COMPANY = '" + company + "'";
            if (!String.IsNullOrEmpty(company))
            {
                var comp = "";
                comp = "'" + company.Replace(",", "','") + "'";
                cmd += " and COMPANY in (" + comp + ") ";
            }

            if (!String.IsNullOrEmpty(sqno))
            {
                cmd += " and SQNO = '" + sqno + "'";
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

        public List<ASSETKKF_MODEL.Response.Asset.LeaderList> getLeaderLst(String DEPCODEOL, String company,string DeptLST = null,bool Menu3 = false, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = "select distinct OFFICECODE,OFNAME from  FT_UserAsset('') where 1 = 1";
            if (!String.IsNullOrEmpty(company))
            {
                var comp = "";
                comp = "'" + company.Replace(",", "','") + "'";
                sql += " and COMPANY in (" + comp + ") ";
            }

            //sql += " and DEPCODEEOL like (case when isnull('" + DEPCODEOL + "','') <> '' then   SUBSTRING('" + DEPCODEOL + "',1,1) else '' end + '%')";
            if (!Menu3 && ((!String.IsNullOrEmpty(DEPCODEOL)) || DeptLST != null))
            {
                sql += " and (";
                if (!String.IsNullOrEmpty(DEPCODEOL))
                {
                    sql += " DEPCODEEOL like (case when isnull('" + DEPCODEOL + "','') <> '' then   SUBSTRING('" + DEPCODEOL + "',1,1) else '' end + '%')";
                }
                if ((DeptLST != null && DeptLST.Length > 0) && (DeptLST != "null"))
                {
                    var arrDept = DeptLST.Split(",");
                    foreach (string s in arrDept)
                    {
                        sql += " or DEPCODEEOL like (case when isnull('" + s + "','') <> '' then   SUBSTRING('" + s + "',1,1) else '' end + '%')";
                    }

                }
                sql += " )";
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

        public List<String> getDepLikeList(String sqno, String company, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " SELECT case when isnull(DEPCODEOL,'') <> '' then   SUBSTRING(DEPCODEOL,1,2) else '' end as DEPLike FROM [dbo].[FT_ASAUDITCUTDATE] () where 1 = 1";
            if (!String.IsNullOrEmpty(sqno))
            {
                sql += " and SQNO = '" + sqno + "'";
            }
            if (!String.IsNullOrEmpty(company))
            {
                var comp = "";
                comp = "'" + company.Replace(",", "','") + "'";
                sql += " and COMPANY in (" + comp + ") ";
            }
            sql += " group by case when isnull(DEPCODEOL,'') <> '' then   SUBSTRING(DEPCODEOL,1,2) else '' end";

            var res = Query<String>(sql, param).ToList();
            return res;
        }

        public ASAUDITCUTDATEMST getAUDITCUTDATEMST(AuditPostReq d, SqlTransaction transac = null)            

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select M.* from  FT_ASAUDITCUTDATE() D, FT_ASAUDITCUTDATEMST() M ";
            sql += " where D.SQNO = M.SQNO ";
            sql += " and D.Company = M.Company ";
            sql += " and M.SQNO = '" + d.SQNO + "'";
            sql += " and M.COMPANY = '" + d.COMPANY + "'";
            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }
                
            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
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
            

            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }
            sql += " and  M.FLAG not in ('X','C')";

            var res = Query<ASAUDITCUTDATE>(sql, param).ToList();
            return res;
        }

        public List<ASAUDITPOSTMST> getAUDITPOSTMST(AuditPostReq d,string flag = null, SqlTransaction transac = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  [FT_ASAUDITPOSTMST] ()  ";
            sql += " where SQNO = '" + d.SQNO + "'";
            sql += " and COMPANY = '" + d.COMPANY + "'";
           
            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }

            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }

            if(flag == "")
            {
                sql += " and isnull(PCODE,'') = '' ";
            }

            if(!String.IsNullOrEmpty(flag))
            {
                sql += " and isnull(PCODE,'') <> '' ";
            }

            var res = Query<ASAUDITPOSTMST>(sql, param).ToList();
            return res;
        }

        public ASAUDITPOSTMST checkAUDITAssetNo(AuditPostCheckReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  [FT_ASAUDITPOSTMST] ()  ";
            sql += " where SQNO = '" + d.SQNO + "'";
            sql += " and COMPANY = '" + d.COMPANY + "'";
            sql += " and ASSETNO = '" + d.ASSETNO + "'";

            var res = Query<ASAUDITPOSTMST>(sql, param).FirstOrDefault();
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


            var res = ExecuteNonQuery(sql, param);
            return res;
        }

        public List<ASAUDITPOSTTRN> getAUDITPOSTTRN(AuditPostReq d, SqlTransaction transac = null)

        {
            DynamicParameters param = new DynamicParameters();
            sql = " select * from  FT_ASAUDITPOSTTRN()  ";
            sql += " where SQNO = '" + d.SQNO + "'";
            sql += " and COMPANY = '" + d.COMPANY + "'";
            
            if (!String.IsNullOrEmpty(d.DEPCODEOL))
            {
                sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
            }
            if (!String.IsNullOrEmpty(d.AREACODE))
            {
                sql += " and POSITCODE = '" + d.AREACODE + "'";
            }

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
