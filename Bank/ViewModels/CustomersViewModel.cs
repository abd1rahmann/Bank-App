namespace Bank.ViewModels
{
    public class CustomersViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string? NationalId { get; set; }
        public int CurrentPage { get; set; }
        public bool IsActive { get; set; }
    }
}
