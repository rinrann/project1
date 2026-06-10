using System.Collections.Generic;
using ClinicMS.Models.DTOs;

namespace ClinicMS.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponse Login(LoginRequest request, string clientIp);
        void          Logout(string userId, string compCode);
        UserSessionDto GetCurrentUser(string userId, string compCode);
        IEnumerable<CompanyDto> GetCompanies();
    }
}
