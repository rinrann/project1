using ClinicMS.Models.Domain;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Interfaces;
using ClinicMS.Services.Interfaces;

namespace ClinicMS.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo) { _repo = repo; }

        public PagedResult<PatientListDto> GetPatients(string compCode, string q, int page, int pageSize)
            => _repo.GetPatients(compCode, q, page, pageSize);

        public PatientDto GetById(string patientReg, string compCode)
        {
            var p = _repo.GetById(patientReg, compCode);
            if (p == null) return null;
            return ToDto(p);
        }

        public string Create(PatientDto dto, string compCode, string createdBy)
        {
            var p = ToDomain(dto, compCode);
            return _repo.Create(p, createdBy);
        }

        public bool Update(PatientDto dto, string compCode)
        {
            var p = ToDomain(dto, compCode);
            return _repo.Update(p);
        }

        public bool Delete(string patientReg, string compCode)
            => _repo.Delete(patientReg, compCode);

        private static Patient ToDomain(PatientDto d, string compCode) => new Patient
        {
            PatientReg    = d.PatientReg,
            CompCode      = compCode,
            FirstName     = d.FirstName,
            LastName      = d.LastName,
            Gender        = d.Gender,
            DateOfBirth   = d.DateOfBirth,
            Age           = d.Age,
            Mobile        = d.Mobile,
            Email         = d.Email,
            Address       = d.Address,
            City          = d.City,
            State         = d.State,
            BloodGroup    = d.BloodGroup,
            MaritalStatus = d.MaritalStatus,
            ReferredBy    = d.ReferredBy
        };

        private static PatientDto ToDto(Patient p) => new PatientDto
        {
            PatientReg       = p.PatientReg,
            FirstName        = p.FirstName,
            LastName         = p.LastName,
            Gender           = p.Gender,
            DateOfBirth      = p.DateOfBirth,
            Age              = p.Age,
            Mobile           = p.Mobile,
            Email            = p.Email,
            Address          = p.Address,
            City             = p.City,
            State            = p.State,
            BloodGroup       = p.BloodGroup,
            MaritalStatus    = p.MaritalStatus,
            ReferredBy       = p.ReferredBy,
            RegistrationDate = p.RegistrationDate
        };
    }
}
