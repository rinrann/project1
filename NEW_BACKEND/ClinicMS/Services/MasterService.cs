using System.Collections.Generic;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Interfaces;
using ClinicMS.Services.Interfaces;

namespace ClinicMS.Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository _repo;
        public MasterService(IMasterRepository repo) { _repo = repo; }

        public IEnumerable<DoctorDto>       GetDoctors(string cc)        => _repo.GetDoctors(cc);
        public bool SaveDoctor(DoctorDto d, string cc, string uid)        => _repo.SaveDoctor(d, cc, uid);
        public bool DeleteDoctor(string id, string cc)                    => _repo.DeleteDoctor(id, cc);

        public IEnumerable<MedicineDto>     GetMedicines(string cc, string q=null) => _repo.GetMedicines(cc,q);
        public bool SaveMedicine(MedicineDto d, string cc, string uid)    => _repo.SaveMedicine(d, cc, uid);
        public bool DeleteMedicine(string id, string cc)                  => _repo.DeleteMedicine(id, cc);

        public IEnumerable<InvestigationDto> GetInvestigations(string cc) => _repo.GetInvestigations(cc);
        public bool SaveInvestigation(InvestigationDto d,string cc,string uid) => _repo.SaveInvestigation(d,cc,uid);
        public bool DeleteInvestigation(string id, string cc)             => _repo.DeleteInvestigation(id, cc);

        public IEnumerable<SymptomDto>      GetSymptoms(string cc)        => _repo.GetSymptoms(cc);
        public bool SaveSymptom(SymptomDto d, string cc, string uid)      => _repo.SaveSymptom(d, cc, uid);

        public IEnumerable<DepartmentDto>   GetDepartments(string cc)     => _repo.GetDepartments(cc);
        public bool SaveDepartment(DepartmentDto d, string cc, string uid)=> _repo.SaveDepartment(d, cc, uid);

        public IEnumerable<DesignationDto>  GetDesignations(string cc)    => _repo.GetDesignations(cc);
        public bool SaveDesignation(DesignationDto d, string cc, string uid) => _repo.SaveDesignation(d, cc, uid);

        public IEnumerable<SupplierDto>     GetSuppliers(string cc)       => _repo.GetSuppliers(cc);
        public bool SaveSupplier(SupplierDto d, string cc, string uid)    => _repo.SaveSupplier(d, cc, uid);
        public bool DeleteSupplier(string id, string cc)                  => _repo.DeleteSupplier(id, cc);

        public IEnumerable<DropdownItemDto> GetDropdown(string e, string cc) => _repo.GetDropdown(e, cc);
    }
}
