using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bank.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<IndexModel> _logger;
        private readonly BankAppDataContext _dbContext;
        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalSum { get; set; }

        public IndexModel(ILogger<IndexModel> logger, BankAppDataContext dbContext, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _signInManager = signInManager;

        }
        public IActionResult OnGet()
        {
            TotalCustomers = _dbContext.Customers.Count();
            TotalAccounts = _dbContext.Accounts.Count();
            TotalSum = _dbContext.Transactions.Sum(t => t.Balance);


            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToPage("/Dashboard/IndexDashboard");
            }
            else
            {
                return Page();
            }
        }


    }
}
