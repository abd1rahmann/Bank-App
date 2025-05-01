using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModels
{
    public class DepositViewModel
    {
        [Range(100, 10000, ErrorMessage = "Must be att least 100 och max 10000")]
        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
        public DateOnly DepositDate { get; set; }

        [MinLength(5, ErrorMessage =
            "Comment must be at least 5 characters long")]
        [MaxLength(250, ErrorMessage =
            "WOOOW to many Characters here")]
        public string? Comment { get; set; }

    }
}
