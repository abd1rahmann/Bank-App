using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Pages.Dashboard
{
    [Authorize]
    public class IndexDashboardModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IndexDashboardModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public string UserRole { get; set; }
        public IdentityUser CurrentUser { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userManager.GetUserAsync(User);
            if (CurrentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var roles = await _userManager.GetRolesAsync(CurrentUser);
            UserRole = roles.FirstOrDefault();

            return Page();
        }
    }
}
