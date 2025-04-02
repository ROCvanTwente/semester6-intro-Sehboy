using Microsoft.AspNetCore.Mvc;
using HashingApp.Models;

namespace HashingApp.Controllers
{
    public class HashController : Controller
    {
        [HttpGet]
        public IActionResult Crypt()
        {
            return View(new HashModel()); // Zorgt ervoor dat Model nooit null is
        }

        [HttpPost]
        public IActionResult Crypt([FromBody] HashModel model)
        {
            if (string.IsNullOrEmpty(model.InputText) || string.IsNullOrEmpty(model.Algorithm))
            {
                return BadRequest(new { success = false, message = "Vul alle velden in!" });
            }

            model.ComputeHash();

            return Json(new
            {
                success = true,
                input = model.InputText,
                algorithm = model.Algorithm,
                hash = model.HashedResult
            });
        }

    }
}
