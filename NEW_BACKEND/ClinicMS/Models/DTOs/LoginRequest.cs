using System.ComponentModel.DataAnnotations;

namespace ClinicMS.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Identifier { get; set; }  // Employee ID or email

        [Required]
        public string Password   { get; set; }

        /// <summary>Financial year code selected by the user at sign-in (e.g. "2025-2026").</summary>
        public string YearCode   { get; set; }

        public bool   RememberMe { get; set; }
    }
}
