using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _DbContext;
        public CustomerService(BankAppDataContext dbContext)
        {
            _DbContext = dbContext;
        }
        public IEnumerable<DataAccessLayer.Models.Customer> GetCustomers()
        {
            return _DbContext.Customers;
        }
        public int SaveNewCustomer(DataAccessLayer.Models.Customer customer)
        {
            _DbContext.Customers.Add(customer);
            _DbContext.SaveChanges();
            return customer.CustomerId;
        }
        public void Update(DataAccessLayer.Models.Customer customer)
        {
            _DbContext.SaveChanges();
        }
        public DataAccessLayer.Models.Customer GetCustomer(int customerId)
        {
            return _DbContext.Customers.First(c => c.CustomerId == customerId);
        }

        public void SoftDelete(int customerId)
        {
            var customer = _DbContext.Customers.First(c => c.CustomerId == customerId);

            if (customer != null)
            {
                customer.IsActive = false;
                _DbContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetCustomerAccounts(DataAccessLayer.Models.Customer customer)
        {

            var accountIds = _DbContext.Dispositions
                .Where(d => d.CustomerId == customer.CustomerId)
                .Select(d => d.AccountId)
                .ToList();

            var customerAccounts = _DbContext.Accounts
                .Where(a => accountIds.Contains(a.AccountId))
                .ToList();

            return customerAccounts;
        }
    }
}
