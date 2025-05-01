using Bank.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Accounts;
using Services.Transactions;

namespace Bank.Pages.Transactions
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionsService;
        public DepositViewModel DepositData { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public DateOnly todaysDate { get; set; }
        public DepositModel(IAccountService accountService, ITransactionService transationsService)
        {
            _accountService = accountService;
            _transactionsService = transationsService;
        }
        public void OnGet(int accountId, int customerId)
        {
            AccountId = accountId;
            CustomerId = customerId;
            todaysDate = DateOnly.FromDateTime(DateTime.Now.AddHours(1));


            var account = _accountService.GetAccount(accountId);
            if (account != null)
            {
                DepositData = new DepositViewModel
                {
                    Balance = account.Balance
                };
            }


        }
        public IActionResult OnPost(int accountId)
        {
            if (todaysDate < DateOnly.FromDateTime(DateTime.Now))
            {
                ModelState.AddModelError("DepositData.DepositDate", "Cannot Deposit money in the past");
            }

            if (ModelState.IsValid)
            {
                var accountDb = _accountService.GetAccount(accountId);

                accountDb.Balance += DepositData.Amount;
                _accountService.Update(accountDb);


                var transaction = new Transaction
                {
                    AccountId = accountId,
                    Date = todaysDate,
                    Type = "Credit",
                    Operation = "Deposit",
                    Amount = DepositData.Amount,
                    Balance = accountDb.Balance,
                    Symbol = "Currency Symbol",
                    Bank = "CHB"
                };

                _transactionsService.DepositMoney(transaction);
                return RedirectToPage("/Transactions/Transaction", new { accountId = AccountId, customerId = CustomerId });
            }
            return Page();
        }
    }
}
