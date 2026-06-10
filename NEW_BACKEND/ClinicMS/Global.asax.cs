using System.Web;
using System.Web.Http;

namespace ClinicMS
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                App_Start.CorsConfig.Register(config);
                App_Start.WebApiConfig.Register(config);
                App_Start.FilterConfig.Register(config);
            });
        }
    }
}
