using System;
using System.Collections.Generic;
using System.Linq;
using ClinicMS.Helpers;
using ClinicMS.Models.Domain;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Interfaces;
using ClinicMS.Services.Interfaces;

namespace ClinicMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo) { _repo = repo; }

        public IEnumerable<UserDto> GetAll(string compCode)
            => _repo.GetAll(compCode).Select(ToDto);

        public UserDto GetById(string userId, string compCode)
        {
            var u = _repo.GetById(userId, compCode);
            return u == null ? null : ToDto(u);
        }

        public bool Create(CreateUserRequest req, string compCode, string createdBy)
        {
            var user = new User
            {
                UserId       = req.UserId,
                CompCode     = compCode,
                UserName     = req.UserName,
                UserRoleId   = req.UserRoleId,
                PasswordHash = PasswordHelper.Hash(req.Password),
                PhoneNo1     = req.PhoneNo1,
                PhoneNo2     = req.PhoneNo2,
                Email        = req.Email,
                ExpiryDate   = req.ExpiryDate,
                AdminUser    = req.IsAdmin ? "1" : "0",
                CreatedBy    = createdBy,
                Status       = 1
            };
            return _repo.Create(user);
        }

        public bool Update(UserDto dto, string compCode)
        {
            var user = new User
            {
                UserId     = dto.UserId,
                CompCode   = compCode,
                UserName   = dto.UserName,
                UserRoleId = dto.UserRoleId,
                PhoneNo1   = dto.PhoneNo1,
                PhoneNo2   = dto.PhoneNo2,
                Email      = dto.Email,
                ExpiryDate = dto.ExpiryDate,
                AdminUser  = dto.AdminUser
            };
            return _repo.Update(user);
        }

        public bool Delete(string userId, string compCode)
            => _repo.Delete(userId, compCode);

        public bool ChangePassword(string userId, string compCode, ChangePasswordRequest req)
        {
            var user = _repo.GetById(userId, compCode);
            if (user == null) return false;

            if (!PasswordHelper.Verify(req.OldPassword, user.PasswordHash))
                throw new UnauthorizedAccessException("Current password is incorrect.");

            return _repo.ChangePassword(userId, compCode, PasswordHelper.Hash(req.NewPassword));
        }

        public IEnumerable<UserRole> GetRoles(string compCode)
            => _repo.GetRoles(compCode);

        private static UserDto ToDto(User u) => new UserDto
        {
            UserId       = u.UserId,
            UserName     = u.UserName,
            UserRoleId   = u.UserRoleId,
            UserRoleName = u.UserRoleName,
            PhoneNo1     = u.PhoneNo1,
            PhoneNo2     = u.PhoneNo2,
            Email        = u.Email,
            ExpiryDate   = u.ExpiryDate,
            AdminUser    = u.AdminUser,
            Status       = u.Status
        };
    }
}
