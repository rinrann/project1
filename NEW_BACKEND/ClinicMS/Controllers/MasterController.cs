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
    /// All Master-data CRUD endpoints.
    ///
    /// GET    /api/masters/dropdown/{entity}          — generic dropdown list
    /// GET    /api/masters/doctors
    /// POST   /api/masters/doctors
    /// DELETE /api/masters/doctors/{id}
    /// GET    /api/masters/medicines?q=
    /// POST   /api/masters/medicines
    /// DELETE /api/masters/medicines/{id}
    /// GET    /api/masters/investigations
    /// POST   /api/masters/investigations
    /// DELETE /api/masters/investigations/{id}
    /// GET    /api/masters/symptoms
    /// POST   /api/masters/symptoms
    /// GET    /api/masters/departments
    /// POST   /api/masters/departments
    /// GET    /api/masters/designations
    /// POST   /api/masters/designations
    /// GET    /api/masters/suppliers
    /// POST   /api/masters/suppliers
    /// DELETE /api/masters/suppliers/{id}
    /// </summary>
    [RoutePrefix("api/masters")]
    [JwtAuthFilter]
    public class MasterController : ApiController
    {
        private readonly IMasterService _svc;

        public MasterController()
        {
            _svc = new MasterService(new MasterRepository());
        }

        private string CompCode => (User?.Identity as ClaimsIdentity)?.FindFirst("compCode")?.Value;
        private string UserId   => (User?.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // ── Generic dropdown ─────────────────────────────────────────────────────
        [HttpGet, Route("dropdown/{entity}")]
        public IHttpActionResult Dropdown(string entity)
            => Ok(ApiResponse<object>.Ok(_svc.GetDropdown(entity, CompCode)));

        // ── Doctors ──────────────────────────────────────────────────────────────
        [HttpGet,   Route("doctors")]
        public IHttpActionResult GetDoctors() => Ok(ApiResponse<object>.Ok(_svc.GetDoctors(CompCode)));

        [HttpPost,  Route("doctors")]
        [RoleAuthFilter("Doctors", "edit")]
        public IHttpActionResult SaveDoctor([FromBody] DoctorDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            _svc.SaveDoctor(dto, CompCode, UserId);
            return Ok(ApiResponse.Ok("Saved."));
        }

        [HttpDelete, Route("doctors/{id}")]
        [RoleAuthFilter("Doctors", "full")]
        public IHttpActionResult DeleteDoctor(string id)
            => _svc.DeleteDoctor(id, CompCode) ? Ok(ApiResponse.Ok("Deleted.")) : (IHttpActionResult)NotFound();

        // ── Medicines ────────────────────────────────────────────────────────────
        [HttpGet,   Route("medicines")]
        public IHttpActionResult GetMedicines([FromUri] string q = null)
            => Ok(ApiResponse<object>.Ok(_svc.GetMedicines(CompCode, q)));

        [HttpPost,  Route("medicines")]
        [RoleAuthFilter("Pharmacy", "edit")]
        public IHttpActionResult SaveMedicine([FromBody] MedicineDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            _svc.SaveMedicine(dto, CompCode, UserId);
            return Ok(ApiResponse.Ok("Saved."));
        }

        [HttpDelete, Route("medicines/{id}")]
        [RoleAuthFilter("Pharmacy", "full")]
        public IHttpActionResult DeleteMedicine(string id)
            => _svc.DeleteMedicine(id, CompCode) ? Ok(ApiResponse.Ok("Deleted.")) : (IHttpActionResult)NotFound();

        // ── Investigations ───────────────────────────────────────────────────────
        [HttpGet,   Route("investigations")]
        public IHttpActionResult GetInvestigations()
            => Ok(ApiResponse<object>.Ok(_svc.GetInvestigations(CompCode)));

        [HttpPost,  Route("investigations")]
        [RoleAuthFilter("Investigation", "edit")]
        public IHttpActionResult SaveInvestigation([FromBody] InvestigationDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            _svc.SaveInvestigation(dto, CompCode, UserId);
            return Ok(ApiResponse.Ok("Saved."));
        }

        [HttpDelete, Route("investigations/{id}")]
        [RoleAuthFilter("Investigation", "full")]
        public IHttpActionResult DeleteInvestigation(string id)
            => _svc.DeleteInvestigation(id, CompCode) ? Ok(ApiResponse.Ok("Deleted.")) : (IHttpActionResult)NotFound();

        // ── Symptoms ─────────────────────────────────────────────────────────────
        [HttpGet,  Route("symptoms")]
        public IHttpActionResult GetSymptoms() => Ok(ApiResponse<object>.Ok(_svc.GetSymptoms(CompCode)));

        [HttpPost, Route("symptoms")]
        [RoleAuthFilter("Patients", "edit")]
        public IHttpActionResult SaveSymptom([FromBody] SymptomDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            _svc.SaveSymptom(dto, CompCode, UserId);
            return Ok(ApiResponse.Ok("Saved."));
        }

        // ── Departments ──────────────────────────────────────────────────────────
        [HttpGet,  Route("departments")]
        public IHttpActionResult GetDepartments() => Ok(ApiResponse<object>.Ok(_svc.GetDepartments(CompCode)));

        [HttpPost, Route("departments")]
        [RoleAuthFilter("Administration", "edit")]
        public IHttpActionResult SaveDepartment([FromBody] DepartmentDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            _svc.SaveDepartment(dto, CompCode, UserId);
            return Ok(ApiResponse.Ok("Saved."));
        }

        // ── Designations ─────────────────────────────────────────────────────────
        [HttpGet,  Route("designations")]
        public IHttpActionResult GetDesignations() => Ok(ApiResponse<object>.Ok(_svc.GetDesignations(CompCode)));

        [HttpPost, Route("designations")]
        [RoleAuthFilter("Administration", "edit")]
        public IHttpActionResult SaveDesignation([FromBody] DesignationDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            _svc.SaveDesignation(dto, CompCode, UserId);
            return Ok(ApiResponse.Ok("Saved."));
        }

        // ── Suppliers ────────────────────────────────────────────────────────────
        [HttpGet,   Route("suppliers")]
        public IHttpActionResult GetSuppliers() => Ok(ApiResponse<object>.Ok(_svc.GetSuppliers(CompCode)));

        [HttpPost,  Route("suppliers")]
        [RoleAuthFilter("Pharmacy", "edit")]
        public IHttpActionResult SaveSupplier([FromBody] SupplierDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            _svc.SaveSupplier(dto, CompCode, UserId);
            return Ok(ApiResponse.Ok("Saved."));
        }

        [HttpDelete, Route("suppliers/{id}")]
        [RoleAuthFilter("Pharmacy", "full")]
        public IHttpActionResult DeleteSupplier(string id)
            => _svc.DeleteSupplier(id, CompCode) ? Ok(ApiResponse.Ok("Deleted.")) : (IHttpActionResult)NotFound();
    }
}
