using System.ComponentModel.DataAnnotations;

namespace Bank.ViewModels
{
    public class WithdrawViewModel
    {
        [Range(100, 10000)]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
