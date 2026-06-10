using System;
using System.Collections.Generic;

namespace ClinicMS.Helpers
{
    /// <summary>
    /// Mirrors the frontend's implicit role-detection and permission matrix
    /// so backend enforcement matches what the UI shows.
    /// </summary>
    public static class RoleHelper
    {
        private static readonly Dictionary<string, string> PrefixRole =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "OWN",   "Super Admin" }, { "SA",    "Super Admin" }, { "SADMIN", "Super Admin" },
                { "SUP",   "Admin"       }, { "ADM",   "Admin"       }, { "ADMIN",  "Admin" },
                { "DOC",   "Doctor"      }, { "DR",    "Doctor"      },
                { "NUR",   "Nurse"       },
                { "REC",   "Receptionist" }, { "FO",  "Receptionist" },
                { "EMB",   "Embryologist" },
                { "ACC",   "Accounts Team" }, { "FIN", "Accounts Team" },
                { "MGT",   "Management"  }, { "MGR",  "Management"   },
            };

        // module → role → access level
        private static readonly Dictionary<string, Dictionary<string, string>> PermMatrix =
            new Dictionary<string, Dictionary<string, string>>
            {
                ["Dashboard"]     = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="View", ["Doctor"]="View", ["Embryologist"]="View", ["Nurse"]="View", ["Accounts Team"]="View" },
                ["Patients"]      = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="Edit", ["Doctor"]="Edit", ["Embryologist"]="View", ["Nurse"]="Edit",  ["Accounts Team"]="View" },
                ["Appointments"]  = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="Full", ["Doctor"]="Edit", ["Embryologist"]="None", ["Nurse"]="View",  ["Accounts Team"]="None" },
                ["Doctors"]       = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="View", ["Doctor"]="View", ["Embryologist"]="View", ["Nurse"]="View",  ["Accounts Team"]="None" },
                ["Investigation"] = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="Edit", ["Doctor"]="Full", ["Embryologist"]="View", ["Nurse"]="Edit",  ["Accounts Team"]="None" },
                ["IVF Cycles"]    = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="None", ["Doctor"]="Full", ["Embryologist"]="Edit", ["Nurse"]="View",  ["Accounts Team"]="None" },
                ["Billing"]       = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="Edit", ["Doctor"]="None", ["Embryologist"]="None", ["Nurse"]="None",  ["Accounts Team"]="Full" },
                ["Pharmacy"]      = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="View", ["Doctor"]="View", ["Embryologist"]="View", ["Nurse"]="Edit",  ["Accounts Team"]="View" },
                ["Employees"]     = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="None", ["Doctor"]="None", ["Embryologist"]="None", ["Nurse"]="None",  ["Accounts Team"]="None" },
                ["Administration"]= new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="None", ["Doctor"]="None", ["Embryologist"]="None", ["Nurse"]="None",  ["Accounts Team"]="None" },
                ["Reports"]       = new Dictionary<string, string> { ["Admin"]="Full", ["Receptionist"]="View", ["Doctor"]="View", ["Embryologist"]="View", ["Nurse"]="View",  ["Accounts Team"]="Full" },
            };

        public static string RoleFromEmpId(string empId)
        {
            if (string.IsNullOrEmpty(empId)) return null;
            var prefix = empId.Split(new[] { '-', '_', ' ' }, 2)[0].ToUpperInvariant();
            return PrefixRole.TryGetValue(prefix, out var role) ? role : null;
        }

        public static Dictionary<string, string> PermissionsForRole(string role)
        {
            var result = new Dictionary<string, string>();
            foreach (var module in PermMatrix)
            {
                if (role == "Super Admin")
                    result[module.Key] = "Full";
                else
                    result[module.Key] = module.Value.TryGetValue(role, out var lvl) ? lvl : "None";
            }
            return result;
        }

        public static bool CanDo(string role, string module, string action)
        {
            if (role == "Super Admin") return true;
            if (!PermMatrix.TryGetValue(module, out var roleMap)) return false;
            if (!roleMap.TryGetValue(role, out var level)) return false;

            switch (action)
            {
                case "view":
                    return level != "None";
                case "edit":
                    return level == "Edit" || level == "Full";
                case "full":
                    return level == "Full";
                default:
                    return false;
            }
        }
    }
}
