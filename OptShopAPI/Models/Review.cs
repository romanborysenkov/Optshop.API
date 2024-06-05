using System.ComponentModel.DataAnnotations;

namespace OptShopAPI.Models
{
    public class Review
    {
        public Guid Id { get; set; }

        [Required]
        public string GmailOwner{ get; set; }
        public string Name {  get; set; }

        public string? Description { get; set; }

        [Required]
        public byte StarCount { get; set; }
    }
}
