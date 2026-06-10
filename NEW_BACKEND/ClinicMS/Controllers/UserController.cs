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
    /// GET    /api/users
    /// GET    /api/users/{id}
    /// POST   /api/users
    /// PUT    /api/users/{id}
    /// DELETE /api/users/{id}
    /// POST   /api/users/{id}/change-password
    /// GET    /api/users/roles
    /// </summary>
    [RoutePrefix("api/users")]
    [JwtAuthFilter]
    [RoleAuthFilter("Administration", "view")]
    public class UserController : ApiController
    {
        private readonly IUserService _svc;

        public UserController()
        {
            _svc = new UserService(new UserRepository());
        }

        private string CompCode => (User?.Identity as ClaimsIdentity)?.FindFirst("compCode")?.Value;
        private string UserId   => (User?.Identity as ClaimsIdentity)?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        [HttpGet, Route("")]
        public IHttpActionResult GetAll()
            => Ok(ApiResponse<object>.Ok(_svc.GetAll(CompCode)));

        [HttpGet, Route("roles")]
        [AllowAnonymous]
        public IHttpActionResult GetRoles()
            => Ok(ApiResponse<object>.Ok(_svc.GetRoles(CompCode)));

        [HttpGet, Route("{id}")]
        public IHttpActionResult GetById(string id)
        {
            var u = _svc.GetById(id, CompCode);
            return u == null ? (IHttpActionResult)NotFound() : Ok(ApiResponse<UserDto>.Ok(u));
        }

        [HttpPost, Route("")]
        [RoleAuthFilter("Administration", "full")]
        public IHttpActionResult Create([FromBody] CreateUserRequest req)
        {
            if (req == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            bool ok = _svc.Create(req, CompCode, UserId);

            if (ok)
                return Ok(ApiResponse.Ok("User created."));

            return BadRequest("Failed to create user.");
        }

        [HttpPut, Route("{id}")]
        [RoleAuthFilter("Administration", "edit")]
        public IHttpActionResult Update(string id, [FromBody] UserDto dto)
        {
            if (dto == null || !ModelState.IsValid) return BadRequest(ModelState);
            dto.UserId = id;
            bool ok = _svc.Update(dto, CompCode);
            return ok ? Ok(ApiResponse.Ok("User updated.")) : (IHttpActionResult)NotFound();
        }

        [HttpDelete, Route("{id}")]
        [RoleAuthFilter("Administration", "full")]
        public IHttpActionResult Delete(string id)
        {
            bool ok = _svc.Delete(id, CompCode);
            return ok ? Ok(ApiResponse.Ok("User deactivated.")) : (IHttpActionResult)NotFound();
        }

        [HttpPost, Route("{id}/change-password"), JwtAuthFilter]
        public IHttpActionResult ChangePassword(string id, [FromBody] ChangePasswordRequest req)
        {
            if (req == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            bool ok = _svc.ChangePassword(id, CompCode, req);

            if (ok)
                return Ok(ApiResponse.Ok("Password changed."));

            return BadRequest("Could not change password.");
        }
    }
}
