using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptShopAPI.Data;
using OptShopAPI.Migrations;
using OptShopAPI.Models;

namespace OptShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DataContext _context;

        public PaymentController(DataContext context)
        {
            _context = context;
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
            var p = _context.payments.Add(payment);
           await _context.SaveChangesAsync();

            return payment;
        }

        [HttpPut]
        public async Task<ActionResult<Payment>> UpdatePayment(PaymentUpdate deliveryPrice)
        {
            var payment = _context.payments.First(x=>x.Id == Guid.Parse(deliveryPrice.PaymentId));
            payment.deliveryPrice = deliveryPrice.DeliveryPrice;
            payment.Status = Models.PaymentStatus.WaitForPay;
            _context.payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }
    }
}
