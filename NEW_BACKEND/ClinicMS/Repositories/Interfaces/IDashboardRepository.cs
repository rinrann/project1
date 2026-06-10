using System;
using ClinicMS.Models.DTOs;

namespace ClinicMS.Repositories.Interfaces
{
    public interface IDashboardRepository
    {
        DashboardMetricsDto GetMetrics(string compCode, DateTime from, DateTime to);
    }
}
