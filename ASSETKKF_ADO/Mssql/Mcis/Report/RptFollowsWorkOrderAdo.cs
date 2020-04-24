using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ASSETKKF_ADO.Mssql.Mcis.Report
{
    public class RptFollowsWorkOrderAdo : Base
    {
        private static RptFollowsWorkOrderAdo instant;

        public static RptFollowsWorkOrderAdo GetInstant()
        {
            if (instant == null) instant = new RptFollowsWorkOrderAdo();
            return instant;
        }

        private string conectStr { get; set; }

        private RptFollowsWorkOrderAdo() { }

        public List<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderHeadRes> GetHead(string WorkOrderId="" )
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkOrderId", WorkOrderId);

            string cmd = "EXECUTE  [mcis].[dbo].[RptFollowsWorkOrderHead] @WorkOrderId";

            var res = Query<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderHeadRes>(cmd, param).ToList();
           
            return res;

        }

        public List<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderDetailRes> GetDetail(string WorkOrderId = ""  )
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkOrderId", WorkOrderId);

            string cmd = "EXECUTE  [mcis].[dbo].[RptFollowsWorkOrderDetail] @WorkOrderId";

            var res = Query<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderDetailRes>(cmd, param).ToList();

            return res;

        }

        public List<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderLastRes> GetLast(string WorkOrderId = "" )
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@WorkOrderId", WorkOrderId);

            string cmd = "EXECUTE  [mcis].[dbo].[RptFollowsWorkOrderLast] @WorkOrderId";

            var res = Query<ASSETKKF_MODEL.Response.Report.RptFollowsWorkOrderLastRes>(cmd, param).ToList();

            return res;

        }


    }
}
