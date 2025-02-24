namespace CompanyApp.Domain.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public int CompanyId { get; set; } // FK
        public int CountryId { get; set; } //FK
        public Company? Company { get; set; }
        public Country? Country { get; set; }
    }
}
