using System.Collections.Generic;
using ClinicMS.Models.Domain;

namespace ClinicMS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User>     GetAll(string compCode);
        User                  GetById(string userId, string compCode);
        bool                  Create(User user);
        bool                  Update(User user);
        bool                  Delete(string userId, string compCode);
        bool                  ChangePassword(string userId, string compCode, string newHash);
        IEnumerable<UserRole> GetRoles(string compCode);
    }
}
