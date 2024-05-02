using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptShopAPI.Models
{

    public enum PaymentStatus
    {
        Prepaid, WaitForPay, Success
    }
    public class Payment
    {
       // [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string? orderids { get; set; }

        public int totalPrice { get; set; }

        public int alreadyPaid { get; set; }

        public int deliveryPrice { get; set; }

        public PaymentStatus Status {  get; set; } = PaymentStatus.Prepaid;

        public string? ownerId { get; set; }

        public string? cardInfo { get; set;}

    }
}
