using System.Collections.Generic;
using ClinicMS.Models.DTOs;

namespace ClinicMS.Repositories.Interfaces
{
    public interface IMasterRepository
    {
        // Doctors
        IEnumerable<DoctorDto>      GetDoctors(string compCode);
        DoctorDto                   GetDoctor(string id, string compCode);
        bool SaveDoctor(DoctorDto dto, string compCode, string userId);
        bool DeleteDoctor(string id, string compCode);

        // Medicines
        IEnumerable<MedicineDto>    GetMedicines(string compCode, string q = null);
        bool SaveMedicine(MedicineDto dto, string compCode, string userId);
        bool DeleteMedicine(string id, string compCode);

        // Investigations (tests)
        IEnumerable<InvestigationDto> GetInvestigations(string compCode);
        bool SaveInvestigation(InvestigationDto dto, string compCode, string userId);
        bool DeleteInvestigation(string id, string compCode);

        // Symptoms
        IEnumerable<SymptomDto>     GetSymptoms(string compCode);
        bool SaveSymptom(SymptomDto dto, string compCode, string userId);

        // Departments
        IEnumerable<DepartmentDto>  GetDepartments(string compCode);
        bool SaveDepartment(DepartmentDto dto, string compCode, string userId);

        // Designations
        IEnumerable<DesignationDto> GetDesignations(string compCode);
        bool SaveDesignation(DesignationDto dto, string compCode, string userId);

        // Suppliers
        IEnumerable<SupplierDto>    GetSuppliers(string compCode);
        bool SaveSupplier(SupplierDto dto, string compCode, string userId);
        bool DeleteSupplier(string id, string compCode);

        // Generic dropdown
        IEnumerable<DropdownItemDto> GetDropdown(string entity, string compCode);
    }
}
