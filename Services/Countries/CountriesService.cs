using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Countries
{
    public class CountriesService : ICountriesService
    {
        private readonly BankAppDataContext _dbContext;

        public CountriesService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<string> GetAllCountries()
        {
            var countries = _dbContext.Customers
                .Select(c => c.Country)
                .Distinct()
                .ToList();
            return countries;
        }
    }
}
