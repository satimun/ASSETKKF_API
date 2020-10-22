using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.File;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset.File
{
    public class AuditPostTRNUploadAPI : Base<AUDITPOSTTRNReq>
    {
        public AuditPostTRNUploadAPI(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }


        protected override void ExecuteChild(AUDITPOSTTRNReq dataReq, ResponseAPI dataRes)
        {
            var res = new AuditUploadRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                res._result.DBMode = DBMode;

                if (dataReq != null && System.IO.File.Exists(dataReq.IMGPATH)) { }
                {

                    var obj = ASSETKKF_ADO.Mssql.Asset.AUDITPOSTTRNADO.GetInstant().UpdateAUDITPOSTTRNIMG(dataReq,null,conString);
                    res.fullpath = dataReq.IMGPATH;
                }

                res._result._code = "200";
                res._result._message = "";
                res._result._status = "OK";


            }
            catch (SqlException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Execute exception Error";
            }
            catch (InvalidOperationException ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Connection Exception Error";
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            dataRes.data = res;

        }

    }
}
