using System;
using System.Security.Claims;
using System.Web.Http;
using ClinicMS.Filters;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories;
using ClinicMS.Services;
using ClinicMS.Services.Interfaces;

namespace ClinicMS.Controllers
{
    /// <summary>
    /// GET /api/dashboard/metrics?preset=thisMonth
    /// GET /api/dashboard/metrics?from=2026-01-01&amp;to=2026-01-31
    /// </summary>
    [RoutePrefix("api/dashboard")]
    [JwtAuthFilter]
    [RoleAuthFilter("Dashboard", "view")]
    public class DashboardController : ApiController
    {
        private readonly IDashboardService _svc;

        public DashboardController()
        {
            _svc = new DashboardService(new DashboardRepository());
        }

        private string CompCode => (User?.Identity as ClaimsIdentity)?.FindFirst("compCode")?.Value;

        [HttpGet, Route("metrics")]
        public IHttpActionResult GetMetrics(
            [FromUri] string preset = "thisMonth",
            [FromUri] DateTime? from = null,
            [FromUri] DateTime? to   = null)
        {
            var metrics = _svc.GetMetrics(CompCode, preset, from, to);
            return Ok(ApiResponse<DashboardMetricsDto>.Ok(metrics));
        }
    }
}
