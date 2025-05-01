using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public TransactionService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public List<DataAccessLayer.Models.Transaction> GetAllTransactionsById(int accountId, int skip, int take)
        {
            return _bankAppDataContext.Set<DataAccessLayer.Models.Transaction>()
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Skip(skip)
                .Take(take)
                .ToList();
        }

        public void DepositMoney(DataAccessLayer.Models.Transaction deposit)
        {
            if (deposit == null)
                throw new ArgumentNullException(nameof(deposit));

            _bankAppDataContext.Transactions.Add(deposit);
            _bankAppDataContext.SaveChanges();
        }

        public void WithdrawMoney(DataAccessLayer.Models.Transaction withdraw)
        {
            if (withdraw == null)
                throw new ArgumentNullException(nameof(withdraw));

            withdraw.Amount = -Math.Abs(withdraw.Amount);

            _bankAppDataContext.Transactions.Add(withdraw);
            _bankAppDataContext.SaveChanges();
        }
    }
}
