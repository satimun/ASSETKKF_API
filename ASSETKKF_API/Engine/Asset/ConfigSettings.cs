using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using Microsoft.Extensions.Configuration;

namespace ASSETKKF_API.Engine.Asset
{
    public class ConfigSettings : Base<ConfigSetting>
    {
        public ConfigSettings(IConfiguration configuration)
        {
            PermissionKey = "ADMIN";
            Configuration = configuration;

        }


        protected override void ExecuteChild(ConfigSetting dataReq, ResponseAPI dataRes)
        {
            var res = new ConfigSetting();
            try
            {
                DBMode = dataReq.DBMode;
                dataRes.ServerAddr = ConnectionString();
                if (dataReq != null)
                {
                    res.DBMode = dataReq.DBMode;
                    res.Status = true;
                    switch (dataReq.DBMode)
                    {                        
                        case "1":
                            ASSETKKF_ADO.Mssql.Base.conString = Configuration["ConnAssetKKFBak"];
                            res.ConnStr = ASSETKKF_ADO.Mssql.Base.conString;                            

                            break;

                        case "2":
                            ASSETKKF_ADO.Mssql.Base.conString = Configuration["ConnAssetKKFLocal"];
                            res.ConnStr = ASSETKKF_ADO.Mssql.Base.conString;

                            break;
                        default:
                            ASSETKKF_ADO.Mssql.Base.conString =  Configuration["ConnAssetKKF"];
                            res.ConnStr = ASSETKKF_ADO.Mssql.Base.conString;
                            
                            break;
                    }

                    if (!String.IsNullOrEmpty(res.ConnStr))
                    {
                        var arrCon = res.ConnStr.Split(";");
                        if (arrCon.Length > 0)
                        {
                            var arrServer = arrCon[0].Split("=");
                            res.ServerAddr = arrServer[1];
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                res.Status = false;
                res.Message = "ไม่สามารถกำหนดค่า Connection String ได้";
            }
            dataRes.data = res;
        }
    }
}
