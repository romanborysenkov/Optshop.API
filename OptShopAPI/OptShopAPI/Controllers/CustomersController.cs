using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptShopAPI.Data;
using OptShopAPI.Models;
using OptShopAPI.Services;

namespace OptShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Customers
     /*   [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
          if (_context.customers == null)
          {
              return NotFound();
          }
            return await _context.customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
          if (_context.customers == null)
          {
              return NotFound();
          }
            var customer = await _context.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
     */

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            List<string> ordersId = new List<string>();

           
            var payment = _context.payments.First(x=>x.Id == Guid.Parse(customer.orderIds));
            ordersId = StringService.SplitString(payment.orderids);

            List<Product> orderedProducts = new List<Product>();

            List<Order>orderedProductsIdsAndCount = new List<Order>();

            foreach(var item in ordersId)
            {
                orderedProductsIdsAndCount.Add(_context.orders.First(x => x.Id == Guid.Parse(item)));
                //orderedProducts.Add(_context.products.First(x=>x.id == int.Parse(item)));
            }

            foreach(var item in orderedProductsIdsAndCount)
            {
                orderedProducts.Add(_context.products.First(x=>x.id == item.productId));
            }

           string message = ShapingMessage.InfoAboutCustomerAndOrdered(customer, orderedProducts, orderedProductsIdsAndCount);

            BotService service = new BotService();
            service.SendingDataAboutCustomer(message, customer.orderIds);
            

            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

      

        // DELETE: api/Customers/5
      /*  [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
      */

        private bool CustomerExists(int id)
        {
            return (_context.customers?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
