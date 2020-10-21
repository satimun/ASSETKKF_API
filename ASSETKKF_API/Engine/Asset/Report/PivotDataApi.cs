using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ASSETKKF_MODEL.Request.Asset;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Report;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ASSETKKF_API.Engine.Asset.Report
{
    public class PivotDataApi : Base<AuditSummaryReq>
    {
        public PivotDataApi(IConfiguration configuration)
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
            Configuration = configuration;
        }

        protected override void ExecuteChild(AuditSummaryReq dataReq, ResponseAPI dataRes)
        {
            PivotDataRes res = new PivotDataRes();
            try
            {
                DBMode = dataReq.DBMode;
                res._result.ServerAddr = ConnectionString();
                var mode = String.IsNullOrEmpty(dataReq.mode) ? null : dataReq.mode.Trim().ToLower();

                switch (mode)
                {
                    case "depcodeol":
                        GetPivotProblemByDepcodeol(dataReq, res);
                        break;

                    default:
                        GetPivotProblemByDep(dataReq, res);
                        break;
                }
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

        private PivotDataRes GetPivotProblemByDep(AuditSummaryReq dataReq, PivotDataRes res)
        {
            try
            {
                var dt = getProblemByDep(dataReq);

                string JSONresult,jsonrow;
                JSONresult = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

                List<string> columns = new List<string>();
                List<string> rows = new List<string>();

                if (dt != null && dt.Columns.Count > 0)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        columns.Add(column.ColumnName);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        //rows.Add(string.Join(";", row.ItemArray.Select(item => item.ToString())));
                        jsonrow = JsonConvert.SerializeObject(row.ItemArray, Newtonsoft.Json.Formatting.Indented);
                        rows.Add(jsonrow);
                    }

                }

                if (!String.IsNullOrEmpty(JSONresult))
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }

                res.columns = columns;
                res.rows = rows;
                res.jsonresult = JSONresult;
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;

            
        }

        private PivotDataRes GetPivotProblemByDepcodeol(AuditSummaryReq dataReq, PivotDataRes res)
        {
            try
            {
                var dt = getProblemByDepcodeol(dataReq);

                string JSONresult, jsonrow;
                JSONresult = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

                List<string> columns = new List<string>();
                List<string> rows = new List<string>();

                if (dt.Columns.Count > 0)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        columns.Add(column.ColumnName);
                    }

                    foreach (DataRow row in dt.Rows)
                    {
                        //rows.Add(string.Join(";", row.ItemArray.Select(item => item.ToString())));
                        jsonrow = JsonConvert.SerializeObject(row.ItemArray, Newtonsoft.Json.Formatting.Indented);
                        rows.Add(jsonrow);
                    }

                }

                if (!String.IsNullOrEmpty(JSONresult))
                {
                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";
                }
                else
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }

                res.columns = columns;
                res.rows = rows;
                res.jsonresult = JSONresult;
            }
            catch (Exception ex)
            {
                res._result._code = "500 ";
                res._result._message = ex.Message;
                res._result._status = "Internal Server Error";
            }

            return res;


        }

        public  DataTable getProblemByDep(AuditSummaryReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Report.PivotDataAdo.GetInstant(conString).getProblemByDep(dataReq)).Result;
        }

        public DataTable getProblemByDepcodeol(AuditSummaryReq dataReq)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Report.PivotDataAdo.GetInstant(conString).getProblemByDepcodeol(dataReq)).Result;
        }

    }
}
