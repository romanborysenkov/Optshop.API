using OptShopAPI.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OptShopAPI.Models
{
    [Table("products")]
    public class Product
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public string? OriginalLink { get; set; }

        [Required]
        [StringLength(200)]
        public string? name { get; set; }

        [Required]
        
        public string? description { get; set; }

        [Required]
        public double? price { get; set; }

        [Required]
       
        public string? characters { get; set; }

        public string? photoName { get; set; }

        public string? color { get; set; }

        public int minimalCount { get; set; }

        public string? size { get; set; }

        
        [NotMapped]
        [JsonPropertyName("photoFile")]
        [Newtonsoft.Json.JsonConverter(typeof(StreamStringConverter))]
        public MultipartFormDataContent? photoFile { get; set; }

        public string? photoSrc { get;set; }

    }
}
