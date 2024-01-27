namespace OptShopAPI.Models
{
    public class PaymentUpdate
    {
        public string Id { get; set; }  
        public int DeliveryPrice { get; set; }
        public string PaymentId {  get; set; }

        public PaymentUpdate( int deliveryPrice, string paymentId)
        {
            DeliveryPrice = deliveryPrice;
            PaymentId = paymentId;
        }
    }
}
