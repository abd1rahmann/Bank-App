using Bank.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Customers;

namespace Bank.Pages.Customers
{
    [Authorize]
    public class CustomersModel : PageModel
    {
        private readonly ICustomersService _customerService;

        public CustomersModel(ICustomersService customerService)
        {
            _customerService = customerService;
        }

        public List<CustomersViewModel> customers { get; set; }
        public int CurrentPage { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int CustomerId { get; set; }
        public string SearchBox { get; set; }
        public void OnGet(int customerId, string sortColumn, string sortOrder, int pageNo, string searchBox, bool IsActive)
        {
            CurrentPage = pageNo == 0 ? 1 : pageNo;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            SearchBox = searchBox;

            customers = _customerService.GetCustomers(sortColumn, sortOrder, pageNo, searchBox)
            .Select(s => new CustomersViewModel
            {
                Id = s.CustomerId,
                Name = s.Givenname,
                City = s.City,
                StreetAddress = s.Streetaddress,
                Country = s.Country,
                NationalId = s.NationalId,
            }).ToList();


        }
    }
}
