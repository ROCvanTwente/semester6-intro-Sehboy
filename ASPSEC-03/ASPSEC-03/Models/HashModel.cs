using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace HashingApp.Models
{
    public class HashModel
    {
        [Required(ErrorMessage = "Voer een tekst in om te hashen.")]
        public string InputText { get; set; }

        [Required(ErrorMessage = "Kies een hash-algoritme.")]
        public string Algorithm { get; set; }

        public string HashedResult { get; set; }

        public void ComputeHash()
        {
            if (string.IsNullOrEmpty(InputText) || string.IsNullOrEmpty(Algorithm))
                return;

            byte[] inputBytes = Encoding.UTF8.GetBytes(InputText);
            byte[] hashBytes = null;

            switch (Algorithm.ToUpperInvariant())
            {
                case "MD5":
                    using (MD5 md5 = MD5.Create())
                        hashBytes = md5.ComputeHash(inputBytes);
                    break;
                case "SHA1":
                    using (SHA1 sha1 = SHA1.Create())
                        hashBytes = sha1.ComputeHash(inputBytes);
                    break;
                case "SHA256":
                    using (SHA256 sha256 = SHA256.Create())
                        hashBytes = sha256.ComputeHash(inputBytes);
                    break;
                case "SHA512":
                    using (SHA512 sha512 = SHA512.Create())
                        hashBytes = sha512.ComputeHash(inputBytes);
                    break;
                case "BCRYPT":
                    HashedResult = BCrypt.Net.BCrypt.HashPassword(InputText);
                    return;
                default:
                    HashedResult = "Ongeldig algoritme";
                    return;
            }

            HashedResult = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }
}
