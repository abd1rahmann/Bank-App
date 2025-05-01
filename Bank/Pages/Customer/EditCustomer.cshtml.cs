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
    public class EditCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;
        private readonly ICountriesService _countriesService;
        private readonly IGenderService _genderService;
        public EditCustomerViewModel CustomerVm { get; set; }
        public EditCustomerModel(ICustomerService customerService, ICountriesService CountriesService, IGenderService genderService)
        {
            _customerService = customerService;
            _countriesService = CountriesService;
            _genderService = genderService;
        }

        public List<SelectListItem> Countries { get; set; }
        public List<SelectListItem> TwoGenders { get; set; }

        public void OnGet(int id)
        {
            var customerDb = _customerService.GetCustomer(id);
            if (customerDb == null)
            {
                RedirectToPage("/NotFound");
                return;
            }


            CustomerVm = new EditCustomerViewModel
            {
                Id = customerDb.CustomerId,
                FirstName = customerDb.Givenname,
                LastName = customerDb.Surname,
                Emailaddress = customerDb.Emailaddress,
                StreetAdress = customerDb.Streetaddress,
                PostalCode = customerDb.Zipcode,
                City = customerDb.City,
                Country = customerDb.Country,
                CountryCode = customerDb.CountryCode,
                Birthday = customerDb.Birthday,
                Gender = customerDb.Gender,
            };

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

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var custDb = _customerService.GetCustomer(id);

                custDb.CustomerId = id;
                custDb.Givenname = CustomerVm.FirstName;
                custDb.Surname = CustomerVm.LastName;
                custDb.Emailaddress = CustomerVm.Emailaddress;
                custDb.Streetaddress = CustomerVm.StreetAdress;
                custDb.Zipcode = CustomerVm.PostalCode;
                custDb.City = CustomerVm.City;
                custDb.Country = CustomerVm.Country;
                custDb.CountryCode = CustomerVm.CountryCode;
                custDb.Birthday = CustomerVm.Birthday;

                _customerService.Update(custDb);

                TempData["SuccessMessage"] = "Customer updated successfully!";
                return RedirectToPage("/Customer/Customer", new { id = custDb.CustomerId });
            }

            return Page();
        }



    }
}
