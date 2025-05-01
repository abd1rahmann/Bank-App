using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModels
{
    public class EditCustomerViewModel
    {
        public int Id { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string StreetAdress { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Birthday { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string? Emailaddress { get; set; }


        public string Gender { get; set; }
    }
}
