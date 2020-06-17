using ASSETKKF_MODEL.Data.Mssql.Asset;
using ASSETKKF_MODEL.Request.Report;
using ASSETKKF_MODEL.Response;
using ASSETKKF_MODEL.Response.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASSETKKF_API.Engine.Asset.Dashboard
{
    public class AuditDeptSummaryApi : Base<AuditSummaryReq>
    {
        public AuditDeptSummaryApi()
        {
            AllowAnonymous = true;
            RecaptchaRequire = true;
        }

        protected override void ExecuteChild(AuditSummaryReq dataReq, ResponseAPI dataRes)
        {
            AuditDeptSummaryRes res = new AuditDeptSummaryRes();
            try
            {
                var req = new ASSETKKF_MODEL.Request.Asset.AuditSummaryReq()
                {
                    Company = dataReq.Company,
                    DeptCode = dataReq.DeptCode,
                    DeptLST = dataReq.DeptLST,
                    Menu3 = dataReq.Menu3
                };

                var obj = ASSETKKF_ADO.Mssql.Asset.AuditSummaryADO.GetInstant().GetDEPMSTSummary(req);
                if (obj == null)
                {
                    res._result._code = "404";
                    res._result._message = "ไม่พบข้อมูล";
                    res._result._status = "Bad Request";
                }
                else
                {
                    List<AuditDeptSummary> lst = new List<AuditDeptSummary>();

                    if (obj != null && obj.Count > 0)
                    {
                        obj.ForEach(x => {
                            var reqDep = new ASSETKKF_MODEL.Request.Asset.AuditSummaryReq
                            {
                                Company = dataReq.Company,
                                DeptCode = dataReq.DeptCode,
                                DeptLST = dataReq.DeptLST,
                                Menu3 = dataReq.Menu3,
                                sqno = x.sqno,
                                depmst=x.DEPMST
                            };
                            // List<ASSETKKF_MODEL.Data.Mssql.Asset.AuditSummary>
                            var objDep = ASSETKKF_ADO.Mssql.Asset.AuditSummaryADO.GetInstant().GetSummaryBySqnoDepMst(reqDep);
                            List<AuditDeptSummary> lstDept = new List<AuditDeptSummary>();
                            if (objDep != null && objDep.Count > 0)
                            {
                                objDep.ForEach(y => {
                                    lstDept.Add(new AuditDeptSummary
                                    {
                                        depcodeol = y.Depcodeol,
                                        deptname = y.STName,
                                        qty_total = y.QTY_TOTAL,
                                        qty_wait = y.QTY_WAIT,
                                        qty_checked = y.QTY_CHECKED,
                                        qty_trn = y.QTY_TRN,
                                        progress = y.PROGRESS,
                                        sqno = y.sqno,
                                        audit_no = y.audit_no,
                                        company = y.Company,
                                        DEPMST = y.DEPMST,
                                        DEPNM = y.DEPNM,
                                    });
                                });


                            }

                                lst.Add(new AuditDeptSummary
                            {
                                depcodeol = x.Depcodeol,
                                deptname = x.STName,
                                qty_total = x.QTY_TOTAL,
                                qty_wait = x.QTY_WAIT,
                                qty_checked = x.QTY_CHECKED,
                                qty_trn = x.QTY_TRN,
                                progress = x.PROGRESS,
                                sqno = x.sqno,
                                audit_no = x.audit_no,
                                company = x.Company,
                                DEPMST = x.DEPMST,
                                DEPNM = x.DEPNM,
                                AuditDepSummaryLST = lstDept
                                });
                        });
                    }

                    res.AuditSummaryLST = lst;

                    res._result._code = "200";
                    res._result._message = "";
                    res._result._status = "OK";

                }

               
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
