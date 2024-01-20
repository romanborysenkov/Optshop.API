using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptShopAPI.Models
{
    public class Order
    {
      //  [Key]
      //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 
        public int productId { get; set; }

        public int productCount {  get; set; }

         public string? color {get;set;}

        public string? description { get;set;}

        public string? status { get;set;}

        public int? totalPrice { get;set; }
    }
}
