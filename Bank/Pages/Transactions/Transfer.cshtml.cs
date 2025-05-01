using Bank.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using Services.Accounts;
using Services.Customer;
using Services.Transactions;
using System.ComponentModel.DataAnnotations;

namespace Bank.Pages.Transactions
{
    [BindProperties]
    public class TransferModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public required TransferViewModel TransferData { get; set; }
        public List<SelectListItem> AccountList { get; set; } = new();


        public int CustomerId { get; set; }
        public DateOnly TodaysDate { get; set; }

        public TransferModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
            TransferData = new TransferViewModel();
        }

        public void OnGet(int customerId, int accountId)
        {
            TransferData = new TransferViewModel
            {
                FromAccountId = accountId
            };
            CustomerId = customerId;


            TodaysDate = DateOnly.FromDateTime(DateTime.Now);

            var accounts = _accountService.GetAccounts();

            AccountList = accounts
                .Where(a => a.AccountId != accountId)
                .Select(a => new SelectListItem
                {
                    Value = a.AccountId.ToString(),
                    Text = $"{a.AccountId} - {a.Balance} kr"
                })
                .ToList();

        }

        public IActionResult OnPost()
        {
       
            TodaysDate = DateOnly.FromDateTime(DateTime.Now);

            var accounts = _accountService.GetAccounts();
            AccountList = accounts
                .Where(a => a.AccountId != TransferData.FromAccountId)
                .Select(a => new SelectListItem
                {
                    Value = a.AccountId.ToString(),
                    Text = $"{a.AccountId} - {a.Balance} kr"
                })
                .ToList();

            if (TransferData.ToAccountId <= 0)
            {
                ModelState.AddModelError("TransferData.ToAccountId", "Invalid to account number.");
            }

            if (TransferData.FromAccountId <= 0)
            {
                ModelState.AddModelError("TransferData.FromAccountId", "Invalid from account number.");
            }

            if (TransferData.Amount <= 0)
            {
                ModelState.AddModelError("TransferData.Amount", "Amount must be greater than 0.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var fromAccount = _accountService.GetAccount(TransferData.FromAccountId);
            var toAccount = _accountService.GetAccount(TransferData.ToAccountId);

            

            if (toAccount == null)
            {
                ModelState.AddModelError("TransferData.ToAccountId", "The account you are trying to transfer to does not exist.");
            }

            if (fromAccount == null)
            {
                ModelState.AddModelError("TransferData.FromAccountId", "The from account does not exist.");
            }

            if (fromAccount != null && TransferData.Amount > fromAccount.Balance)
            {
                ModelState.AddModelError("TransferData.Amount", "Insufficient balance.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            fromAccount!.Balance -= TransferData.Amount;
            toAccount!.Balance += TransferData.Amount;

            _accountService.Update(fromAccount);
            _accountService.Update(toAccount);

            _transactionService.DepositMoney(new Transaction
            {
                AccountId = toAccount.AccountId,
                Amount = TransferData.Amount,
                Balance = toAccount.Balance,
                Date = TodaysDate,
                Type = "Credit",
                Operation = "Transfer",
                Bank = "EasyBank"
            });

            _transactionService.WithdrawMoney(new Transaction
            {
                AccountId = fromAccount.AccountId,
                Amount = TransferData.Amount,
                Balance = fromAccount.Balance,
                Date = TodaysDate,
                Type = "Debit",
                Operation = "Transfer",
                Bank = "EasyBank"
            });

            TempData["SuccessMessage"] = "Transfer successful!";
            return RedirectToPage("/Transactions/Transaction", new { accountId = fromAccount.AccountId, customerId = CustomerId });
        }
    }
}
