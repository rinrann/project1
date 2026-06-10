using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using ClinicMS.Helpers;

namespace ClinicMS.Filters
{
    /// <summary>
    /// Enforces module-level permissions after JWT authentication.
    /// Usage: [RoleAuth("Pharmacy", "edit")]
    /// </summary>
    public class RoleAuthFilter : AuthorizeAttribute
    {
        private readonly string _module;
        private readonly string _action;  // view | edit | full

        public RoleAuthFilter(string module, string action = "view")
        {
            _module = module;
            _action = action;
        }

        protected override bool IsAuthorized(HttpActionContext ctx)
        {
            var identity = ctx.RequestContext.Principal?.Identity as ClaimsIdentity;
            if (identity == null || !identity.IsAuthenticated) return false;

            var role = identity.FindFirst(ClaimTypes.Role)?.Value ?? "";
            return RoleHelper.CanDo(role, _module, _action);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext ctx)
        {
            ctx.Response = ctx.Request.CreateResponse(HttpStatusCode.Forbidden,
                new { success = false, message = "You do not have permission to perform this action." });
        }
    }
}
