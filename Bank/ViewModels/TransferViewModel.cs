using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModels
{
    public class TransferViewModel
    {
        [Required(ErrorMessage = "From account is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid from account number")]
        public int FromAccountId { get; set; }

        [Required(ErrorMessage = "To account is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid to account number")]
        public int ToAccountId { get; set; }

        [Required(ErrorMessage = "Amount is required")]
        [Range(1, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public decimal FromAccountBalance { get; set; }
    }
}
