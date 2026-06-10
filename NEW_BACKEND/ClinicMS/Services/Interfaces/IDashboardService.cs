using System;
using ClinicMS.Models.DTOs;

namespace ClinicMS.Services.Interfaces
{
    public interface IDashboardService
    {
        DashboardMetricsDto GetMetrics(string compCode, string preset, DateTime? from, DateTime? to);
    }
}
