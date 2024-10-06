using System.ComponentModel.DataAnnotations;

namespace InstagramLink.Models
{
    public class OnPageLink
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }
    }
}
