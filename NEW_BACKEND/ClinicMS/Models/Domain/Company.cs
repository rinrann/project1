namespace ClinicMS.Models.Domain
{
    /// <summary>Maps to the <c>parms</c> table — one row per clinic + financial year.</summary>
    public class Company
    {
        public string CompCode  { get; set; }
        public string YearCode  { get; set; }
        public string CoName    { get; set; }
        public string Address   { get; set; }
        public string Phone     { get; set; }
        public string Email     { get; set; }
    }
}
