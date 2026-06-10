using System;
using System.Collections.Generic;
using System.Data;
using ClinicMS.Models.Domain;
using ClinicMS.Models.DTOs;
using ClinicMS.Repositories.Base;
using ClinicMS.Repositories.Interfaces;

namespace ClinicMS.Repositories
{
    public class PatientRepository : BaseRepository, IPatientRepository
    {
        public PagedResult<PatientListDto> GetPatients(string compCode, string q, int page, int pageSize)
        {
            page     = Math.Max(1, page);
            //pageSize = Math.Clamp(pageSize, 1, 200);
            pageSize = Math.Max(1, Math.Min(pageSize, 200));
            int offset = (page - 1) * pageSize;

            string where = string.IsNullOrWhiteSpace(q) ? "" :
                @" AND (LOWER(p.PatientReg) LIKE LOWER(@q)
                    OR   LOWER(p.FirstName + ' ' + p.LastName) LIKE LOWER(@q)
                    OR   p.Mobile LIKE @q)";

            string countSql = $@"
                SELECT COUNT(1)
                FROM   OPD_PatientMaster p
                WHERE  p.compcode = @cc AND p.Status = 1 {where}";

            string dataSql = $@"
                SELECT p.PatientReg,
                       p.FirstName + ' ' + p.LastName AS FullName,
                       p.Gender, p.Age, p.Mobile,
                       CONVERT(DATE, p.RegistrationDate) AS RegistrationDate
                FROM   OPD_PatientMaster p
                WHERE  p.compcode = @cc AND p.Status = 1 {where}
                ORDER  BY p.RegistrationDate DESC
                OFFSET @offset ROWS FETCH NEXT @ps ROWS ONLY";

            int total = Convert.ToInt32(ExecuteScalar(countSql, cmd =>
            {
                cmd.Parameters.AddWithValue("@cc", compCode);
                if (!string.IsNullOrWhiteSpace(q))
                    cmd.Parameters.AddWithValue("@q", "%" + q + "%");
            }));

            var dt = QueryTable(dataSql, cmd =>
            {
                cmd.Parameters.AddWithValue("@cc",     compCode);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@ps",     pageSize);
                if (!string.IsNullOrWhiteSpace(q))
                    cmd.Parameters.AddWithValue("@q", "%" + q + "%");
            });

            var items = new List<PatientListDto>();
            foreach (DataRow r in dt.Rows)
                items.Add(new PatientListDto
                {
                    PatientReg       = S(r, "PatientReg"),
                    FullName         = S(r, "FullName"),
                    Gender           = S(r, "Gender"),
                    Age              = r["Age"] == DBNull.Value ? (int?)null : I(r, "Age"),
                    Mobile           = S(r, "Mobile"),
                    RegistrationDate = DT(r, "RegistrationDate") ?? DateTime.Today
                });

            return new PagedResult<PatientListDto>
            {
                Items      = items,
                TotalCount = total,
                Page       = page,
                PageSize   = pageSize
            };
        }

        public Patient GetById(string patientReg, string compCode)
        {
            var dt = QueryTable(
                @"SELECT * FROM OPD_PatientMaster
                  WHERE PatientReg = @reg AND compcode = @cc AND Status = 1",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@reg", patientReg);
                    cmd.Parameters.AddWithValue("@cc",  compCode);
                });
            return dt.Rows.Count == 0 ? null : MapPatient(dt.Rows[0]);
        }

        public string Create(Patient p, string createdBy)
        {
            // Generate registration number: PYYYYMMDD-NNNN
            string reg = GenerateRegNo(p.CompCode);
            Execute(
                @"INSERT INTO OPD_PatientMaster
                    (PatientReg, compcode, FirstName, LastName, Gender, DateOfBirth, Age,
                     Mobile, EmailId, Address, City, State, BloodGroup, MaritalStatus,
                     ReferredBy, RegistrationDate, CreatedBy, Status)
                  VALUES
                    (@reg, @cc, @fn, @ln, @g, @dob, @age,
                     @mob, @email, @addr, @city, @state, @bg, @ms,
                     @ref, GETDATE(), @cb, 1)",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@reg",   reg);
                    cmd.Parameters.AddWithValue("@cc",    p.CompCode);
                    cmd.Parameters.AddWithValue("@fn",    p.FirstName  ?? "");
                    cmd.Parameters.AddWithValue("@ln",    p.LastName   ?? "");
                    cmd.Parameters.AddWithValue("@g",     p.Gender     ?? "");
                    cmd.Parameters.AddWithValue("@dob",   (object)p.DateOfBirth ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@age",   (object)p.Age        ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@mob",   p.Mobile     ?? "");
                    cmd.Parameters.AddWithValue("@email", p.Email      ?? "");
                    cmd.Parameters.AddWithValue("@addr",  p.Address    ?? "");
                    cmd.Parameters.AddWithValue("@city",  p.City       ?? "");
                    cmd.Parameters.AddWithValue("@state", p.State      ?? "");
                    cmd.Parameters.AddWithValue("@bg",    p.BloodGroup ?? "");
                    cmd.Parameters.AddWithValue("@ms",    p.MaritalStatus ?? "");
                    cmd.Parameters.AddWithValue("@ref",   p.ReferredBy ?? "");
                    cmd.Parameters.AddWithValue("@cb",    createdBy    ?? "");
                });
            return reg;
        }

        public bool Update(Patient p)
        {
            int rows = Execute(
                @"UPDATE OPD_PatientMaster
                  SET FirstName=@fn, LastName=@ln, Gender=@g, DateOfBirth=@dob, Age=@age,
                      Mobile=@mob, EmailId=@email, Address=@addr, City=@city, State=@state,
                      BloodGroup=@bg, MaritalStatus=@ms, ReferredBy=@ref
                  WHERE PatientReg=@reg AND compcode=@cc AND Status=1",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@fn",   p.FirstName  ?? "");
                    cmd.Parameters.AddWithValue("@ln",   p.LastName   ?? "");
                    cmd.Parameters.AddWithValue("@g",    p.Gender     ?? "");
                    cmd.Parameters.AddWithValue("@dob",  (object)p.DateOfBirth ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@age",  (object)p.Age        ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@mob",  p.Mobile     ?? "");
                    cmd.Parameters.AddWithValue("@email",p.Email      ?? "");
                    cmd.Parameters.AddWithValue("@addr", p.Address    ?? "");
                    cmd.Parameters.AddWithValue("@city", p.City       ?? "");
                    cmd.Parameters.AddWithValue("@state",p.State      ?? "");
                    cmd.Parameters.AddWithValue("@bg",   p.BloodGroup ?? "");
                    cmd.Parameters.AddWithValue("@ms",   p.MaritalStatus ?? "");
                    cmd.Parameters.AddWithValue("@ref",  p.ReferredBy ?? "");
                    cmd.Parameters.AddWithValue("@reg",  p.PatientReg);
                    cmd.Parameters.AddWithValue("@cc",   p.CompCode);
                });
            return rows > 0;
        }

        public bool Delete(string patientReg, string compCode)
        {
            int rows = Execute(
                "UPDATE OPD_PatientMaster SET Status=0 WHERE PatientReg=@reg AND compcode=@cc",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@reg", patientReg);
                    cmd.Parameters.AddWithValue("@cc",  compCode);
                });
            return rows > 0;
        }

        public IEnumerable<Patient> GetPatientHistory(string patientReg, string compCode)
        {
            var dt = QueryTable(
                @"SELECT * FROM OPD_PatientHistoryMapping
                  WHERE PatientReg=@reg AND compcode=@cc
                  ORDER BY Date DESC",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@reg", patientReg);
                    cmd.Parameters.AddWithValue("@cc",  compCode);
                });

            var list = new List<Patient>();
            foreach (DataRow r in dt.Rows)
                list.Add(new Patient { PatientReg = S(r, "PatientReg") });
            return list;
        }

        public int CountToday(string compCode)
        {
            var result = ExecuteScalar(
                @"SELECT COUNT(1) FROM OPD_PatientMaster
                  WHERE compcode=@cc AND Status=1
                    AND CONVERT(DATE, RegistrationDate) = CONVERT(DATE, GETDATE())",
                cmd => cmd.Parameters.AddWithValue("@cc", compCode));
            return Convert.ToInt32(result);
        }

        // ── Helpers ──────────────────────────────────────────────────────────────

        private string GenerateRegNo(string compCode)
        {
            string prefix = $"P{DateTime.Today:yyyyMMdd}-";
            var result = ExecuteScalar(
                @"SELECT ISNULL(MAX(CAST(SUBSTRING(PatientReg, LEN(@p)+1, 4) AS INT)),0)+1
                  FROM   OPD_PatientMaster
                  WHERE  compcode=@cc AND PatientReg LIKE @p+'%'",
                cmd =>
                {
                    cmd.Parameters.AddWithValue("@p",  prefix);
                    cmd.Parameters.AddWithValue("@cc", compCode);
                });
            return prefix + Convert.ToInt32(result).ToString("D4");
        }

        private static Patient MapPatient(DataRow r) => new Patient
        {
            PatientReg       = S(r, "PatientReg"),
            CompCode         = S(r, "compcode"),
            FirstName        = S(r, "FirstName"),
            LastName         = S(r, "LastName"),
            FullName         = S(r, "FirstName") + " " + S(r, "LastName"),
            Gender           = S(r, "Gender"),
            DateOfBirth      = DT(r, "DateOfBirth"),
            Age              = r.Table.Columns.Contains("Age") && r["Age"] != DBNull.Value ? (int?)Convert.ToInt32(r["Age"]) : null,
            Mobile           = S(r, "Mobile"),
            Email            = S(r, "EmailId"),
            Address          = S(r, "Address"),
            City             = S(r, "City"),
            State            = S(r, "State"),
            BloodGroup       = S(r, "BloodGroup"),
            MaritalStatus    = S(r, "MaritalStatus"),
            ReferredBy       = S(r, "ReferredBy"),
            RegistrationDate = DT(r, "RegistrationDate") ?? DateTime.Today,
            Status           = I(r, "Status")
        };
    }
}
