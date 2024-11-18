using System.ComponentModel.DataAnnotations;

namespace API_Labb3.Models
{
    public class Person
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();
    }
}
