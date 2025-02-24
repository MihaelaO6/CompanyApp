namespace CompanyApp.Domain.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        //1-Many relationship

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
