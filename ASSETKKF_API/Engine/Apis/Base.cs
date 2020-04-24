using Core.Recaptha;
using Core.Util;

using ASSETKKF_API.Constant;
using ASSETKKF_MODEL.Enum;
using ASSETKKF_MODEL.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Apis
{
    
        public abstract class Base<TRequest>
        {
            protected string AccessToken { get; set; }
            protected string IPAddress { get; set; }
            protected string UserAgent { get; set; }

            protected string Token { get; set; }

            protected string RecaptchaResponse { get; set; }

            protected string PermissionKey = "PUBLIC";
            protected bool AllowAnonymous = false;
            protected bool RecaptchaRequire = false;
            protected bool CheckVerify = false;

            //protected Model.Table.Mssql.sxsUser User { get; set; }

            protected string UserCode { get; set; }

            protected abstract void ExecuteChild(TRequest dataReq, ResponseAPI dataRes);

            public ResponseAPI Execute(HttpContext context, dynamic dataReq = null)
            {
                ResponseAPI res = new ResponseAPI();

                StringValues HToken;
                context.Request.Headers.TryGetValue("Token", out HToken);
                Token = HToken.ToString();

                DateTime StartTime = DateTime.Now;
                string StackTraceMsg = string.Empty;
                try
                {
                    StringValues HAccessToken;
                    context.Request.Headers.TryGetValue("AccessToken", out HAccessToken);
                    AccessToken = HAccessToken.ToString();

                    IPHostEntry heserver = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress = /*string.Join(',', heserver.AddressList.Select(x => x.ToString()).ToList());*/ context.Connection.RemoteIpAddress.ToString();

                    StringValues HUserAgent;
                    context.Request.Headers.TryGetValue("User-Agent", out HUserAgent);
                    UserAgent = HUserAgent.ToString();

                    StringValues HRecaptchaResponse;
                    context.Request.Headers.TryGetValue("RecaptchaResponse", out HRecaptchaResponse);
                    RecaptchaResponse = HRecaptchaResponse.ToString();

               // if (!this.GetType().Name.Equals("OauthAccessTokenGet")) this.ValidatePermission();
               /*
                    StringValues Husercode;
                    context.Request.Headers.TryGetValue("UserCode", out Husercode);
                    UserCode = Husercode.ToString();
               */

                    if (dataReq != null)
                    {
                        try
                        {
                            dataReq = this.MappingRequest(dataReq);
                        }
                        catch (Exception)
                        {
                            dataReq = this.MappingRequestArr(dataReq);
                        }

                    }

                    this.ExecuteChild(dataReq, res);

                    res.code = "S0001";
                    res.message = "SUCCESS";
                    res.status = "S";

                }
                catch (Exception ex)
                {
                    StackTraceMsg = ex.StackTrace;
                    //map error code, message
                    ErrorCode error = EnumUtil.GetEnum<ErrorCode>(ex.Message);
                    res.code = error.ToString();
                    if (res.code == ErrorCode.U000.ToString())
                    {
                        res.message = ex.Message;
                    }
                    else
                    {
                        res.message = error.GetDescription();
                    }

                    res.status = "F";
                }
                finally
                {
                    ASSETKKF_ADO.Mssql.Mcis.muAPILogAdo.GetInstant().Insert(new ASSETKKF_MODEL.Data.Mssql.Mcis.muAPILog()
                    {
                        Token = Token,
                        APIName = this.GetType().Name,
                        //RefID = this.Logger.RefID,
                        ServerName = Environment.MachineName,
                        StartDate = StartTime,
                        EndDate = DateTime.Now,
                        Status = res.status,
                        StatusMessage = res.message,
                        Input = this.GetType().Name.Equals("OauthLogin") ? "" : JsonConvert.SerializeObject(dataReq),
                        Output = JsonConvert.SerializeObject(res),
                        Remark = StackTraceMsg
                    });
                }
                return res;
            }

            private TRequest MappingRequest(dynamic dataReq)
            {
                string json = dataReq is string ? (string)dataReq : JsonConvert.SerializeObject(dataReq);
                return JsonConvert.DeserializeObject<TRequest>(json);
            }

            private List<TRequest> MappingRequestArr(dynamic dataReq)
            {
                string json = dataReq is string ? (string)dataReq : JsonConvert.SerializeObject(dataReq);
                return JsonConvert.DeserializeObject<List<TRequest>>(json);
            }

            private void ValidatePermission()
            {
            /*
                if (RecaptchaRequire)
                {
                    if (!Recaptha.ReCaptchaPassed(RecaptchaResponse))
                    {
                        throw new Exception(ErrorCode.V000.ToString());
                    }
                }
*/
                // check access token
                var accesskey = StaticValue.GetInstant().muAccessToken.Where(x => x.Code.Equals(AccessToken)).FirstOrDefault();
             //   if (accesskey == null) { throw new Exception(ErrorCode.O000.ToString()); }

                // check token
                if (!AllowAnonymous)
                {
                    var token = StaticValue.GetInstant().muToken?.Where(x => x.Code.Equals(Token) && x.AccessToken_Code.Equals(AccessToken)).FirstOrDefault();

                    if (token == null) { throw new Exception(ErrorCode.O000.ToString()); }
                    if (token.Status != "A") { throw new Exception(ErrorCode.O001.ToString()); }
                    if (token.ExpiryTime.ToLocalTime().Ticks < DateTime.Now.Ticks) { throw new Exception(ErrorCode.O002.ToString()); }

                    // set user id
                    UserCode = token.UserCode;

                    if (!PermissionKey.Equals("PUBLIC"))
                    {
                        //if (!PermissionADO.GetInstant().CheckPermission(this.token, "C", this.PermissionKey).Any(x => x.Code == this.PermissionKey)) { throw new Exception(ErrorCode.P000.ToString()); }
                    }

                }

            }

        }
    
}
