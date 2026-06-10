using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using ClinicMS.Helpers;

namespace ClinicMS.Filters
{
    /// <summary>
    /// Validates the Bearer JWT on every request decorated with [JwtAuth].
    /// Populate Thread.CurrentPrincipal so ClaimsPrincipal.Current works downstream.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class JwtAuthFilter : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken ct)
        {
            var request = context.Request;
            var auth    = request.Headers.Authorization;

            if (auth == null || !string.Equals(auth.Scheme, "Bearer", StringComparison.OrdinalIgnoreCase))
            {
                context.ErrorResult = new UnauthorizedResult("Missing or invalid Authorization header.", request);
                return Task.CompletedTask;
            }

            try
            {
                var principal = JwtHelper.ValidateToken(auth.Parameter);
                context.Principal = principal;
                Thread.CurrentPrincipal = principal;
            }
            catch
            {
                context.ErrorResult = new UnauthorizedResult("Token is invalid or expired.", request);
            }

            return Task.CompletedTask;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken ct)
            => Task.CompletedTask;

        private class UnauthorizedResult : System.Web.Http.IHttpActionResult
        {
            private readonly string _message;
            private readonly HttpRequestMessage _request;

            public UnauthorizedResult(string message, HttpRequestMessage request)
            { _message = message; _request = request; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken ct)
            {
                var response = _request.CreateResponse(HttpStatusCode.Unauthorized,
                    new { success = false, message = _message });
                return Task.FromResult(response);
            }
        }
    }
}
