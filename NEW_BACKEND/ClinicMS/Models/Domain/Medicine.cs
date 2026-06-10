namespace ClinicMS.Models.Domain
{
    public class Medicine
    {
        public string MedicineId    { get; set; }
        public string CompCode      { get; set; }
        public string MedicineName  { get; set; }
        public string GenericName   { get; set; }
        public string Category      { get; set; }
        public string Unit          { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SaleRate     { get; set; }
        public int     Stock        { get; set; }
        public int     Status       { get; set; }
    }
}
