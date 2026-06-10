using System;
using System.Collections.Generic;
using System.Data;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Base;
using ClinicMS.Repositories.Interfaces;

namespace ClinicMS.Repositories
{
    public class MasterRepository : BaseRepository, IMasterRepository
    {
        // ── Doctors ─────────────────────────────────────────────────────────────

        public IEnumerable<DoctorDto> GetDoctors(string compCode)
        {
            var dt = QueryTable(
                "SELECT * FROM OPD_DoctorMaster WHERE compcode=@cc AND Status=1 ORDER BY DoctorName",
                cmd => cmd.Parameters.AddWithValue("@cc", compCode));
            var list = new List<DoctorDto>();
            foreach (DataRow r in dt.Rows)
                list.Add(new DoctorDto
                {
                    DoctorId      = S(r, "DoctorId"),
                    DoctorName    = S(r, "DoctorName"),
                    Speciality    = S(r, "Speciality"),
                    Qualification = S(r, "Qualification"),
                    Mobile        = S(r, "Mobile"),
                    Email         = S(r, "Email"),
                    Status        = I(r, "Status")
                });
            return list;
        }

        public DoctorDto GetDoctor(string id, string compCode)
        {
            var dt = QueryTable(
                "SELECT * FROM OPD_DoctorMaster WHERE DoctorId=@id AND compcode=@cc",
                cmd => { cmd.Parameters.AddWithValue("@id", id); cmd.Parameters.AddWithValue("@cc", compCode); });
            if (dt.Rows.Count == 0) return null;
            var r = dt.Rows[0];
            return new DoctorDto { DoctorId=S(r,"DoctorId"), DoctorName=S(r,"DoctorName"), Speciality=S(r,"Speciality"), Qualification=S(r,"Qualification"), Mobile=S(r,"Mobile"), Email=S(r,"Email"), Status=I(r,"Status") };
        }

        public bool SaveDoctor(DoctorDto dto, string compCode, string userId)
        {
            bool exists = !string.IsNullOrEmpty(dto.DoctorId) && GetDoctor(dto.DoctorId, compCode) != null;
            if (exists)
            {
                return Execute(
                    "UPDATE OPD_DoctorMaster SET DoctorName=@n,Speciality=@s,Qualification=@q,Mobile=@m,Email=@e WHERE DoctorId=@id AND compcode=@cc",
                    cmd => { cmd.Parameters.AddWithValue("@n",dto.DoctorName??""); cmd.Parameters.AddWithValue("@s",dto.Speciality??""); cmd.Parameters.AddWithValue("@q",dto.Qualification??""); cmd.Parameters.AddWithValue("@m",dto.Mobile??""); cmd.Parameters.AddWithValue("@e",dto.Email??""); cmd.Parameters.AddWithValue("@id",dto.DoctorId); cmd.Parameters.AddWithValue("@cc",compCode); }) > 0;
            }
            dto.DoctorId = dto.DoctorId ?? NewId("DR");
            return Execute(
                "INSERT INTO OPD_DoctorMaster(DoctorId,compcode,DoctorName,Speciality,Qualification,Mobile,Email,Status,CreatedBy,CreateDate) VALUES(@id,@cc,@n,@s,@q,@m,@e,1,@cb,GETDATE())",
                cmd => { cmd.Parameters.AddWithValue("@id",dto.DoctorId); cmd.Parameters.AddWithValue("@cc",compCode); cmd.Parameters.AddWithValue("@n",dto.DoctorName??""); cmd.Parameters.AddWithValue("@s",dto.Speciality??""); cmd.Parameters.AddWithValue("@q",dto.Qualification??""); cmd.Parameters.AddWithValue("@m",dto.Mobile??""); cmd.Parameters.AddWithValue("@e",dto.Email??""); cmd.Parameters.AddWithValue("@cb",userId); }) > 0;
        }

        public bool DeleteDoctor(string id, string compCode)
            => Execute("UPDATE OPD_DoctorMaster SET Status=0 WHERE DoctorId=@id AND compcode=@cc",
                cmd => { cmd.Parameters.AddWithValue("@id",id); cmd.Parameters.AddWithValue("@cc",compCode); }) > 0;

        // ── Medicines ────────────────────────────────────────────────────────────

        public IEnumerable<MedicineDto> GetMedicines(string compCode, string q = null)
        {
            string where = string.IsNullOrWhiteSpace(q) ? "" : " AND (LOWER(MedicineName) LIKE LOWER(@q) OR LOWER(GenericName) LIKE LOWER(@q))";
            var dt = QueryTable(
                $"SELECT * FROM MED_MedicineMaster WHERE compcode=@cc AND Status=1 {where} ORDER BY MedicineName",
                cmd => { cmd.Parameters.AddWithValue("@cc",compCode); if(!string.IsNullOrWhiteSpace(q)) cmd.Parameters.AddWithValue("@q","%"+q+"%"); });
            var list = new List<MedicineDto>();
            foreach (DataRow r in dt.Rows)
                list.Add(new MedicineDto { MedicineId=S(r,"MedicineId"), MedicineName=S(r,"MedicineName"), GenericName=S(r,"GenericName"), Category=S(r,"Category"), Unit=S(r,"Unit"), PurchaseRate=D(r,"PurchaseRate"), SaleRate=D(r,"SaleRate"), Stock=I(r,"Stock"), Status=I(r,"Status") });
            return list;
        }

        public bool SaveMedicine(MedicineDto dto, string compCode, string userId)
        {
            bool exists = !string.IsNullOrEmpty(dto.MedicineId) && Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) FROM MED_MedicineMaster WHERE MedicineId=@id AND compcode=@cc", cmd=>{cmd.Parameters.AddWithValue("@id",dto.MedicineId);cmd.Parameters.AddWithValue("@cc",compCode);})) > 0;
            if (exists)
                return Execute("UPDATE MED_MedicineMaster SET MedicineName=@n,GenericName=@g,Category=@cat,Unit=@u,PurchaseRate=@pr,SaleRate=@sr WHERE MedicineId=@id AND compcode=@cc",
                    cmd=>{cmd.Parameters.AddWithValue("@n",dto.MedicineName??"");cmd.Parameters.AddWithValue("@g",dto.GenericName??"");cmd.Parameters.AddWithValue("@cat",dto.Category??"");cmd.Parameters.AddWithValue("@u",dto.Unit??"");cmd.Parameters.AddWithValue("@pr",dto.PurchaseRate);cmd.Parameters.AddWithValue("@sr",dto.SaleRate);cmd.Parameters.AddWithValue("@id",dto.MedicineId);cmd.Parameters.AddWithValue("@cc",compCode);}) > 0;
            dto.MedicineId = dto.MedicineId ?? NewId("MED");
            return Execute("INSERT INTO MED_MedicineMaster(MedicineId,compcode,MedicineName,GenericName,Category,Unit,PurchaseRate,SaleRate,Stock,Status,CreatedBy,CreateDate) VALUES(@id,@cc,@n,@g,@cat,@u,@pr,@sr,0,1,@cb,GETDATE())",
                cmd=>{cmd.Parameters.AddWithValue("@id",dto.MedicineId);cmd.Parameters.AddWithValue("@cc",compCode);cmd.Parameters.AddWithValue("@n",dto.MedicineName??"");cmd.Parameters.AddWithValue("@g",dto.GenericName??"");cmd.Parameters.AddWithValue("@cat",dto.Category??"");cmd.Parameters.AddWithValue("@u",dto.Unit??"");cmd.Parameters.AddWithValue("@pr",dto.PurchaseRate);cmd.Parameters.AddWithValue("@sr",dto.SaleRate);cmd.Parameters.AddWithValue("@cb",userId);}) > 0;
        }

        public bool DeleteMedicine(string id, string compCode)
            => Execute("UPDATE MED_MedicineMaster SET Status=0 WHERE MedicineId=@id AND compcode=@cc",
                cmd=>{cmd.Parameters.AddWithValue("@id",id);cmd.Parameters.AddWithValue("@cc",compCode);}) > 0;

        // ── Investigations ───────────────────────────────────────────────────────

        public IEnumerable<InvestigationDto> GetInvestigations(string compCode)
        {
            var dt = QueryTable("SELECT * FROM PATH_TestMaster WHERE compcode=@cc AND Status=1 ORDER BY TestName",
                cmd=>cmd.Parameters.AddWithValue("@cc",compCode));
            var list = new List<InvestigationDto>();
            foreach (DataRow r in dt.Rows)
                list.Add(new InvestigationDto { TestId=S(r,"TestId"), TestName=S(r,"TestName"), Category=S(r,"Category"), Rate=D(r,"Rate"), Unit=S(r,"Unit"), NormalRange=S(r,"NormalRange"), Status=I(r,"Status") });
            return list;
        }

        public bool SaveInvestigation(InvestigationDto dto, string compCode, string userId)
        {
            bool exists = !string.IsNullOrEmpty(dto.TestId) && Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) FROM PATH_TestMaster WHERE TestId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@id",dto.TestId);cmd.Parameters.AddWithValue("@cc",compCode);}))>0;
            if(exists) return Execute("UPDATE PATH_TestMaster SET TestName=@n,Category=@c,Rate=@r,Unit=@u,NormalRange=@nr WHERE TestId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@n",dto.TestName??"");cmd.Parameters.AddWithValue("@c",dto.Category??"");cmd.Parameters.AddWithValue("@r",dto.Rate);cmd.Parameters.AddWithValue("@u",dto.Unit??"");cmd.Parameters.AddWithValue("@nr",dto.NormalRange??"");cmd.Parameters.AddWithValue("@id",dto.TestId);cmd.Parameters.AddWithValue("@cc",compCode);})>0;
            dto.TestId = dto.TestId ?? NewId("TST");
            return Execute("INSERT INTO PATH_TestMaster(TestId,compcode,TestName,Category,Rate,Unit,NormalRange,Status,CreatedBy,CreateDate) VALUES(@id,@cc,@n,@c,@r,@u,@nr,1,@cb,GETDATE())",cmd=>{cmd.Parameters.AddWithValue("@id",dto.TestId);cmd.Parameters.AddWithValue("@cc",compCode);cmd.Parameters.AddWithValue("@n",dto.TestName??"");cmd.Parameters.AddWithValue("@c",dto.Category??"");cmd.Parameters.AddWithValue("@r",dto.Rate);cmd.Parameters.AddWithValue("@u",dto.Unit??"");cmd.Parameters.AddWithValue("@nr",dto.NormalRange??"");cmd.Parameters.AddWithValue("@cb",userId);})>0;
        }

        public bool DeleteInvestigation(string id, string compCode)
            => Execute("UPDATE PATH_TestMaster SET Status=0 WHERE TestId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@id",id);cmd.Parameters.AddWithValue("@cc",compCode);})>0;

        // ── Symptoms ─────────────────────────────────────────────────────────────

        public IEnumerable<SymptomDto> GetSymptoms(string compCode)
        {
            var dt = QueryTable("SELECT * FROM OPD_ComplainMaster WHERE compcode=@cc AND Status=1 ORDER BY ComplainName",cmd=>cmd.Parameters.AddWithValue("@cc",compCode));
            var list=new List<SymptomDto>();
            foreach(DataRow r in dt.Rows) list.Add(new SymptomDto{SymptomId=S(r,"ComplainId"),SymptomName=S(r,"ComplainName"),Status=I(r,"Status")});
            return list;
        }

        public bool SaveSymptom(SymptomDto dto, string compCode, string userId)
        {
            bool exists = !string.IsNullOrEmpty(dto.SymptomId) && Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) FROM OPD_ComplainMaster WHERE ComplainId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@id",dto.SymptomId);cmd.Parameters.AddWithValue("@cc",compCode);}))>0;
            if(exists) return Execute("UPDATE OPD_ComplainMaster SET ComplainName=@n WHERE ComplainId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@n",dto.SymptomName??"");cmd.Parameters.AddWithValue("@id",dto.SymptomId);cmd.Parameters.AddWithValue("@cc",compCode);})>0;
            dto.SymptomId = dto.SymptomId ?? NewId("SYM");
            return Execute("INSERT INTO OPD_ComplainMaster(ComplainId,compcode,ComplainName,Status,CreatedBy,CreateDate) VALUES(@id,@cc,@n,1,@cb,GETDATE())",cmd=>{cmd.Parameters.AddWithValue("@id",dto.SymptomId);cmd.Parameters.AddWithValue("@cc",compCode);cmd.Parameters.AddWithValue("@n",dto.SymptomName??"");cmd.Parameters.AddWithValue("@cb",userId);})>0;
        }

        // ── Departments ──────────────────────────────────────────────────────────

        public IEnumerable<DepartmentDto> GetDepartments(string compCode)
        {
            var dt = QueryTable("SELECT * FROM GN_DepartmentMaster WHERE compcode=@cc AND Status=1 ORDER BY DeptName",cmd=>cmd.Parameters.AddWithValue("@cc",compCode));
            var list=new List<DepartmentDto>();
            foreach(DataRow r in dt.Rows) list.Add(new DepartmentDto{DeptId=S(r,"DeptId"),DeptName=S(r,"DeptName"),Status=I(r,"Status")});
            return list;
        }

        public bool SaveDepartment(DepartmentDto dto, string compCode, string userId)
        {
            bool exists = !string.IsNullOrEmpty(dto.DeptId) && Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) FROM GN_DepartmentMaster WHERE DeptId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@id",dto.DeptId);cmd.Parameters.AddWithValue("@cc",compCode);}))>0;
            if(exists) return Execute("UPDATE GN_DepartmentMaster SET DeptName=@n WHERE DeptId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@n",dto.DeptName??"");cmd.Parameters.AddWithValue("@id",dto.DeptId);cmd.Parameters.AddWithValue("@cc",compCode);})>0;
            dto.DeptId = dto.DeptId ?? NewId("DEPT");
            return Execute("INSERT INTO GN_DepartmentMaster(DeptId,compcode,DeptName,Status,CreatedBy,CreateDate) VALUES(@id,@cc,@n,1,@cb,GETDATE())",cmd=>{cmd.Parameters.AddWithValue("@id",dto.DeptId);cmd.Parameters.AddWithValue("@cc",compCode);cmd.Parameters.AddWithValue("@n",dto.DeptName??"");cmd.Parameters.AddWithValue("@cb",userId);})>0;
        }

        // ── Designations ─────────────────────────────────────────────────────────

        public IEnumerable<DesignationDto> GetDesignations(string compCode)
        {
            var dt = QueryTable("SELECT * FROM GN_DesignationMaster WHERE compcode=@cc AND Status=1 ORDER BY DesigName",cmd=>cmd.Parameters.AddWithValue("@cc",compCode));
            var list=new List<DesignationDto>();
            foreach(DataRow r in dt.Rows) list.Add(new DesignationDto{DesigId=S(r,"DesigId"),DesigName=S(r,"DesigName"),Status=I(r,"Status")});
            return list;
        }

        public bool SaveDesignation(DesignationDto dto, string compCode, string userId)
        {
            bool exists = !string.IsNullOrEmpty(dto.DesigId) && Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) FROM GN_DesignationMaster WHERE DesigId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@id",dto.DesigId);cmd.Parameters.AddWithValue("@cc",compCode);}))>0;
            if(exists) return Execute("UPDATE GN_DesignationMaster SET DesigName=@n WHERE DesigId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@n",dto.DesigName??"");cmd.Parameters.AddWithValue("@id",dto.DesigId);cmd.Parameters.AddWithValue("@cc",compCode);})>0;
            dto.DesigId = dto.DesigId ?? NewId("DSG");
            return Execute("INSERT INTO GN_DesignationMaster(DesigId,compcode,DesigName,Status,CreatedBy,CreateDate) VALUES(@id,@cc,@n,1,@cb,GETDATE())",cmd=>{cmd.Parameters.AddWithValue("@id",dto.DesigId);cmd.Parameters.AddWithValue("@cc",compCode);cmd.Parameters.AddWithValue("@n",dto.DesigName??"");cmd.Parameters.AddWithValue("@cb",userId);})>0;
        }

        // ── Suppliers ────────────────────────────────────────────────────────────

        public IEnumerable<SupplierDto> GetSuppliers(string compCode)
        {
            var dt = QueryTable("SELECT * FROM MED_SupplierMaster WHERE compcode=@cc AND Status=1 ORDER BY SupplierName",cmd=>cmd.Parameters.AddWithValue("@cc",compCode));
            var list=new List<SupplierDto>();
            foreach(DataRow r in dt.Rows) list.Add(new SupplierDto{SupplierId=S(r,"SupplierId"),SupplierName=S(r,"SupplierName"),Address=S(r,"Address"),Mobile=S(r,"Mobile"),Email=S(r,"Email"),Status=I(r,"Status")});
            return list;
        }

        public bool SaveSupplier(SupplierDto dto, string compCode, string userId)
        {
            bool exists = !string.IsNullOrEmpty(dto.SupplierId) && Convert.ToInt32(ExecuteScalar("SELECT COUNT(1) FROM MED_SupplierMaster WHERE SupplierId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@id",dto.SupplierId);cmd.Parameters.AddWithValue("@cc",compCode);}))>0;
            if(exists) return Execute("UPDATE MED_SupplierMaster SET SupplierName=@n,Address=@a,Mobile=@m,Email=@e WHERE SupplierId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@n",dto.SupplierName??"");cmd.Parameters.AddWithValue("@a",dto.Address??"");cmd.Parameters.AddWithValue("@m",dto.Mobile??"");cmd.Parameters.AddWithValue("@e",dto.Email??"");cmd.Parameters.AddWithValue("@id",dto.SupplierId);cmd.Parameters.AddWithValue("@cc",compCode);})>0;
            dto.SupplierId = dto.SupplierId ?? NewId("SUP");
            return Execute("INSERT INTO MED_SupplierMaster(SupplierId,compcode,SupplierName,Address,Mobile,Email,Status,CreatedBy,CreateDate) VALUES(@id,@cc,@n,@a,@m,@e,1,@cb,GETDATE())",cmd=>{cmd.Parameters.AddWithValue("@id",dto.SupplierId);cmd.Parameters.AddWithValue("@cc",compCode);cmd.Parameters.AddWithValue("@n",dto.SupplierName??"");cmd.Parameters.AddWithValue("@a",dto.Address??"");cmd.Parameters.AddWithValue("@m",dto.Mobile??"");cmd.Parameters.AddWithValue("@e",dto.Email??"");cmd.Parameters.AddWithValue("@cb",userId);})>0;
        }

        public bool DeleteSupplier(string id, string compCode)
            => Execute("UPDATE MED_SupplierMaster SET Status=0 WHERE SupplierId=@id AND compcode=@cc",cmd=>{cmd.Parameters.AddWithValue("@id",id);cmd.Parameters.AddWithValue("@cc",compCode);})>0;

        // ── Generic dropdown ─────────────────────────────────────────────────────

        public IEnumerable<DropdownItemDto> GetDropdown(string entity, string compCode)
        {
            var map = new Dictionary<string, (string table, string idCol, string nameCol)>
            {
                ["doctors"]      = ("OPD_DoctorMaster",       "DoctorId",    "DoctorName"),
                ["medicines"]    = ("MED_MedicineMaster",      "MedicineId",  "MedicineName"),
                ["investigations"]=("PATH_TestMaster",         "TestId",      "TestName"),
                ["symptoms"]     = ("OPD_ComplainMaster",      "ComplainId",  "ComplainName"),
                ["departments"]  = ("GN_DepartmentMaster",     "DeptId",      "DeptName"),
                ["designations"] = ("GN_DesignationMaster",    "DesigId",     "DesigName"),
                ["suppliers"]    = ("MED_SupplierMaster",      "SupplierId",  "SupplierName"),
                ["roles"]        = ("GN_UserRole",             "UserRoleID",  "UserRoleName"),
            };

            if (!map.TryGetValue(entity.ToLower(), out var m))
                return new List<DropdownItemDto>();

            var dt = QueryTable(
                $"SELECT {m.idCol} AS Id, {m.nameCol} AS Name FROM {m.table} WHERE compcode=@cc AND Status=1 ORDER BY {m.nameCol}",
                cmd => cmd.Parameters.AddWithValue("@cc", compCode));

            var list = new List<DropdownItemDto>();
            foreach (DataRow r in dt.Rows)
                list.Add(new DropdownItemDto { Value = S(r,"Id"), Label = S(r,"Name") });
            return list;
        }

        private static string NewId(string prefix)
            => $"{prefix}-{DateTime.Now:yyMMddHHmmss}-{Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper()}";
    }
}
