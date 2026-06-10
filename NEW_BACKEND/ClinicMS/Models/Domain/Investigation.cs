namespace ClinicMS.Models.Domain
{
    public class Investigation
    {
        public string TestId      { get; set; }
        public string CompCode    { get; set; }
        public string TestName    { get; set; }
        public string Category    { get; set; }
        public decimal Rate       { get; set; }
        public string Unit        { get; set; }
        public string NormalRange { get; set; }
        public int    Status      { get; set; }
    }
}
