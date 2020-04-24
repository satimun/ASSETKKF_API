using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis
{
    public  class zUserAdo : Base
    {
        private static zUserAdo instant;

        public static zUserAdo GetInstant()
        {
            if (instant == null) instant = new zUserAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private zUserAdo()
        {
        }

        public List<ASSETKKF_MODEL.Data.Mssql.Mcis.zUser> Search(ASSETKKF_MODEL.Data.Mssql.Mcis.zUser d, SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserCode", d.UserCode);
            param.Add("@Code", d.Code);
         //   param.Add("@Description", d.Description);
            param.Add("@Type", d.Type);
            param.Add("@UserName", d.UserName);
            //param.Add("@Email", d.Email);
            param.Add("@Status", d.Status);

            string cmd = "SELECT * FROM zUser " +
                "WHERE (@UserCode IS NULL OR UserCode=@UserCode) " +
                "AND (@Code IS NULL OR Code=@Code) " +
            //    "AND (@Description IS NULL OR UserName=@Description) " +
                "AND (@Type IS NULL OR Type=@Type) " +
                "AND (@Username IS NULL OR Username=@Username) " +
            //    "AND (@Email IS NULL OR Email=@Email) " +
                "AND (@Status IS NULL OR Status=@Status) " +
                ";";
            var res = Query<ASSETKKF_MODEL.Data.Mssql.Mcis.zUser>(cmd, param).ToList();
            return res;
        }

        public int Update(ASSETKKF_MODEL.Data.Mssql.Mcis.zUser d, string EditUser = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Code", d.Code);
        //    param.Add("@Description", d.Description);
            param.Add("@Type", d.Type);
            param.Add("@Username", d.UserName);
        //    param.Add("@Email", d.Email);
      //      param.Add("@Password", d.Password);
       //     param.Add("@SoftPassword", d.SoftPassword);
            param.Add("@UserPw", d.UserPw);
           
            param.Add("@Status", d.Status);
            param.Add("@EditUserId", EditUser);
            param.Add("@UserCode", d.UserCode);

            string cmd = $"UPDATE zUser SET " +
                "Code=@Code, " +
               // "Description=@Description, " +
                "Type=@Type, " +
                "Username=@Username, " +
                //  "Email=@Email, " +
                //  "Password=@Password, " +
                //  "SoftPassword=@SoftPassword, " +
                "UserPw=@UserPw, " +

                "Status=@Status, " +
                "EditUserId=@EditUserId, " +
                "EditDate=GETDATE() " +
                "WHERE UserCode=@UserCode;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int UpdateStatus(String UserCode, string status, string EditUser = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Status", status);
            param.Add("@EditUserId", EditUser);
            param.Add("@UserCode", UserCode);

            string cmd = $"UPDATE zUser SET " +
                "Status=@Status, " +
                "EditUserId=@EditUserId, " +
                "EditDate=GETDATE() " +
                "WHERE UserCode=@UserCode;";
            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }

        public int Insert(ASSETKKF_MODEL.Data.Mssql.Mcis.zUser d, string EditUser = "", SqlTransaction transac = null)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@UserCode", d.UserCode);
            param.Add("@Code", d.Code);
          //  param.Add("@Description", d.Description);
            param.Add("@Type", d.Type);  // 1 = staff, 0 = customer
            param.Add("@Username", d.UserName);
            param.Add("@UserGrp", d.UserGrp);
            //   param.Add("@Email", d.Email);
            //  param.Add("@Password", d.Password);
            //   param.Add("@SoftPassword", d.SoftPassword);
            param.Add("@UserPw", d.UserPw);
            param.Add("@Status", d.Status);
            param.Add("@EditUserId", EditUser);

           // string cmd = "INSERT INTO sxsUser (UserCode, Code, Description, Type, Username, Email, Password, SoftPassword, Status, UpdateBy, Timestamp) " +
           //     "VALUES (@ID, @Code, @Description, @Type, @Username, @Email, @Password, @SoftPassword, @Status, @UpdateBy, GETDATE());";

            string cmd = "INSERT INTO zUser (UserCode, Code,  Type, Username, UserGrp, UserPw, Status, EditUserId, EditDate) " +
                "VALUES (@ID, @Code, @ @Type, @Username, @UserGrp, @UserPw, @Status, @EditUserId, GETDATE());";

            var res = ExecuteNonQuery(transac, cmd, param);
            return res;
        }
    }
}
