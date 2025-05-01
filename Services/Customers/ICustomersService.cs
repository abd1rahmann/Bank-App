using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Customers
{
    public interface ICustomersService
    {
        List<DataAccessLayer.Models.Customer> GetCustomers(string sortColumn, string sortOrder, int pageNo, string searchBox, bool IsActive = true);
    }
}
