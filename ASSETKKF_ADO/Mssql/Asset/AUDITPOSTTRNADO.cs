using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response.Asset;
using Dapper;

namespace ASSETKKF_ADO.Mssql.Asset
{
    public class AUDITPOSTTRNADO : Base
    {
        private static AUDITPOSTTRNADO instant;

        public static AUDITPOSTTRNADO GetInstant()
        {
            if (instant == null) instant = new AUDITPOSTTRNADO();
            return instant;
        }

        private string conectStr { get; set; }

        private AUDITPOSTTRNADO()
        {
        }

        public int addAUDITPOSTTRN(AUDITPOSTTRNReq d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            sql = " EXEC [dbo].[SP_AUDITPOSTTRN]  ";
			sql += " @SQNO  = '" + d.SQNO + "'";
			sql += " ,@COMPANY = '" + d.COMPANY + "'";
			sql += " ,@DEPMST = '" + d.DEPMST + "'";
			sql += " ,@YR= " + d.YR ;
			sql += " ,@MN = " + d.MN ;
			sql += " ,@YRMN = " + d.YRMN ;
			sql += ", @FINDY = '" + d.FINDY + "'";
			sql += " ,@PCODE = '" + d.PCODE + "'";
			sql += " ,@PNAME = '" + d.PNAME + "'";
			sql += " ,@MEMO1 = '" + d.MEMO1 + "'";
			sql += " ,@CUTDT = '" + d.CUTDT + "'";
			sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
			sql += ", @ASSETNAME = '" + d.ASSETNAME + "'";
			sql += ", @OFFOLD = '" + d.OFFOLD + "'";
			sql += " ,@OFFNAMOLD = '" + d.OFFNAMOLD + "'";
			sql += " ,@OFFICECODE = '" + d.OFFICECODE + "'";
			sql += " ,@OFNAME = '" + d.OFNAME + "'";
			sql += " ,@DEPCODE = '" + d.DEPCODE + "'";
			sql += " ,@DEPCODEOL = '" + d.DEPCODEOL + "'";
			sql += " ,@STNAME = '" + d.STNAME + "'";
			sql += " ,@LEADERCODE = '" + d.LEADERCODE + "'";
			sql += " ,@LEADERNAME = '" + d.LEADERNAME + "'";
			sql += " ,@AREANAME = '" + d.AREANAME + "'";
			sql += " ,@AREACODE = '" + d.AREACODE + "'";
			sql += " ,@USERID = '" + d.UCODE + "'";
			sql += " ,@MODE = '" + d.MODE + "'";
			sql += " ,@POSITNAME = '" + d.POSITNAME + "'";

			var res = ExecuteNonQuery(sql, param);
			return res;

		}

		public int UpdateAUDITPOSTTRNIMG(AUDITPOSTTRNReq d, SqlTransaction transac = null)
		{
			if (d == null && (d != null && (d.SQNO == null || d.ASSETNO == null))) { return -1; }
			DynamicParameters param = new DynamicParameters();
			sql = " EXEC [dbo].[SP_AUDITPOSTTRNIMG]  ";
			sql += " @SQNO  = '" + d.SQNO + "'";
			sql += " ,@COMPANY = '" + d.COMPANY + "'";
			sql += " ,@ASSETNO = '" + d.ASSETNO + "'";
			sql += " ,@IMGPATH = '" + d.IMGPATH + "'";
			sql += " ,@USERID = '" + d.UCODE + "'"; ;


			var res = ExecuteNonQuery(sql, param);
			return res;
		}

		public List<ASSETOFFICECODE> getASSETOFFICECODELST(ASSETOFFICECODEReq d, SqlTransaction transac = null)
		{
			DynamicParameters param = new DynamicParameters();
			param.Add("@OFFICECODE", d.OFFICECODE);
			sql = " select OFFICECODE,MAX(OFNAME) AS OFNAME,DEPCODEOL,MAX(STNAME) AS STNAME,DEPCODE from  [FT_ASFIXEDASSET] (@OFFICECODE)  ";
			sql += " where SALEDT IS NULL";
			sql += " and COMPANY = '" + d.COMPANY + "'";
			if (!String.IsNullOrEmpty(d.DEPCODEOL))
			{
				sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
			}

			if (!String.IsNullOrEmpty(d.OFFICECODE))
			{
				sql += " and OFFICECODE = '" + d.OFFICECODE + "'";
			}
			sql += " GROUP BY OFFICECODE,DEPCODEOL,DEPCODE";
			var res = Query<ASSETOFFICECODE>(sql, param).ToList();
			return res;

		}

		public List<ASSETASSETNO> getASSETASSETNOLST(ASSETASSETNOReq d, SqlTransaction transac = null)
		{
			DynamicParameters param = new DynamicParameters();
			param.Add("@OFFICECODE", "");
			sql = " select * from  [FT_ASFIXEDASSET] (@OFFICECODE)  ";
			sql += " where COMPANY = '" + d.COMPANY + "'";
			
			var res = Query<ASSETASSETNO>(sql, param).ToList();
			return res;

		}

		public ASSETOFFICECODE checkASSETOFFICECODE(ASSETOFFICECODEReq d, SqlTransaction transac = null)
		{
			DynamicParameters param = new DynamicParameters();
			param.Add("@OFFICECODE", d.OFFICECODE);
			sql = " select OFFICECODE,MAX(OFNAME) AS OFNAME,DEPCODEOL,MAX(STNAME) AS STNAME,DEPCODE from  [FT_ASFIXEDASSET] (@OFFICECODE)  ";
			sql += " where SALEDT IS NULL";
			sql += " and COMPANY = '" + d.COMPANY + "'";
			
			if (!String.IsNullOrEmpty(d.OFFICECODE))
			{
				sql += " and OFFICECODE = '" + d.OFFICECODE + "'";
			}
			sql += " GROUP BY OFFICECODE,DEPCODEOL,DEPCODE";
			var res = Query<ASSETOFFICECODE>(sql, param).FirstOrDefault();
			return res;
		}

		public ASSETASSETNO checkASSETASSETNO(ASSETASSETNOReq d, SqlTransaction transac = null)
		{
			DynamicParameters param = new DynamicParameters();
			param.Add("@OFFICECODE", "");
			sql = " select * from  [FT_ASFIXEDASSET] (@OFFICECODE)  ";
			sql += " where COMPANY = '" + d.COMPANY + "'";
			if (!String.IsNullOrEmpty(d.ASSETNO))
			{
				sql += " and ASSETNO = '" + d.ASSETNO + "'";
			}

			if (!String.IsNullOrEmpty(d.DEPCODEOL))
			{
				sql += " and DEPCODEOL = '" + d.DEPCODEOL + "'";
			}

			var res = Query<ASSETASSETNO>(sql, param).FirstOrDefault();
			return res;
		}

		public ASSETASSETNO getASSETASSETNO(ASSETASSETNOReq d, SqlTransaction transac = null)
		{
			DynamicParameters param = new DynamicParameters();
			sql = " SELECT FORMATMESSAGE('%s%d', '" + d.ASSETNO + "', (COUNT(assetno)+1))  as assetno FROM [dbo].[FT_ASAUDITPOSTTRN] ()   ";
			sql += " where COMPANY = '" + d.COMPANY + "'";
			sql += " and not (ASSETNO like (COMPANY + '%'))";
			if (!String.IsNullOrEmpty(d.ASSETNO))
			{
				sql += " and ASSETNO like ( '" + d.ASSETNO + "%')";
			}

			

			var res = Query<ASSETASSETNO>(sql, param).FirstOrDefault();
			return res;
		}

		public List<AuditPostTRN> getAUDITPOSTTRN(AuditPostTRNReq d, SqlTransaction transac = null)

		{
			DynamicParameters param = new DynamicParameters();
			sql = " select * from  FT_ASAUDITPOSTTRN()  as a ";
			sql += " left outer join [FT_ASAUDITPOSTTRN_PHONE] () as b";
			sql += " on b.SQNO = a.SQNO and a.COMPANY = b.COMPANY and b.ASSETNO = a.ASSETNO";
			sql += " where a.SQNO = '" + d.SQNO + "'";
			sql += " and a.COMPANY = '" + d.COMPANY + "'";
			sql += " and a.DEPCODEOL = '" + d.DEPCODEOL + "'";
			if (!String.IsNullOrEmpty(d.AREACODE))
			{
				sql += " and a.POSITCODE = '" + d.AREACODE + "'";
			}
			if (!String.IsNullOrEmpty(d.ASSETNO))
			{
				sql += " and a.ASSETNO = '" + d.ASSETNO + "'";
			}

			var obj = Query<ASAUDITPOSTTRN>(sql, param).ToList();
			List<AuditPostTRN> res = new List<AuditPostTRN>();


			if (obj != null && obj.Count > 0)
			{
				obj.ForEach(x => {
					res.Add(new AuditPostTRN
					{
						YR = x.YR,
						MN = x.MN,
						YRMN = x.YRMN,
						SQNO = x.SQNO,
						DEPMST = x.DEPMST,
						FINDY = x.FINDY,
						PCODE = x.PCODE,
						PNAME = x.PNAME,
						MEMO1 = x.MEMO1,
						ASSETNO = x.ASSETNO,
						ASSETNAME = x.ASSETNAME,
						OFFICECODE = x.OFFICECODE,
						OFNAME = x.OFNAME,
						DEPCODE = x.DEPCODE,

						DEPCODEOL = x.DEPCODEOL,
						STNAME = x.STNAME,
						LEADERCODE = x.LEADERCODE,
						IMGPATH = x.IMGPATH,

					});

				});
			}

			return res;
		}

		public List<ASAUDITPOSTTRN> getAuditPostTRN(AUDITPOSTTRNReq d, SqlTransaction transac = null)
		{
			DynamicParameters param = new DynamicParameters();
			
			sql = " select * from  FT_ASAUDITPOSTTRN ()  ";
			sql += " where COMPANY = '" + d.COMPANY + "'";
			sql += " and sqno = '" + d.SQNO + "'";
			sql += " and INPID = '" + d.UCODE + "'"; 
			 sql += " and assetno = '" + d.ASSETNO + "'";

			var res = Query<ASAUDITPOSTTRN>(sql, param).ToList();
			return res;

		}

	}
}
