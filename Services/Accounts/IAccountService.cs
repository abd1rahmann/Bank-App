using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Accounts
{
    public interface IAccountService
    {
        List<Account> GetAccounts();
        List<string> GetFrequencies();
        void Update(Account account);
        void Delete(int account);
        Account GetAccount(int id);
        Account CreateAccount(Account account);

    }
}
