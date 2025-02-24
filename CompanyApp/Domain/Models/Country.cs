using System.Text.Json.Serialization;

namespace CompanyApp.Domain.Models
{
    public class Country
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        [JsonIgnore]
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    }
}
