using System;

namespace ClinicMS.Models.Domain
{
    public class Employee
    {
        public string   EmpId          { get; set; }
        public string   CompCode       { get; set; }
        public string   EmpName        { get; set; }
        public string   DesignationId  { get; set; }
        public string   DepartmentId   { get; set; }
        public string   Mobile         { get; set; }
        public string   Email          { get; set; }
        public DateTime? JoiningDate   { get; set; }
        public int      Status         { get; set; }

        // Navigation
        public string DesignationName  { get; set; }
        public string DepartmentName   { get; set; }
    }
}
