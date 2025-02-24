namespace CompanyApp.Domain.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; } = string.Empty;

        public int CompanyId { get; set; } //fk
        public Company Company { get; set; }

        public int CountryId { get; set; } //fk
        public Country Country { get; set; }
    }

}
