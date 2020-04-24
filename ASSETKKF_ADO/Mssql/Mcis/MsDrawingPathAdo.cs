using Core.Util;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class MsDrawingPathAdo : Base
    {
        private static MsDrawingPathAdo instant;

        public static MsDrawingPathAdo GetInstant()
        {
            if (instant == null) instant = new MsDrawingPathAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private MsDrawingPathAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.MsDrawingPath ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath> GetData(ASSETKKF_MODEL.Request.Mcis.MsDrawingPathReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@Itemno", d.Itemno);
            param.Add("@PathName", d.PathName);
            param.Add("@PathLink", d.PathLink);
            param.Add("@EditUser", d.EditUser);
            param.Add("@EditDate", d.EditDate);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * ,ROW_NUMBER() OVER(ORDER BY DrawingCd,itemno) AS RowID" +
                $"    , (SELECT TOP 1 DrawingNmTh FROM mmDrawing WHERE mmDrawing.DrawingCd = mcis.dbo.MsDrawingPath.DrawingCd) AS DrawingNmTh  " +
                $" FROM mcis.dbo.MsDrawingPath " +
            $"WHERE (@DrawingCd IS NULL OR DrawingCd = @DrawingCd) " +
            $"  AND (@Itemno IS NULL OR Itemno = @Itemno) " +
            $"  AND (@PathName IS NULL OR PathName = @PathName) " +
            $"  AND (@PathLink IS NULL OR PathLink = @PathLink) " +
            $"  AND (@EditUser IS NULL OR EditUser = @EditUser) " +
            $"  AND (@EditDate IS NULL OR EditDate = @EditDate) " +
            //$"AND (DrawingCd LIKE @txtSearch OR DrawingCd LIKE @txtSearch) " +  
            "ORDER BY  ;"; 
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath> Search(ASSETKKF_MODEL.Request.Mcis.MsDrawingPathReq d)
        {
            DynamicParameters param = new DynamicParameters();
           
           param.Add("@DrawingCdIsNull", $"%{d.DrawingCd.Trim()}%");
            /*
           param.Add("@ItemnoIsNull", d.Itemno.ListNull()); 
           param.Add("@PathNameIsNull", d.PathName.ListNull()); 
           param.Add("@PathLinkIsNull", d.PathLink.ListNull()); 
           param.Add("@EditUserIsNull", d.EditUser.ListNull()); 
           param.Add("@EditDateIsNull", d.EditDate.ListNull()); 

           param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
           */

             
             
            string cmd = "SELECT * ,ROW_NUMBER() OVER(ORDER BY DrawingCd,itemno) AS RowID" +  
                $"    , (SELECT TOP 1 DrawingNmTh FROM mmDrawing WHERE mmDrawing.DrawingCd = mcis.dbo.MsDrawingPath.DrawingCd) AS DrawingNmTh  " +
                $" FROM mcis.dbo.MsDrawingPath " +
                $"WHERE (DrawingCd LIKE @DrawingCdIsNull )   " +
            //$"WHERE (DrawingCd LIKE '%"+d.DrawingCd.Trim()+"%')   " +
            /*
            $"AND (@ItemnoIsNull IS NULL OR Itemno IN ('{ d.Itemno.Join("','") }')) " +
            $"AND (@PathNameIsNull IS NULL OR PathName IN ('{ d.PathName.Join("','") }')) " +
            $"AND (@PathLinkIsNull IS NULL OR PathLink IN ('{ d.PathLink.Join("','") }')) " +
            $"AND (@EditUserIsNull IS NULL OR EditUser IN ('{ d.EditUser.Join("','") }')) " +
            $"AND (@EditDateIsNull IS NULL OR EditDate IN ('{ d.EditDate.Join("','") }')) " +
            $"AND (DrawingCd LIKE @txtSearch OR DrawingCd LIKE @txtSearch) " +  
            */
            $"ORDER BY  DrawingCd;"; 
            

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath d, string userID = "")
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd);
            param.Add("@Itemno", d.Itemno);
            param.Add("@PathName", d.PathName);
            param.Add("@PathLink", d.PathLink);
            param.Add("@EditUser", d.EditUser);
            param.Add("@EditDate", d.EditDate);
            string cmd = "INSERT INTO mcis.dbo.MsDrawingPath " +
            $"      (DrawingCd, Itemno, PathName, PathLink, EditUser, EditDate) " +
            $"VALUES(@DrawingCd, @Itemno, @PathName, @PathLink, @EditUser, @EditDate); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@Itemno", d.Itemno);
            param.Add("@PathName", d.PathName.GetValue());
            param.Add("@PathLink", d.PathLink.GetValue());
            param.Add("@EditUser", d.EditUser.GetValue());
            param.Add("@EditDate", d.EditDate);
            string cmd = "";
            /*
                   cmd = UPDATE mcis.dbo.MsDrawingPath "+
            "SET DrawingCd = @.DrawingCd "+ 
            " , Itemno = @.Itemno "+ 
            " , PathName = @.PathName "+ 
            " , PathLink = @.PathLink "+ 
            " , EditUser = @.EditUser "+ 
            " , EditDate = @.EditDate "+ 
            "WHERE DrawingCd = @.DrawingCd "+ 
            " AND Itemno = @.Itemno "+ 
            " AND PathName = @.PathName "+ 
            " AND PathLink = @.PathLink "+ 
            " AND EditUser = @.EditUser "+ 
            " AND EditDate = @.EditDate "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.MsDrawingPath d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@DrawingCd", d.DrawingCd.GetValue());
            param.Add("@Itemno", d.Itemno);
            param.Add("@PathName", d.PathName.GetValue());
            param.Add("@PathLink", d.PathLink.GetValue());
            param.Add("@EditUser", d.EditUser.GetValue());
            param.Add("@EditDate", d.EditDate);
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.MsDrawingPath "+
            "WHERE DrawingCd = @.DrawingCd "+ 
            " AND Itemno = @.Itemno "+ 
            " AND PathName = @.PathName "+ 
            " AND PathLink = @.PathLink "+ 
            " AND EditUser = @.EditUser "+ 
            " AND EditDate = @.EditDate "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        
    }







}
