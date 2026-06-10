using System;
using System.Net;
using System.Net.Http;
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
    /// POST /api/auth/login
    /// POST /api/auth/logout
    /// GET  /api/auth/me
    /// GET  /api/auth/companies
    /// </summary>
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthService _svc;

        public AuthController()
        {
            _svc = new AuthService(new AuthRepository());
        }

        // ── POST /api/auth/login ─────────────────────────────────────────────────
        [HttpPost, Route("login"), AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string ip = Request.GetClientIpAddress();
                var result = _svc.Login(request, ip);
                return Ok(ApiResponse<LoginResponse>.Ok(result, "Login successful."));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Content(HttpStatusCode.Unauthorized,
                    ApiResponse<object>.Fail(ex.Message));
            }
        }

        // ── POST /api/auth/logout ────────────────────────────────────────────────
        [HttpPost, Route("logout"), JwtAuthFilter]
        public IHttpActionResult Logout()
        {
            var identity = User?.Identity as ClaimsIdentity;
            string userId   = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string compCode = identity?.FindFirst("compCode")?.Value;

            if (!string.IsNullOrEmpty(userId))
                _svc.Logout(userId, compCode);

            return Ok(ApiResponse.Ok("Logged out."));
        }

        // ── GET /api/auth/me ─────────────────────────────────────────────────────
        [HttpGet, Route("me"), JwtAuthFilter]
        public IHttpActionResult Me()
        {
            var identity  = User?.Identity as ClaimsIdentity;
            string userId   = identity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string compCode = identity?.FindFirst("compCode")?.Value;

            var session = _svc.GetCurrentUser(userId, compCode);
            if (session == null)
                return Content(HttpStatusCode.Unauthorized, ApiResponse<object>.Fail("Session not found."));

            return Ok(ApiResponse<UserSessionDto>.Ok(session));
        }

        // ── GET /api/auth/companies ──────────────────────────────────────────────
        [HttpGet, Route("companies"), AllowAnonymous]
        public IHttpActionResult Companies()
        {
            var list = _svc.GetCompanies();
            return Ok(ApiResponse<object>.Ok(list));
        }
    }

    internal static class HttpRequestMessageExtensions
    {
        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            if (request.Properties.TryGetValue("MS_HttpContext", out var ctx))
            {
                dynamic httpCtx = ctx;
                return httpCtx?.Request?.UserHostAddress ?? "unknown";
            }
            return "unknown";
        }
    }
}
