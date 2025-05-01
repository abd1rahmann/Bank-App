using Bank.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Accounts;
using Services.Customer;

namespace Bank.Pages.Accounts
{
    [BindProperties]

    public class CreateNewAccountModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly BankAppDataContext _bankAppDataContext;


        public CreateNewAccountModel(IAccountService accountService,
            ICustomerService customerService,
            IHttpContextAccessor contextAccessor,
            BankAppDataContext bankAppDataContext)
        {
            _accountService = accountService;
            _customerService = customerService;
            _contextAccessor = contextAccessor;
            _bankAppDataContext = bankAppDataContext;
        }

        public AccountViewModel newAccount { get; set; }
        public int CustomerId { get; set; }
        public List<SelectListItem> Frequencies { get; set; }

        public void OnGet(int id)
        {
            var customer = _customerService.GetCustomer(id);
            CustomerId = customer.CustomerId;

            Frequencies = _accountService.GetFrequencies()
                .ConvertAll(c => new SelectListItem
                {
                    Text = c,
                    Value = c
                });

        }

        public IActionResult OnPost(int customerId)
        {
            if (ModelState.IsValid)
            {

                var newAcc = new Account
                {
                    Balance = newAccount.Balance,
                    Frequency = newAccount.Frequency,
                    Created = DateOnly.FromDateTime(DateTime.Now)
                };

                var createdAcc = _accountService.CreateAccount(newAcc);

                var newdisposition = new Disposition
                {
                    CustomerId = CustomerId,
                    AccountId = createdAcc.AccountId,
                    Type = "Owner"
                };

                _bankAppDataContext.Accounts.Update(newAcc);
                _bankAppDataContext.Dispositions.Add(newdisposition);
                _bankAppDataContext.SaveChanges();

                TempData["Message"] = "New account created successfully!";

                return RedirectToPage("/Customer/Customer", new { id = customerId });

            }

            return Page();

        }
    }
}
