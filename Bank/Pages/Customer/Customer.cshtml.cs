using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Accounts;
using Services.Customer;
using Services.Transactions;

namespace Bank.Pages.Customer
{
    public class CustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionsService;
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerAdress { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerZipcode { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerMail { get; set; }
        public string? NationalId { get; set; }
        public string Gender { get; set; }
        public decimal TotalBalance { get; set; }
        public int AccountId { get; set; }
        public List<Account> CustomerAccounts { get; set; }
        public Disposition Disposition { get; set; }
        public CustomerModel(ICustomerService customerService, IAccountService accountService, ITransactionService transationsService)
        {
            _customerService = customerService;
            _accountService = accountService;
            _transactionsService = transationsService;
        }

        public void OnGet(int id, int accountId)
        {
            var customer = _customerService.GetCustomer(id);

            CustomerId = customer.CustomerId;
            CustomerFirstName = customer.Givenname;
            CustomerLastName = customer.Surname;
            CustomerAdress = customer.Streetaddress;
            CustomerCity = customer.City;
            CustomerZipcode = customer.Zipcode;
            CustomerCountry = customer.Country;
            CustomerMail = customer.Emailaddress;
            NationalId = customer.NationalId;
            Gender = customer.Gender;


            CustomerAccounts = _customerService.GetCustomerAccounts(customer).ToList();

            TotalBalance = CustomerAccounts.Sum(account => account.Balance);
        }

        public IActionResult OnPostSoftDeleteCustomer(int id)
        {
            _customerService.SoftDelete(id);
            return RedirectToPage("/Customers/Customers");
        }

        public IActionResult OnPostDeleteAccount(int accountId, int id)
        {
            _accountService.Delete(accountId);

            return RedirectToPage("/Customer/Customer", new { id = id });
        }


    }
}
