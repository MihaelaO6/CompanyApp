namespace CompanyApp.Domain.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        //1-Many relationship
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
