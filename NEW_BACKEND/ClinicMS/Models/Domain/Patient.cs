using System;

namespace ClinicMS.Models.Domain
{
    public class Patient
    {
        public string  PatientReg   { get; set; }
        public string  CompCode     { get; set; }
        public string  FirstName    { get; set; }
        public string  LastName     { get; set; }
        public string  FullName     { get; set; }
        public string  Gender       { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int?    Age          { get; set; }
        public string  Mobile       { get; set; }
        public string  Email        { get; set; }
        public string  Address      { get; set; }
        public string  City         { get; set; }
        public string  State        { get; set; }
        public string  BloodGroup   { get; set; }
        public string  MaritalStatus { get; set; }
        public string  ReferredBy   { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string  CreatedBy    { get; set; }
        public int     Status       { get; set; }
    }
}
