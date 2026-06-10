namespace ClinicMS.Models.Domain
{
    public class Doctor
    {
        public string DoctorId    { get; set; }
        public string CompCode    { get; set; }
        public string DoctorName  { get; set; }
        public string Speciality  { get; set; }
        public string Qualification { get; set; }
        public string Mobile      { get; set; }
        public string Email       { get; set; }
        public int    Status      { get; set; }
    }
}
