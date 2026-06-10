using System.Collections.Generic;

namespace ClinicMS.Models.DTOs
{
    public class DashboardMetricsDto
    {
        // Financial KPIs
        public decimal Revenue           { get; set; }
        public decimal Profit            { get; set; }
        public decimal ProfitMargin      { get; set; }
        public decimal Loss              { get; set; }
        public decimal LossPct           { get; set; }
        public decimal RevenueGrowth     { get; set; }
        public decimal ProfitGrowth      { get; set; }

        // Patient KPIs
        public int TotalPatients          { get; set; }
        public int NewRegistrations       { get; set; }
        public int TotalAppointments      { get; set; }
        public int PendingAppointments    { get; set; }
        public int TodaysCheckIns         { get; set; }

        // Pharmacy
        public int LowStockAlerts         { get; set; }
        public int SalesTxns              { get; set; }

        // Averages
        public decimal AvgRevenuePerPatient     { get; set; }
        public decimal AvgRevenuePerAppointment { get; set; }

        // Trend arrays (7/30 data-points) for charts
        public List<TrendPoint> DailyRevenue    { get; set; }
        public List<TrendPoint> DailyPatients   { get; set; }
        public List<TrendPoint> MonthlyRevenue  { get; set; }
    }

    public class TrendPoint
    {
        public string  Label  { get; set; }
        public decimal Value  { get; set; }
    }
}
