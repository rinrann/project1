using System;
using System.Security.Cryptography;

namespace ClinicMS.Helpers
{
    /// <summary>
    /// PBKDF2-SHA256 password hashing.
    /// Format stored in DB:  "v2$<iterations>$<base64-salt>$<base64-hash>"
    ///
    /// Migration path from the old Enc.MAIN.SCrypt hashes:
    ///   - On first successful login with the legacy hash, re-hash with this helper
    ///     and update the row — so the DB gradually migrates without a forced reset.
    /// </summary>
    public static class PasswordHelper
    {
        private const int SaltBytes   = 16;
        private const int HashBytes    = 32;
        private const int Iterations   = 120_000;
        private const string Prefix    = "v2$";

        public static string Hash(string password)
        {
            byte[] salt = new byte[SaltBytes];
            using (var rng = new RNGCryptoServiceProvider())
                rng.GetBytes(salt);

            byte[] hash = Pbkdf2(password, salt, Iterations, HashBytes);
            return $"{Prefix}{Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(storedHash))
                return false;

            // New format
            if (storedHash.StartsWith(Prefix))
            {
                var parts = storedHash.Split('$');
                if (parts.Length != 4) return false;
                int iters      = int.Parse(parts[1]);
                byte[] salt    = Convert.FromBase64String(parts[2]);
                byte[] expected = Convert.FromBase64String(parts[3]);
                byte[] actual   = Pbkdf2(password, salt, iters, expected.Length);
                return SlowEquals(expected, actual);
            }

            // Legacy: the old system stored raw SCrypt output (a hex-like string).
            // We cannot reverse it, so return false here — the Auth service handles
            // the "legacy fallback + re-hash" flow when this returns false.
            return false;
        }

        /// <summary>Returns true if the stored hash looks like an old-style (pre-migration) hash.</summary>
        public static bool IsLegacyHash(string storedHash)
            => !string.IsNullOrEmpty(storedHash) && !storedHash.StartsWith(Prefix);

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int bytes)
        {
            using (var prf = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                return prf.GetBytes(bytes);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            uint diff = (uint)(a.Length ^ b.Length);
            for (int i = 0; i < a.Length && i < b.Length; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }
    }
}
