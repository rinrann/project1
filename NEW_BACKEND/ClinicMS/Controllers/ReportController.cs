using System;
using System.Data;
using System.Security.Claims;
using System.Web.Http;
using ClinicMS.Filters;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Base;

namespace ClinicMS.Controllers
{
    /// <summary>
    /// Read-only report endpoints consumed by hms-pages-reports.js.
    ///
    /// GET /api/reports/invoices?from=&to=&q=&page=&pageSize=
    /// GET /api/reports/daily-collection?from=&to=
    /// GET /api/reports/sales-register?from=&to=
    /// GET /api/reports/stock
    /// </summary>
    [RoutePrefix("api/reports")]
    [JwtAuthFilter]
    [RoleAuthFilter("Reports", "view")]
    public class ReportController : ApiController
    {
        private readonly ReportQueryHelper _db = new ReportQueryHelper();

        private string CompCode => (User?.Identity as ClaimsIdentity)?.FindFirst("compCode")?.Value;

        [HttpGet, Route("invoices")]
        public IHttpActionResult Invoices(
            [FromUri] DateTime? from = null, [FromUri] DateTime? to = null,
            [FromUri] string q = null, [FromUri] int page = 1, [FromUri] int pageSize = 50)
        {
            var dt = _db.Invoices(CompCode, from ?? DateTime.Today.AddDays(-29), to ?? DateTime.Today, q, page, pageSize);
            return Ok(ApiResponse<object>.Ok(dt));
        }

        [HttpGet, Route("daily-collection")]
        public IHttpActionResult DailyCollection(
            [FromUri] DateTime? from = null, [FromUri] DateTime? to = null)
        {
            var dt = _db.DailyCollection(CompCode, from ?? DateTime.Today.AddDays(-29), to ?? DateTime.Today);
            return Ok(ApiResponse<object>.Ok(dt));
        }

        [HttpGet, Route("sales-register")]
        public IHttpActionResult SalesRegister(
            [FromUri] DateTime? from = null, [FromUri] DateTime? to = null)
        {
            var dt = _db.SalesRegister(CompCode, from ?? DateTime.Today.AddDays(-29), to ?? DateTime.Today);
            return Ok(ApiResponse<object>.Ok(dt));
        }

        [HttpGet, Route("stock")]
        public IHttpActionResult Stock()
        {
            var dt = _db.StockSummary(CompCode);
            return Ok(ApiResponse<object>.Ok(dt));
        }
    }

    // Lightweight inline query helper (no extra file needed for this thin layer).
    internal class ReportQueryHelper : BaseRepository
    {
        public object Invoices(string cc, DateTime from, DateTime to, string q, int page, int ps)
        {
            string where = string.IsNullOrWhiteSpace(q) ? "" : " AND (LOWER(BillNo) LIKE LOWER(@q) OR LOWER(PatientName) LIKE LOWER(@q))";
            int offset = (page - 1) * ps;
            var dt = QueryTable(
                $@"SELECT BillNo, PatientReg, PatientName,
                          CONVERT(DATE, BillDate) AS BillDate,
                          BillAmount, PaidAmount, (BillAmount - PaidAmount) AS DueAmount, Status
                   FROM   OPD_BillMaster
                   WHERE  compcode=@cc AND CONVERT(DATE,BillDate) BETWEEN @from AND @to {where}
                   ORDER  BY BillDate DESC
                   OFFSET @off ROWS FETCH NEXT @ps ROWS ONLY",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@cc",   cc);
                    cmd.Parameters.AddWithValue("@from", from.Date);
                    cmd.Parameters.AddWithValue("@to",   to.Date);
                    cmd.Parameters.AddWithValue("@off",  offset);
                    cmd.Parameters.AddWithValue("@ps",   ps);
                    if (!string.IsNullOrWhiteSpace(q)) cmd.Parameters.AddWithValue("@q", "%" + q + "%");
                });
            return ToList(dt);
        }

        public object DailyCollection(string cc, DateTime from, DateTime to)
        {
            var dt = QueryTable(
                @"SELECT CONVERT(DATE, BillDate) AS Date,
                         COUNT(1) AS TotalBills,
                         ISNULL(SUM(BillAmount),0) AS TotalAmount,
                         ISNULL(SUM(PaidAmount),0) AS Collected
                  FROM   OPD_BillMaster
                  WHERE  compcode=@cc AND Status=1
                    AND  CONVERT(DATE,BillDate) BETWEEN @from AND @to
                  GROUP  BY CONVERT(DATE,BillDate)
                  ORDER  BY CONVERT(DATE,BillDate)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@cc",   cc);
                    cmd.Parameters.AddWithValue("@from", from.Date);
                    cmd.Parameters.AddWithValue("@to",   to.Date);
                });
            return ToList(dt);
        }

        public object SalesRegister(string cc, DateTime from, DateTime to)
        {
            var dt = QueryTable(
                @"SELECT s.SaleNo, s.PatientName,
                         CONVERT(DATE, s.SaleDate) AS SaleDate,
                         s.TotalAmount, s.DiscountAmount, s.NetAmount
                  FROM   MED_SalesMaster s
                  WHERE  s.compcode=@cc AND CONVERT(DATE,s.SaleDate) BETWEEN @from AND @to
                  ORDER  BY s.SaleDate DESC",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@cc",   cc);
                    cmd.Parameters.AddWithValue("@from", from.Date);
                    cmd.Parameters.AddWithValue("@to",   to.Date);
                });
            return ToList(dt);
        }

        public object StockSummary(string cc)
        {
            var dt = QueryTable(
                @"SELECT m.MedicineId, m.MedicineName, m.Category, m.Unit,
                         m.Stock, m.SaleRate,
                         (m.Stock * m.SaleRate) AS StockValue
                  FROM   MED_MedicineMaster m
                  WHERE  m.compcode=@cc AND m.Status=1
                  ORDER  BY m.MedicineName",
                cmd => cmd.Parameters.AddWithValue("@cc", cc));
            return ToList(dt);
        }

        private static System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>> ToList(DataTable dt)
        {
            var rows = new System.Collections.Generic.List<System.Collections.Generic.Dictionary<string, object>>();
            foreach (DataRow r in dt.Rows)
            {
                var dict = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn c in dt.Columns)
                    dict[c.ColumnName] = r[c] == DBNull.Value ? null : r[c];
                rows.Add(dict);
            }
            return rows;
        }
    }
}
