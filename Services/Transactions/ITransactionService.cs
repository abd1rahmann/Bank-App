using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Transactions
{
    public interface ITransactionService
    {
        List<Transaction> GetAllTransactionsById(int accountId, int skip, int take);

        void DepositMoney(Transaction deposit);
        void WithdrawMoney(Transaction withdraw);
    }
}
