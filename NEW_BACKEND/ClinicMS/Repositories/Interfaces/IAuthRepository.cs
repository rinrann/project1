using System.Collections.Generic;
using ClinicMS.Models.Domain;
using ClinicMS.Models.DTOs;

namespace ClinicMS.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        User GetUserByIdentifier(string identifier, string compCode);
        void UpdatePasswordHash(string userId, string compCode, string newHash);
        void InsertLoginHistory(string userId, string ipAddress);
        void UpdateLogoutHistory(string userId);
        IEnumerable<CompanyDto> GetCompanies();
        bool IsUserAlreadyLoggedIn(string userId);
        void ForceLogout(string userId);
    }
}
