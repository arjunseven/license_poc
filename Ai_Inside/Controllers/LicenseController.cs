using Ai_Inside.Data;
using Ai_Inside.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ai_Inside.Controllers
{
    public class LicenseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LicenseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult ChoosePlan()
        {
            return View();
        }

        // ✅ Fix 1: Add support for GET requests for Purchase
        [HttpGet]
        public async Task<IActionResult> Purchase(string plan, int years)
        {
            if (string.IsNullOrEmpty(plan) || years <= 0)
            {
                return RedirectToAction("ChoosePlan");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Register"); // ✅ Ensures correct redirection
            }

            ViewBag.SelectedPlan = plan;
            ViewBag.Years = years;
            return View(); // ✅ Ensure you have a `Purchase.cshtml` file in `Views/License`
        }

        // ✅ Fix 2: Keep POST method for actual purchase logic
        [HttpPost]
        public async Task<IActionResult> CompletePurchase(string plan, int years)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("/Identity/Account/Register");
            }

            int pricePerYear = plan == "Standard" ? 1000 : 2500;
            int totalPrice = pricePerYear * years;

            var newLicense = new License
            {
                UserId = user.Id,
                Plan = plan,
                Years = years,
                TotalPrice = totalPrice,
                LicenseKey = Guid.NewGuid().ToString("N").ToUpper(),
                ExpiryDate = DateTime.UtcNow.AddYears(years),
                IsActive = true
            };

            _context.Licenses.Add(newLicense);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
