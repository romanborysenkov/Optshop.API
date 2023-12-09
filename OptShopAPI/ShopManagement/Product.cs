using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using ShopManagemant;

namespace ShopManagement
{
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


        [JsonPropertyName("photoFile")]
        [Newtonsoft.Json.JsonConverter(typeof(StreamStringConverter))]
        public MultipartFormDataContent photoFile { get; set; }

        public string? photoSrc { get; set; }
    }
}
