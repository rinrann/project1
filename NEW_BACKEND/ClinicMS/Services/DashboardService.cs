using System;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Interfaces;
using ClinicMS.Services.Interfaces;

namespace ClinicMS.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;

        public DashboardService(IDashboardRepository repo) { _repo = repo; }

        public DashboardMetricsDto GetMetrics(string compCode, string preset, DateTime? from, DateTime? to)
        {
            var (f, t) = ResolveRange(preset, from, to);
            return _repo.GetMetrics(compCode, f, t);
        }

        private static Tuple<DateTime, DateTime> ResolveRange(string preset, DateTime? from, DateTime? to)
        {
            DateTime today = DateTime.Today;

            if (string.IsNullOrEmpty(preset))
                preset = "thisMonth";

            switch (preset)
            {
                case "today":
                    return Tuple.Create(today, today);

                case "yesterday":
                    return Tuple.Create(today.AddDays(-1), today.AddDays(-1));

                case "last7":
                    return Tuple.Create(today.AddDays(-6), today);

                case "last30":
                    return Tuple.Create(today.AddDays(-29), today);

                case "thisWeek":
                    return Tuple.Create(today.AddDays(-(int)today.DayOfWeek), today);

                case "lastWeek":
                    return Tuple.Create(
                        today.AddDays(-(int)today.DayOfWeek - 7),
                        today.AddDays(-(int)today.DayOfWeek - 1));

                case "thisMonth":
                    return Tuple.Create(
                        new DateTime(today.Year, today.Month, 1),
                        today);

                case "prevMonth":
                    return Tuple.Create(
                        new DateTime(today.Year, today.Month, 1).AddMonths(-1),
                        new DateTime(today.Year, today.Month, 1).AddDays(-1));

                case "thisQuarter":
                    return Tuple.Create(
                        QuarterStart(today),
                        today);

                case "prevQuarter":
                    return Tuple.Create(
                        QuarterStart(today).AddMonths(-3),
                        QuarterStart(today).AddDays(-1));

                case "thisYear":
                    return Tuple.Create(
                        new DateTime(today.Year, 1, 1),
                        today);

                case "prevYear":
                    return Tuple.Create(
                        new DateTime(today.Year - 1, 1, 1),
                        new DateTime(today.Year - 1, 12, 31));

                case "custom":
                    return Tuple.Create(
                        from ?? today.AddDays(-29),
                        to ?? today);

                default:
                    return Tuple.Create(
                        new DateTime(today.Year, today.Month, 1),
                        today);
            }
        }

        private static DateTime QuarterStart(DateTime d)
        {
            int q = (d.Month - 1) / 3;
            return new DateTime(d.Year, q * 3 + 1, 1);
        }
    }
}
