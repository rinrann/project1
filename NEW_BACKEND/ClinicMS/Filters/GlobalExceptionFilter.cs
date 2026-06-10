using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace ClinicMS.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext ctx)
        {
            var ex = ctx.Exception;

#if DEBUG
            var detail = ex.ToString();
#else
            var detail = "An unexpected error occurred. Please try again.";
#endif

            ctx.Response = ctx.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new { success = false, message = detail }
            );
        }
    }
}
