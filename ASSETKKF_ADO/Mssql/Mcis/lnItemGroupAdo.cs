using Core.Util;
using Dapper;
using ASSETKKF_MODEL.Enum;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public class lnItemGroupAdo : Base
    {
        private static lnItemGroupAdo instant;

        public static lnItemGroupAdo GetInstant()
        {
            if (instant == null) instant = new lnItemGroupAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private lnItemGroupAdo() { }


        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup> ListActive()
        {
            string cmd = " SELECT * FROM mcis.dbo.lnItemGroup ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup>(cmd, null).ToList();

            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup> GetDataSeq(ASSETKKF_MODEL.Request.Mcis.lnItemGroupReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Seq_Itemgroup", d.Seq_Itemgroup);
            param.Add("@Item_Group", d.Item_Group);
            param.Add("@Description", d.Description);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM mcis.dbo.lnItemGroup " +
            $"WHERE  Seq_Itemgroup = @Seq_Itemgroup  ";

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup>(cmd, param).ToList();
            return res;

        }   

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup> GetData(ASSETKKF_MODEL.Request.Mcis.lnItemGroupReq d)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Seq_Itemgroup", d.Seq_Itemgroup);
            param.Add("@Item_Group", d.Item_Group);
            param.Add("@Description", d.Description);
            //param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");

            string cmd = "SELECT * FROM mcis.dbo.lnItemGroup " +
            $"WHERE  Seq_Itemgroup = @Seq_Itemgroup  ";
            
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup>(cmd, param).ToList();
            return res;
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup> Search(ASSETKKF_MODEL.Request.Mcis.lnItemGroupReq d)
        {
            DynamicParameters param = new DynamicParameters();
            /*
            param.Add("@Seq_ItemgroupIsNull", d.Seq_Itemgroup.ListNull()); 
            param.Add("@Item_GroupIsNull", d.Item_Group.ListNull()); 
            param.Add("@DescriptionIsNull", d.Description.ListNull()); 
            param.Add("@txtSearch", $"%{d.txtSearch.GetValue()}%");
            */

            string cmd = "";
            /*
            string cmd = "SELECT * FROM mcis.dbo.lnItemGroup " +
            $"WHERE (@Seq_ItemgroupIsNull IS NULL OR Seq_Itemgroup IN ('{ d.Seq_Itemgroup.Join("','") }')) " +
            $"AND (@Item_GroupIsNull IS NULL OR Item_Group IN ('{ d.Item_Group.Join("','") }')) " +
            $"AND (@DescriptionIsNull IS NULL OR Description IN ('{ d.Description.Join("','") }')) " +
            $"AND (Seq_Itemgroup LIKE @txtSearch OR Seq_Itemgroup LIKE @txtSearch) " +  
            //"ORDER BY  ;"; 
            */

            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup>(cmd, param).ToList();
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@Seq_Itemgroup", d.seq_itemgroup);
            param.Add("@Item_Group", d.itemgroup);
            param.Add("@Description", d.description);
            string cmd = "INSERT INTO mcis.dbo.lnItemGroup " +
            $"      (Seq_Itemgroup, Item_Group, Description) " +
            $"VALUES(@Seq_Itemgroup, @Item_Group, @Description); " +
            $"SELECT SCOPE_IDENTITY();";
            return ExecuteScalar<int>(cmd, param);
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@Seq_Itemgroup", d.seq_itemgroup);
            param.Add("@Item_Group", d.itemgroup.GetValue());
            param.Add("@Description", d.description.GetValue());

            string cmd   = "UPDATE mcis.dbo.lnItemGroup "+
            "SET Item_Group = @Item_Group "+ 
            "  , Description = @Description "+ 
            "WHERE Seq_Itemgroup = @Seq_Itemgroup "+              
            " "; 
     

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Delete(ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup d, string userID = "", SqlTransaction transac = null)
        {
            var param = new Dapper.DynamicParameters();
            param.Add("@Seq_Itemgroup", d.seq_itemgroup);
            param.Add("@Item_Group", d.itemgroup.GetValue());
            param.Add("@Description", d.description.GetValue());
            string cmd = "";
            /*
                   cmd = DELETE FROM mcis.dbo.lnItemGroup "+
            "WHERE Seq_Itemgroup = @.Seq_Itemgroup "+ 
            " AND Item_Group = @.Item_Group "+ 
            " AND Description = @.Description "+ 
            " "; 
            */

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Save(ASSETKKF_MODEL.Data.Mssql.Mcis.lnItemGroup d, string userID = "", SqlTransaction transac = null)
        {

       
                var req = new ASSETKKF_MODEL.Request.Mcis.lnItemGroupReq();
                req.Seq_Itemgroup = d.seq_itemgroup;

                if (GetDataSeq(req).Count > 0)
                {
                    return Update(d, userID, transac);

                }

                return Insert(d, userID, transac);

           
        }




    }
}
