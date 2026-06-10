using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClinicMS.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClinicMS.Helpers
{
    public static class ResponseHelper
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver  = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore
        };

        public static HttpResponseMessage Json<T>(ApiController ctrl, ApiResponse<T> response, HttpStatusCode code = HttpStatusCode.OK)
        {
            return ctrl.Request.CreateResponse(code, response);
        }
    }
}
