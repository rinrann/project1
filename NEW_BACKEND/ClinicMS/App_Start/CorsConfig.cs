using System.Configuration;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ClinicMS.App_Start
{
    public static class CorsConfig
    {
        public static void Register(HttpConfiguration config)
        {
            string origins = ConfigurationManager.AppSettings["Cors:AllowedOrigins"]
                             ?? "http://localhost:5500";

            var cors = new EnableCorsAttribute(origins, headers: "*", methods: "*")
            {
                SupportsCredentials = true
            };
            config.EnableCors(cors);
        }
    }
}
