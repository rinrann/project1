using System.Collections.Generic;

namespace ClinicMS.Models.DTOs
{
    public class LoginResponse
    {
        public string Token   { get; set; }
        public UserSessionDto User { get; set; }
    }

    public class UserSessionDto
    {
        public string UserId      { get; set; }
        public string Name        { get; set; }
        public string Email       { get; set; }
        public string Role        { get; set; }
        public string RoleId      { get; set; }
        public string CompCode    { get; set; }
        public string YearCode    { get; set; }
        public string CompName    { get; set; }

        /// <summary>Module-level permission map: module name → "None"|"View"|"Edit"|"Full".</summary>
        public Dictionary<string, string> Permissions { get; set; }
    }
}
