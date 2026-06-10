using System;
using System.Collections.Generic;
using ClinicMS.Helpers;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Interfaces;
using ClinicMS.Services.Interfaces;

namespace ClinicMS.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;

        public AuthService(IAuthRepository repo)
        {
            _repo = repo;
        }

        public LoginResponse Login(LoginRequest request, string clientIp)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Identifier))
                throw new ArgumentException("Identifier is required.");

            // Resolve company code from the composite key sent by the login dropdown.
            // The dropdown value is "compCode~yearCode~compName"; we split it here.
            string compCode = null, yearCode = null, compName = null;
            if (!string.IsNullOrWhiteSpace(request.YearCode))
            {
                var parts = request.YearCode.Split('~');
                compCode = parts.Length > 0 ? parts[0].Trim() : null;
                yearCode = parts.Length > 1 ? parts[1].Trim() : null;
                compName = parts.Length > 2 ? parts[2].Trim() : null;
            }

            var user = _repo.GetUserByIdentifier(request.Identifier.Trim(), compCode);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // Account expiry check
            if (user.ExpiryDate.HasValue && user.ExpiryDate.Value.Date < DateTime.Today)
                throw new UnauthorizedAccessException("Your account has expired. Contact your administrator.");

            bool passwordOk = false;

            if (PasswordHelper.IsLegacyHash(user.PasswordHash))
            {
                // Legacy path: the old system's Enc.MAIN.SCrypt stored a direct
                // string comparison value.  We cannot reproduce that hash here,
                // so we compare the raw stored value as a plain-text migration aid.
                // On success, immediately re-hash with PBKDF2 and update the row.
                if (user.PasswordHash == request.Password)
                {
                    passwordOk = true;
                    string newHash = PasswordHelper.Hash(request.Password);
                    _repo.UpdatePasswordHash(user.UserId, user.CompCode, newHash);
                }
            }
            else
            {
                passwordOk = PasswordHelper.Verify(request.Password, user.PasswordHash);
            }

            if (!passwordOk)
                throw new UnauthorizedAccessException("Invalid credentials.");

            // Derive role from Employee-ID prefix (implicit role detection, mirrors frontend).
            string role = RoleHelper.RoleFromEmpId(user.UserId) ?? user.UserRoleName ?? "User";

            // Build JWT.
            string token = JwtHelper.GenerateToken(
                user.UserId, user.UserName, user.UserRoleId, role,
                user.CompCode, yearCode ?? "");

            // Log the login.
            _repo.InsertLoginHistory(user.UserId, clientIp);

            var session = new UserSessionDto
            {
                UserId      = user.UserId,
                Name        = user.UserName,
                Email       = user.Email,
                Role        = role,
                RoleId      = user.UserRoleId,
                CompCode    = user.CompCode,
                YearCode    = yearCode,
                CompName    = compName,
                Permissions = RoleHelper.PermissionsForRole(role)
            };

            return new LoginResponse { Token = token, User = session };
        }

        public void Logout(string userId, string compCode)
        {
            _repo.UpdateLogoutHistory(userId);
        }

        public UserSessionDto GetCurrentUser(string userId, string compCode)
        {
            var user = _repo.GetUserByIdentifier(userId, compCode);
            if (user == null) return null;

            string role = RoleHelper.RoleFromEmpId(user.UserId) ?? user.UserRoleName ?? "User";
            return new UserSessionDto
            {
                UserId      = user.UserId,
                Name        = user.UserName,
                Email       = user.Email,
                Role        = role,
                RoleId      = user.UserRoleId,
                CompCode    = user.CompCode,
                Permissions = RoleHelper.PermissionsForRole(role)
            };
        }

        public IEnumerable<CompanyDto> GetCompanies() => _repo.GetCompanies();
    }
}
