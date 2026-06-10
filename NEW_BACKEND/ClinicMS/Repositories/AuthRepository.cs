using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ClinicMS.Models.Domain;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Base;
using ClinicMS.Repositories.Interfaces;

namespace ClinicMS.Repositories
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        /// <summary>
        /// Looks up a user by Employee-ID or email within a company.
        /// If compCode is null/empty the lookup is cross-company (used when
        /// we only have an email before the company is known).
        /// </summary>
        public User GetUserByIdentifier(string identifier, string compCode)
        {
            string sql = string.IsNullOrEmpty(compCode)
                ? @"SELECT u.*, r.UserRoleName
                    FROM   GN_UserDetails u
                    LEFT JOIN GN_UserRole r ON r.UserRoleID = u.UserRoleID AND r.compcode = u.compcode
                    WHERE  u.Status = 1
                      AND  (LOWER(u.UserId) = LOWER(@id) OR LOWER(u.EmailId) = LOWER(@id))"
                : @"SELECT u.*, r.UserRoleName
                    FROM   GN_UserDetails u
                    LEFT JOIN GN_UserRole r ON r.UserRoleID = u.UserRoleID AND r.compcode = u.compcode
                    WHERE  u.Status = 1
                      AND  u.compcode = @compCode
                      AND  (LOWER(u.UserId) = LOWER(@id) OR LOWER(u.EmailId) = LOWER(@id))";

            var dt = QueryTable(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", identifier.Trim());
                if (!string.IsNullOrEmpty(compCode))
                    cmd.Parameters.AddWithValue("@compCode", compCode.Trim());
            });

            return dt.Rows.Count == 0 ? null : MapUser(dt.Rows[0]);
        }

        public void UpdatePasswordHash(string userId, string compCode, string newHash)
        {
            Execute(
                "UPDATE GN_UserDetails SET Password = @pwd WHERE UserId = @uid AND compcode = @cc",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@pwd", newHash);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@cc",  compCode);
                });
        }

        public void InsertLoginHistory(string userId, string ipAddress)
        {
            Execute(
                "INSERT INTO LoginHistory (UserName, Logindate, IPAddress) VALUES (@uid, GETDATE(), @ip)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@ip",  ipAddress ?? "");
                });
        }

        public void UpdateLogoutHistory(string userId)
        {
            Execute(
                "UPDATE LoginHistory SET LogoutDate = GETDATE(), flag = 1 WHERE UserName = @uid AND LogoutDate IS NULL",
                cmd => cmd.Parameters.AddWithValue("@uid", userId));
        }

        public IEnumerable<CompanyDto> GetCompanies()
        {
            var dt = QueryTable(
                @"SELECT LTRIM(RTRIM(compcode))  AS CompCode,
                         LTRIM(RTRIM(yearcode))  AS YearCode,
                         LTRIM(RTRIM(coname))    AS CoName,
                         compcode + ' : ' + coname + ' : ' + yearcode AS Display
                  FROM   parms
                  ORDER  BY coname");

            var list = new List<CompanyDto>();
            foreach (DataRow r in dt.Rows)
                list.Add(new CompanyDto
                {
                    CompCode = S(r, "CompCode"),
                    YearCode = S(r, "YearCode"),
                    CoName   = S(r, "CoName"),
                    Display  = S(r, "Display")
                });
            return list;
        }

        public bool IsUserAlreadyLoggedIn(string userId)
        {
            var result = ExecuteScalar(
                "SELECT COUNT(1) FROM LoginHistory WHERE UserName = @uid AND LogoutDate IS NULL",
                cmd => cmd.Parameters.AddWithValue("@uid", userId));
            return Convert.ToInt32(result) > 0;
        }

        public void ForceLogout(string userId)
            => UpdateLogoutHistory(userId);

        // ── Mapper ───────────────────────────────────────────────────────────────

        private static User MapUser(DataRow r) => new User
        {
            UserId       = S(r, "UserId"),
            CompCode     = S(r, "compcode"),
            UserName     = S(r, "UserName"),
            UserRoleId   = S(r, "UserRoleID"),
            UserRoleName = S(r, "UserRoleName"),
            PasswordHash = S(r, "Password"),
            PhoneNo1     = S(r, "PhoneNo_1"),
            PhoneNo2     = S(r, "PhoneNo_2"),
            Email        = S(r, "EmailId"),
            CreatedBy    = S(r, "CreatedBy"),
            ExpiryDate   = DT(r, "ExpiryDate"),
            Status       = I(r, "Status"),
            AdminUser    = S(r, "AdminUser"),
            CreateDate   = DT(r, "CreateDate") ?? DateTime.MinValue
        };
    }
}
