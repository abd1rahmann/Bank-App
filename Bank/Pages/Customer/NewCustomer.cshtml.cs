using Bank.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Countries;
using Services.Customer;
using Services.Gender;

namespace Bank.Pages.Customer
{
    [BindProperties]
    public class NewCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICountriesService _countriesService;
        private readonly IGenderService _genderService;

        public NewCustomerModel(ICustomerService customerService, ICountriesService countriesService, IGenderService genderService)
        {
            _customerService = customerService;
            _countriesService = countriesService;
            _genderService = genderService;
        }
        public NewCustomerViewModel CustomerVm { get; set; }
        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> TwoGenders { get; set; }

        public void OnGet()
        {
            Countries = _countriesService.GetAllCountries()
                .ConvertAll(c => new SelectListItem
                {
                    Text = c,
                    Value = c
                });

            TwoGenders = _genderService.GetBothGenders()
                .ConvertAll(g => new SelectListItem
                {
                    Text = g,
                    Value = g
                });
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var customer = new DataAccessLayer.Models.Customer
                {
                    CustomerId = CustomerVm.id,
                    Givenname = CustomerVm.FirstName,
                    Surname = CustomerVm.LastName,
                    Streetaddress = CustomerVm.StreetAdress,
                    Zipcode = CustomerVm.PostalCode,
                    City = CustomerVm.City,
                    Country = CustomerVm.Country,
                    CountryCode = CustomerVm.CountryCode,
                    Emailaddress = CustomerVm.Emailaddress,
                    Birthday = CustomerVm.Birthday,
                    Gender = CustomerVm.Gender,
                    IsActive = true
                };
                _customerService.SaveNewCustomer(customer);

                TempData["SuccessMessage"] = "New customer created successfully!";
                return RedirectToPage("/Customers/Customers");
            }
            return Page();
        }
    }
}
