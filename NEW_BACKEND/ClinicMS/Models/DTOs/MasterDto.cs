using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Models.DTOs
{
    // Generic key-value used for dropdown endpoints.
    public class DropdownItemDto
    {
        public string Value { get; set; }
        public string Label { get; set; }
    }

    public class DoctorDto
    {
        public string DoctorId      { get; set; }
        [Required] public string DoctorName { get; set; }
        public string Speciality    { get; set; }
        public string Qualification { get; set; }
        public string Mobile        { get; set; }
        public string Email         { get; set; }
        public int    Status        { get; set; }
    }

    public class MedicineDto
    {
        public string  MedicineId   { get; set; }
        [Required] public string MedicineName { get; set; }
        public string  GenericName  { get; set; }
        public string  Category     { get; set; }
        public string  Unit         { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SaleRate     { get; set; }
        public int     Stock        { get; set; }
        public int     Status       { get; set; }
    }

    public class InvestigationDto
    {
        public string  TestId      { get; set; }
        [Required] public string TestName  { get; set; }
        public string  Category    { get; set; }
        public decimal Rate        { get; set; }
        public string  Unit        { get; set; }
        public string  NormalRange { get; set; }
        public int     Status      { get; set; }
    }

    public class SymptomDto
    {
        public string SymptomId   { get; set; }
        [Required] public string SymptomName { get; set; }
        public int    Status      { get; set; }
    }

    public class DepartmentDto
    {
        public string DeptId    { get; set; }
        [Required] public string DeptName { get; set; }
        public int    Status    { get; set; }
    }

    public class DesignationDto
    {
        public string DesigId    { get; set; }
        [Required] public string DesigName { get; set; }
        public int    Status     { get; set; }
    }

    public class SupplierDto
    {
        public string SupplierId   { get; set; }
        [Required] public string SupplierName { get; set; }
        public string Address      { get; set; }
        public string Mobile       { get; set; }
        public string Email        { get; set; }
        public int    Status       { get; set; }
    }

    public class CompanyDto
    {
        public string CompCode  { get; set; }
        public string YearCode  { get; set; }
        public string CoName    { get; set; }
        // Composite display used by login dropdown
        public string Display   { get; set; }
    }
}
