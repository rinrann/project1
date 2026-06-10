using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Models.DTOs
{
    public class PatientDto
    {
        public string   PatientReg      { get; set; }
        [Required] public string FirstName  { get; set; }
        [Required] public string LastName   { get; set; }
        [Required] public string Gender     { get; set; }
        public DateTime? DateOfBirth        { get; set; }
        public int?      Age                { get; set; }
        [Required] public string Mobile     { get; set; }
        public string   Email               { get; set; }
        public string   Address             { get; set; }
        public string   City                { get; set; }
        public string   State               { get; set; }
        public string   BloodGroup          { get; set; }
        public string   MaritalStatus       { get; set; }
        public string   ReferredBy          { get; set; }
        public DateTime RegistrationDate    { get; set; }
    }

    public class PatientListDto
    {
        public string PatientReg       { get; set; }
        public string FullName         { get; set; }
        public string Gender           { get; set; }
        public int?   Age              { get; set; }
        public string Mobile           { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
