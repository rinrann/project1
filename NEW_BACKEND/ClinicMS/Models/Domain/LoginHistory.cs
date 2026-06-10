using System;

namespace ClinicMS.Models.Domain
{
    public class LoginHistory
    {
        public int      Id          { get; set; }
        public string   UserName    { get; set; }
        public DateTime LoginDate   { get; set; }
        public string   IpAddress   { get; set; }
        public DateTime? LogoutDate { get; set; }
        public int      Flag        { get; set; }
    }
}
