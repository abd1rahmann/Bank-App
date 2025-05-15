using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public AccountService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }
        public Account CreateAccount(Account account)
        {
            _bankAppDataContext.Accounts.Add(account);
            _bankAppDataContext.SaveChanges();
            return account;
        }
        public List<Account> GetAccounts()
        {
            return _bankAppDataContext.Accounts.ToList();
        }
        public Account GetAccount(int accountId)
        {
            var account = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);

            if (account == null)
            {
                throw new Exception($"Account with ID {accountId} was not found.");
            }

            return account;
        }
        public void Update(Account account)
        {
            _bankAppDataContext.SaveChanges();
        }

        public List<string> GetFrequencies()
        {
            var frequencies = _bankAppDataContext.Accounts
                .Select(c => c.Frequency)
                .Distinct()
                .ToList();
            return frequencies;
        }
        public void Delete(int accountId)
        {
            var accountToDelete = _bankAppDataContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);
            if (accountToDelete != null)
            {
                var dispositionAccToDelete = _bankAppDataContext.Dispositions.Where(d => d.AccountId == accountId).ToList();
                foreach (var disp in dispositionAccToDelete)
                {
                    _bankAppDataContext.Dispositions.Remove(disp);
                }

                if (dispositionAccToDelete.Any())
                {
                    _bankAppDataContext.SaveChanges();
                }
            }
        }
    }
}
