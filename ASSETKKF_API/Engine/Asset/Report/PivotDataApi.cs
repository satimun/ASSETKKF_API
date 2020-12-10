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
                res._result.DBMode = DBMode;

                var mode = String.IsNullOrEmpty(dataReq.mode) ? null : dataReq.mode.Trim().ToLower();

                switch (mode)
                {
                    case "depcodeol":
                        GetPivotProblemByDepcodeol(dataReq, res,conString);
                        break;

                    default:
                        GetPivotProblemByDep(dataReq, res,conString);
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

        private PivotDataRes GetPivotProblemByDep(AuditSummaryReq dataReq, PivotDataRes res, string conStr = null)
        {
            try
            {
                var dt = getProblemByDep(dataReq,conStr);

                string JSONresult,jsonrow;
                

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
                        var depmst = row[0].ToString();
                        AuditSummaryReq req1 = new AuditSummaryReq();
                        req1 = dataReq;
                        req1.depmst = depmst;

                        var obj = ASSETKKF_ADO.Mssql.Report.PivotDataAdo.GetInstant().getQuantityByDep(req1, null, conStr).FirstOrDefault();

                        if(obj != null )
                        {

                            row[2] = obj.QTY_ASSET;
                            row[3] = obj.QTY_AUDIT;
                        }



                        //rows.Add(string.Join(";", row.ItemArray.Select(item => item.ToString())));
                        jsonrow = JsonConvert.SerializeObject(row.ItemArray, Newtonsoft.Json.Formatting.Indented);
                        rows.Add(jsonrow);
                    }

                }

                JSONresult = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

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

        private PivotDataRes GetPivotProblemByDepcodeol(AuditSummaryReq dataReq, PivotDataRes res, string conStr = null)
        {
            try
            {
                var dt = getProblemByDepcodeol(dataReq,conStr);

                string JSONresult, jsonrow;
                

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
                        var depcodeeol = row[0].ToString();
                        AuditSummaryReq req1 = new AuditSummaryReq();
                        req1 = dataReq;
                        req1.DEPCODEOL = depcodeeol;

                        var obj = ASSETKKF_ADO.Mssql.Report.PivotDataAdo.GetInstant().getQuantityByDEPCODEOL(req1, null, conStr).FirstOrDefault();

                        if (obj != null)
                        {

                            row[2] = obj.QTY_ASSET;
                            row[3] = obj.QTY_AUDIT;
                        }

                        //rows.Add(string.Join(";", row.ItemArray.Select(item => item.ToString())));
                        jsonrow = JsonConvert.SerializeObject(row.ItemArray, Newtonsoft.Json.Formatting.Indented);
                        rows.Add(jsonrow);
                    }

                }

                JSONresult = JsonConvert.SerializeObject(dt, Newtonsoft.Json.Formatting.Indented);

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

        public  DataTable getProblemByDep(AuditSummaryReq dataReq, string conStr = null)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Report.PivotDataAdo.GetInstant().getProblemByDep(dataReq,null,conStr)).Result;
        }

        public DataTable getProblemByDepcodeol(AuditSummaryReq dataReq, string conStr = null)
        {
            return Task.Run(() => ASSETKKF_ADO.Mssql.Report.PivotDataAdo.GetInstant().getProblemByDepcodeol(dataReq,null,conStr)).Result;
        }

    }
}
