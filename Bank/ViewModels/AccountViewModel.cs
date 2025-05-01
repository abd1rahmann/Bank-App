using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to deposit some money when creating an account")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive number")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Please select a frequency.")]
        public string Frequency { get; set; }
        public DateOnly Created { get; set; }
    }
}
