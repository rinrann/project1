using System;

namespace ClinicMS.Models.Domain
{
    public class User
    {
        public string UserId       { get; set; }
        public string CompCode     { get; set; }
        public string UserName     { get; set; }
        public string UserRoleId   { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNo1     { get; set; }
        public string PhoneNo2     { get; set; }
        public string Email        { get; set; }
        public string CreatedBy    { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int    Status       { get; set; }   // 1 = active, 2 = deleted
        public string AdminUser    { get; set; }
        public DateTime CreateDate { get; set; }

        // Navigation (populated by join in repository)
        public string UserRoleName { get; set; }
    }
}
