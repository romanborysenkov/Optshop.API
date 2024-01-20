using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OptShopAPI.Data;
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

        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            payment.Id = Guid.NewGuid();
            var p = _context.payments.Add(payment);
           await _context.SaveChangesAsync();

            return payment;
        }
    }
}
