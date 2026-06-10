using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Models.DTOs
{
    public class UserDto
    {
        public string   UserId      { get; set; }
        [Required] public string UserName  { get; set; }
        [Required] public string UserRoleId { get; set; }
        public string   UserRoleName { get; set; }
        public string   PhoneNo1    { get; set; }
        public string   PhoneNo2    { get; set; }
        public string   Email       { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string   AdminUser   { get; set; }
        public int      Status      { get; set; }
    }

    public class CreateUserRequest
    {
        [Required] public string UserId      { get; set; }
        [Required] public string UserName    { get; set; }
        [Required] public string UserRoleId  { get; set; }
        [Required] public string Password    { get; set; }
        public string PhoneNo1   { get; set; }
        public string PhoneNo2   { get; set; }
        public string Email      { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool   IsAdmin    { get; set; }
    }

    public class ChangePasswordRequest
    {
        [Required] public string OldPassword { get; set; }
        [Required] public string NewPassword { get; set; }
    }
}
