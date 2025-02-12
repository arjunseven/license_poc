using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ai_Inside.Areas.Identity.Pages.Account
{
    public class LicenseModel : PageModel
    {
        [BindProperty]
        public string SelectedPlan { get; set; }

        public void OnGet()
        {
            // Page load logic (if any)
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(SelectedPlan))
            {
                ModelState.AddModelError("", "Please select a plan.");
                return Page();
            }

            // Placeholder: License Purchase Logic will be added later
            return RedirectToPage("/Account/License");
        }
    }
}