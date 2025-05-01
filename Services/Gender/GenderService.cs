using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Gender
{
    public class GenderService : IGenderService
    {
        private readonly BankAppDataContext _dbContext;

        public GenderService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<string> GetBothGenders()
        {
            var gender = _dbContext.Customers
                .Select(c => c.Gender)
                .Distinct()
                .ToList();
            return gender;
        }
    }
}
