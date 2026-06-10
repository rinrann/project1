using System.Net;
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
    /// GET    /api/patients?q=&page=&pageSize=
    /// GET    /api/patients/{reg}
    /// POST   /api/patients
    /// PUT    /api/patients/{reg}
    /// DELETE /api/patients/{reg}
    /// </summary>
    [RoutePrefix("api/patients")]
    [JwtAuthFilter]
    [RoleAuthFilter("Patients", "view")]
    public class PatientController : ApiController
    {
        private readonly IPatientService _svc;

        public PatientController()
        {
            _svc = new PatientService(new PatientRepository());
        }

        private string CompCode => (User?.Identity as ClaimsIdentity)?.FindFirst("compCode")?.Value;
        private string UserId   => (User?.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet, Route("")]
        public IHttpActionResult GetAll([FromUri] string q = null, [FromUri] int page = 1, [FromUri] int pageSize = 50)
        {
            var result = _svc.GetPatients(CompCode, q, page, pageSize);
            return Ok(ApiResponse<PagedResult<PatientListDto>>.Ok(result));
        }

        [HttpGet, Route("{reg}")]
        public IHttpActionResult GetById(string reg)
        {
            var p = _svc.GetById(reg, CompCode);
            if (p == null) return NotFound();
            return Ok(ApiResponse<PatientDto>.Ok(p));
        }

        [HttpPost, Route("")]
        [RoleAuthFilter("Patients", "edit")]
        public IHttpActionResult Create([FromBody] PatientDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            string reg = _svc.Create(dto, CompCode, UserId);
            return Content(HttpStatusCode.Created, ApiResponse<object>.Ok(new { patientReg = reg }, "Patient registered."));
        }

        [HttpPut, Route("{reg}")]
        [RoleAuthFilter("Patients", "edit")]
        public IHttpActionResult Update(string reg, [FromBody] PatientDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            dto.PatientReg = reg;
            bool ok = _svc.Update(dto, CompCode);
            return ok ? Ok(ApiResponse.Ok("Patient updated.")) : (IHttpActionResult)NotFound();
        }

        [HttpDelete, Route("{reg}")]
        [RoleAuthFilter("Patients", "full")]
        public IHttpActionResult Delete(string reg)
        {
            bool ok = _svc.Delete(reg, CompCode);
            return ok ? Ok(ApiResponse.Ok("Patient deleted.")) : (IHttpActionResult)NotFound();
        }
    }
}
