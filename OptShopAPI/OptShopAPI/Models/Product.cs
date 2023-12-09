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
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string? name { get; set; }

        [Required]
        [StringLength(50)]
        public string? description { get; set; }

        [Required]
        public int? price { get; set; }

        [Required]
        [StringLength(50)]
        public string? characters { get; set; }

        public string? photoName { get; set; }

        public string? color { get; set; }

        
        [NotMapped]
        [JsonPropertyName("photoFile")]
        [Newtonsoft.Json.JsonConverter(typeof(StreamStringConverter))]
        public MultipartFormDataContent? photoFile { get; set; }

        public string? photoSrc { get;set; }

    }
}
