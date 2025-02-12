using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ai_Inside.Data;
using Ai_Inside.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ai_Inside.Areas.Identity.Pages.Account.Manage
{
    public class PurchasesModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PurchasesModel(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<License> Purchases { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ActivePage"] = ManageNavPages.Purchases; // ? Ensure this is set
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Purchases = await _context.Licenses
                .Where(l => l.UserId == user.Id)
                .ToListAsync();

            return Page();
        }

    }
}
