using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClinicMS.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Pure JSON API — camelCase, no XML.
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver      = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling     = NullValueHandling.Ignore,
                    DateFormatString      = "yyyy-MM-dd",
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            });
        }
    }
}
