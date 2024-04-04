using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OptShopAPI.Data;
//using OptShopAPI.Migrations;
using OptShopAPI.Models;
using OptShopAPI.Services;

namespace OptShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DataContext _context;
        OrdersBotService botService;

        public PaymentController(DataContext context)
        {
            _context = context;
            this.botService = new OrdersBotService();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(string id)
        {
            var payment = _context.payments.First(x=>x.Id == Guid.Parse(id));
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            payment.Id = Guid.NewGuid();
         

            List<string> ordersId = new List<string>();

            ordersId = StringService.SplitString(payment.orderids);

            List<Product> orderedProducts = new List<Product>();

            List<Order> orderedProductsIdsAndCount = new List<Order>();

            foreach (var item in ordersId)
            {
                orderedProductsIdsAndCount.Add(_context.orders.First(x => x.Id == Guid.Parse(item)));
                //orderedProducts.Add(_context.products.First(x=>x.id == int.Parse(item)));
            }

            foreach (var item in orderedProductsIdsAndCount)
            {
                orderedProducts.Add(_context.products.First(x => x.id == item.productId));
            }
            var customer = _context.customers.First(x=>x.id == int.Parse(payment.ownerId));
            string message = ShapingMessage.InfoAboutCustomerAndOrdered(customer, orderedProducts, orderedProductsIdsAndCount);

            botService.SendingDataAboutCustomer(message, payment.Id.ToString());

            _context.payments.Add(payment);
            await _context.SaveChangesAsync();

            return Ok(payment);
        }

        [HttpPut]
        public async Task<ActionResult<Payment>> UpdatePayment(PaymentUpdate deliveryPrice)
        {
            
            var payment = _context.payments.First(x=>x.Id == Guid.Parse(deliveryPrice.PaymentId));

            if (payment.deliveryPrice == 0)
            {
                payment.Status = PaymentStatus.WaitForPay;
            }
            else { 
                payment.Status = PaymentStatus.Success;
            }
            payment.deliveryPrice = deliveryPrice.DeliveryPrice;
           
            if (payment.Status == Models.PaymentStatus.Success)
            {
                payment.alreadyPaid = payment.totalPrice;
                OrdersBotService botService = new OrdersBotService();
                string message = ShapingMessage.OrderList(payment.orderids, _context);
                botService.SendingDataAboutCustomer(message);
            }
            _context.payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

       
    }
}
