using System.ComponentModel.DataAnnotations;

namespace API_Labb3.Models
{
    public class Interest
    {
        [Key]
        public int InterestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Link> Links { get; set; } = new List<Link>();

    }
}
