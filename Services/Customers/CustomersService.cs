using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace Services.Customers
{
    public class CustomersService : ICustomersService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomersService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<DataAccessLayer.Models.Customer> GetCustomers(string sortColumn, string sortOrder, int pageNo, string searchBox, bool isActive = true)
        {
            var query = _dbContext.Customers
                .Where(c => c.IsActive == isActive)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchBox))
            {
                if (int.TryParse(searchBox, out int customerId))
                {
                    query = query.Where(s => s.CustomerId == customerId);
                }
                else
                {
                    query = query.Where(s =>
                        s.Givenname.Contains(searchBox) ||
                        s.City.Contains(searchBox) ||
                        s.Country.Contains(searchBox));
                }
            }

            query = (sortColumn, sortOrder) switch
            {
                ("Name", "desc") => query.OrderByDescending(s => s.Givenname),
                ("Name", _) => query.OrderBy(s => s.Givenname),

                ("Country", "desc") => query.OrderByDescending(s => s.Country),
                ("Country", _) => query.OrderBy(s => s.Country),

                ("City", "desc") => query.OrderByDescending(s => s.City),
                ("City", _) => query.OrderBy(s => s.City),

                ("NationalId", "desc") => query.OrderByDescending(s => s.NationalId),
                ("NationalId", _) => query.OrderBy(s => s.NationalId),

                ("StreetAddress", "desc") => query.OrderByDescending(s => s.Streetaddress),
                ("StreetAddress", _) => query.OrderBy(s => s.Streetaddress),

                _ => query.OrderBy(s => s.CustomerId) 
            };

            int pageSize = 50;
            pageNo = pageNo <= 0 ? 1 : pageNo;
            int skip = (pageNo - 1) * pageSize;

            return query.Skip(skip).Take(pageSize).ToList();
        }

    }
}
