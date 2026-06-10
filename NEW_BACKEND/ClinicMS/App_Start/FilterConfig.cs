using System.Web.Http;
using ClinicMS.Filters;

namespace ClinicMS.App_Start
{
    public static class FilterConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new GlobalExceptionFilter());
        }
    }
}
