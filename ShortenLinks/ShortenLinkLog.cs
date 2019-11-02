using System.ComponentModel.DataAnnotations;

namespace ShortenLinks
{
    public class ShortenLinkLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ShortLinkId { get; set; }

        [Required]
        public string LongLink { get; set; }

        public string ShortLink { get; set; }

        public string Origen { get; set; }

        public System.DateTime DateAccess { get; set; }
    }
}