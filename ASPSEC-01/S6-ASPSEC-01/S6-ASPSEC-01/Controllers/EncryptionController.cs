using Microsoft.AspNetCore.Mvc;
using S6_ASPSEC_01.Models;

namespace S6_ASPSEC_01.Controllers
{
    public class EncryptionController : Controller
    {
        private readonly EncryptionFunctions _encryptionFunctions;
        public EncryptionController()
        {
            _encryptionFunctions = new EncryptionFunctions();
        }

        public IActionResult Encryption()
        {
            return View();
        }
        public IActionResult Decryption()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EncryptMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Error = "Please enter a message to encrypt.";
                return View("Encryption");
            }

            string encryptedMessage = _encryptionFunctions.Encrypt(message);
            ViewBag.OriginalMessage = message;
            ViewBag.EncryptedMessage = encryptedMessage;

            return View("Encryption");
        }

        [HttpPost]
        public IActionResult DecryptMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Error = "Please enter a message to encrypt.";
                return View("Decryption");
            }

            string DecryptedMessage = _encryptionFunctions.Decrypt(message);
            ViewBag.OriginalMessage = message;
            ViewBag.DecryptedMessage = DecryptedMessage;

            return View("Decryption");
        }
    }
}
