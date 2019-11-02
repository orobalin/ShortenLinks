using System.ComponentModel.DataAnnotations;

namespace ShortenLinks
{
    public class ShortenLink
    {
        [Key]
        public string Id { get; set; }

        [RegularExpression(@"\b(\S+)\s?")]
        [Required]
        public string LongLink { get; set; }
                
        public string ShortLink { get; set; }

        public System.DateTime DateCreated { get; set; }

        public int AccessCount { get; set; }
    }
}