using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModels
{
    public class NewCustomerViewModel
    {

        public int id { get; set; }
        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(50)]
        [Required]
        public string LastName { get; set; }

        [StringLength(50)]
        [Required]
        public string StreetAdress { get; set; }

        [StringLength(10)]
        [Required]
        public string PostalCode { get; set; }

        [StringLength(50)]
        [Required]
        public string City { get; set; }

        [StringLength(50)]
        [Required]
        public string Country { get; set; }

        [StringLength(2)]
        public string CountryCode { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Birthday { get; set; }

        [StringLength(150)]
        [EmailAddress]
        [Required]
        public string Emailaddress { get; set; }


        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
    }
}
