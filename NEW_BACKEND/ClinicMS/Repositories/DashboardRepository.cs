using System;
using System.Collections.Generic;
using System.Data;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Base;
using ClinicMS.Repositories.Interfaces;

namespace ClinicMS.Repositories
{
    public class DashboardRepository : BaseRepository, IDashboardRepository
    {
        public DashboardMetricsDto GetMetrics(string compCode, DateTime from, DateTime to)
        {
            // All aggregations in a single multi-result-set stored-proc call
            // falls back to inline queries if the proc doesn't exist yet.
            var ds = QueryDataSet(
                @"SELECT ISNULL(SUM(BillAmount),0) AS Revenue,
                         ISNULL(SUM(PaidAmount),0) AS Collected
                  FROM   OPD_BillMaster
                  WHERE  compcode=@cc AND Status=1
                    AND  CONVERT(DATE,BillDate) BETWEEN @from AND @to;

                  SELECT COUNT(1) AS TotalPatients
                  FROM   OPD_PatientMaster
                  WHERE  compcode=@cc AND Status=1
                    AND  CONVERT(DATE,RegistrationDate) BETWEEN @from AND @to;

                  SELECT COUNT(1) AS TotalAppointments
                  FROM   OPD_AppointmentMaster
                  WHERE  compcode=@cc
                    AND  CONVERT(DATE,AppointmentDate) BETWEEN @from AND @to;

                  SELECT COUNT(1) AS SalesTxns
                  FROM   MED_SalesMaster
                  WHERE  compcode=@cc
                    AND  CONVERT(DATE,SaleDate) BETWEEN @from AND @to;

                  SELECT CONVERT(VARCHAR(10),CONVERT(DATE,BillDate),23) AS Label,
                         ISNULL(SUM(BillAmount),0) AS Value
                  FROM   OPD_BillMaster
                  WHERE  compcode=@cc AND Status=1
                    AND  CONVERT(DATE,BillDate) BETWEEN @from AND @to
                  GROUP  BY CONVERT(DATE,BillDate)
                  ORDER  BY CONVERT(DATE,BillDate);

                  SELECT CONVERT(VARCHAR(10),CONVERT(DATE,RegistrationDate),23) AS Label,
                         COUNT(1) AS Value
                  FROM   OPD_PatientMaster
                  WHERE  compcode=@cc AND Status=1
                    AND  CONVERT(DATE,RegistrationDate) BETWEEN @from AND @to
                  GROUP  BY CONVERT(DATE,RegistrationDate)
                  ORDER  BY CONVERT(DATE,RegistrationDate);",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@cc",   compCode);
                    cmd.Parameters.AddWithValue("@from", from.Date);
                    cmd.Parameters.AddWithValue("@to",   to.Date);
                });

            var metrics = new DashboardMetricsDto
            {
                DailyRevenue  = new List<TrendPoint>(),
                DailyPatients = new List<TrendPoint>(),
                MonthlyRevenue = new List<TrendPoint>()
            };

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var r = ds.Tables[0].Rows[0];
                metrics.Revenue = D(r, "Revenue");
            }
            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                metrics.NewRegistrations = metrics.TotalPatients = I(ds.Tables[1].Rows[0], "TotalPatients");
            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                metrics.TotalAppointments = I(ds.Tables[2].Rows[0], "TotalAppointments");
            if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                metrics.SalesTxns = I(ds.Tables[3].Rows[0], "SalesTxns");
            if (ds.Tables.Count > 4)
                foreach (DataRow r in ds.Tables[4].Rows)
                    metrics.DailyRevenue.Add(new TrendPoint { Label = S(r,"Label"), Value = D(r,"Value") });
            if (ds.Tables.Count > 5)
                foreach (DataRow r in ds.Tables[5].Rows)
                    metrics.DailyPatients.Add(new TrendPoint { Label = S(r,"Label"), Value = D(r,"Value") });

            if (metrics.TotalPatients > 0)
                metrics.AvgRevenuePerPatient = metrics.Revenue / metrics.TotalPatients;
            if (metrics.TotalAppointments > 0)
                metrics.AvgRevenuePerAppointment = metrics.Revenue / metrics.TotalAppointments;

            return metrics;
        }
    }
}
