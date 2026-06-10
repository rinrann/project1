using System.Collections.Generic;
using ClinicMS.Models.DTOs;
using ClinicMS.Models.Domain;

namespace ClinicMS.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto>  GetAll(string compCode);
        UserDto               GetById(string userId, string compCode);
        bool                  Create(CreateUserRequest req, string compCode, string createdBy);
        bool                  Update(UserDto dto, string compCode);
        bool                  Delete(string userId, string compCode);
        bool                  ChangePassword(string userId, string compCode, ChangePasswordRequest req);
        IEnumerable<UserRole> GetRoles(string compCode);
    }
}
