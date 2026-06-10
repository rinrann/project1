using System.Collections.Generic;
using ClinicMS.Models.DTOs;

namespace ClinicMS.Services.Interfaces
{
    public interface IMasterService
    {
        IEnumerable<DoctorDto>       GetDoctors(string compCode);
        bool SaveDoctor(DoctorDto dto, string compCode, string userId);
        bool DeleteDoctor(string id, string compCode);

        IEnumerable<MedicineDto>     GetMedicines(string compCode, string q = null);
        bool SaveMedicine(MedicineDto dto, string compCode, string userId);
        bool DeleteMedicine(string id, string compCode);

        IEnumerable<InvestigationDto> GetInvestigations(string compCode);
        bool SaveInvestigation(InvestigationDto dto, string compCode, string userId);
        bool DeleteInvestigation(string id, string compCode);

        IEnumerable<SymptomDto>      GetSymptoms(string compCode);
        bool SaveSymptom(SymptomDto dto, string compCode, string userId);

        IEnumerable<DepartmentDto>   GetDepartments(string compCode);
        bool SaveDepartment(DepartmentDto dto, string compCode, string userId);

        IEnumerable<DesignationDto>  GetDesignations(string compCode);
        bool SaveDesignation(DesignationDto dto, string compCode, string userId);

        IEnumerable<SupplierDto>     GetSuppliers(string compCode);
        bool SaveSupplier(SupplierDto dto, string compCode, string userId);
        bool DeleteSupplier(string id, string compCode);

        IEnumerable<DropdownItemDto> GetDropdown(string entity, string compCode);
    }
}
