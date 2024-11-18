using System.ComponentModel.DataAnnotations;

namespace API_Labb3.Models
{
    public class Link
    {
        [Key]
        public int LinkId { get; set; }
        public string Url { get; set; }
        public int PersonId { get; set; }
        public Person? Person { get; set; }
        public int InterestId { get; set; }
        public Interest? Interest { get; set; }
    }
}
