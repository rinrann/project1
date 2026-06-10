using ClinicMS.Models.DTOs;

namespace ClinicMS.Services.Interfaces
{
    public interface IPatientService
    {
        PagedResult<PatientListDto> GetPatients(string compCode, string q, int page, int pageSize);
        PatientDto GetById(string patientReg, string compCode);
        string     Create(PatientDto dto, string compCode, string createdBy);
        bool       Update(PatientDto dto, string compCode);
        bool       Delete(string patientReg, string compCode);
    }
}
