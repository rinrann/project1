using System;
using System.Collections.Generic;
using System.Data;
using ClinicMS.Models.Domain;
using ClinicMS.Repositories.Base;
using ClinicMS.Repositories.Interfaces;

namespace ClinicMS.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public IEnumerable<User> GetAll(string compCode)
        {
            var dt = QueryTable(
                @"SELECT u.*, r.UserRoleName,
                         ISNULL(u.AdminUser,'0') AdminUser,
                         CONVERT(VARCHAR, u.ExpiryDate, 103) AS ExDateStr
                  FROM   GN_UserDetails u
                  LEFT JOIN GN_UserRole r ON r.UserRoleID = u.UserRoleID AND r.compcode = u.compcode
                  WHERE  u.Status = 1 AND u.CompCode = @cc
                  ORDER  BY u.UserName",
                cmd => cmd.Parameters.AddWithValue("@cc", compCode));

            var list = new List<User>();
            foreach (DataRow r in dt.Rows) list.Add(Map(r));
            return list;
        }

        public User GetById(string userId, string compCode)
        {
            var dt = QueryTable(
                @"SELECT u.*, r.UserRoleName
                  FROM   GN_UserDetails u
                  LEFT JOIN GN_UserRole r ON r.UserRoleID=u.UserRoleID AND r.compcode=u.compcode
                  WHERE  u.UserId=@uid AND u.compcode=@cc AND u.Status=1",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@cc",  compCode);
                });
            return dt.Rows.Count == 0 ? null : Map(dt.Rows[0]);
        }

        public bool Create(User u)
        {
            int rows = Execute(
                @"INSERT INTO GN_UserDetails
                    (compcode, CreateDate, UserId, UserName, UserRoleID, Password,
                     PhoneNo_1, PhoneNo_2, EmailId, CreatedBy, ExpiryDate, Status, AdminUser)
                  VALUES
                    (@cc, GETDATE(), @uid, @uname, @rid, @pwd,
                     @p1, @p2, @email, @cb, @exp, 1, @admin)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@cc",    u.CompCode);
                    cmd.Parameters.AddWithValue("@uid",   u.UserId);
                    cmd.Parameters.AddWithValue("@uname", u.UserName);
                    cmd.Parameters.AddWithValue("@rid",   u.UserRoleId);
                    cmd.Parameters.AddWithValue("@pwd",   u.PasswordHash);
                    cmd.Parameters.AddWithValue("@p1",    u.PhoneNo1    ?? "");
                    cmd.Parameters.AddWithValue("@p2",    u.PhoneNo2    ?? "");
                    cmd.Parameters.AddWithValue("@email", u.Email       ?? "");
                    cmd.Parameters.AddWithValue("@cb",    u.CreatedBy   ?? "");
                    cmd.Parameters.AddWithValue("@exp",   (object)u.ExpiryDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@admin", u.AdminUser   ?? "0");
                });
            return rows > 0;
        }

        public bool Update(User u)
        {
            int rows = Execute(
                @"UPDATE GN_UserDetails
                  SET UserName=@uname, UserRoleID=@rid, PhoneNo_1=@p1, PhoneNo_2=@p2,
                      EmailId=@email, ExpiryDate=@exp, AdminUser=@admin
                  WHERE UserId=@uid AND compcode=@cc",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@uname", u.UserName);
                    cmd.Parameters.AddWithValue("@rid",   u.UserRoleId);
                    cmd.Parameters.AddWithValue("@p1",    u.PhoneNo1  ?? "");
                    cmd.Parameters.AddWithValue("@p2",    u.PhoneNo2  ?? "");
                    cmd.Parameters.AddWithValue("@email", u.Email     ?? "");
                    cmd.Parameters.AddWithValue("@exp",   (object)u.ExpiryDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@admin", u.AdminUser ?? "0");
                    cmd.Parameters.AddWithValue("@uid",   u.UserId);
                    cmd.Parameters.AddWithValue("@cc",    u.CompCode);
                });
            return rows > 0;
        }

        public bool Delete(string userId, string compCode)
        {
            int rows = Execute(
                "UPDATE GN_UserDetails SET Status=2 WHERE UserId=@uid AND compcode=@cc",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@cc",  compCode);
                });
            return rows > 0;
        }

        public bool ChangePassword(string userId, string compCode, string newHash)
        {
            int rows = Execute(
                "UPDATE GN_UserDetails SET Password=@pwd WHERE UserId=@uid AND compcode=@cc",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@pwd", newHash);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@cc",  compCode);
                });
            return rows > 0;
        }

        public IEnumerable<UserRole> GetRoles(string compCode)
        {
            var dt = QueryTable(
                "SELECT UserRoleID, UserRoleName, Status, compcode FROM GN_UserRole WHERE Status=1 AND compcode=@cc",
                cmd => cmd.Parameters.AddWithValue("@cc", compCode));

            var list = new List<UserRole>();
            foreach (DataRow r in dt.Rows)
                list.Add(new UserRole
                {
                    UserRoleId   = S(r, "UserRoleID"),
                    UserRoleName = S(r, "UserRoleName"),
                    Status       = I(r, "Status"),
                    CompCode     = S(r, "compcode")
                });
            return list;
        }

        private static User Map(DataRow r) => new User
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
