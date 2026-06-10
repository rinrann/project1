using System.Collections.Generic;
using ClinicMS.Models.Domain;
using ClinicMS.Models.DTOs;

namespace ClinicMS.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        PagedResult<PatientListDto> GetPatients(string compCode, string q, int page, int pageSize);
        Patient GetById(string patientReg, string compCode);
        string Create(Patient patient, string createdBy);
        bool Update(Patient patient);
        bool Delete(string patientReg, string compCode);
        IEnumerable<Patient> GetPatientHistory(string patientReg, string compCode);
        int CountToday(string compCode);
    }
}
