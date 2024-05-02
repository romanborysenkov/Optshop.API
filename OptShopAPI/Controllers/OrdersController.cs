using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptShopAPI.Data;
using OptShopAPI.IServices;
using OptShopAPI.Models;
using OptShopAPI.Services;
using Newtonsoft.Json;

namespace OptShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {


        private readonly DataContext _context;
     //   IFilesService _filesService;

        public OrdersController(DataContext context)
        {
            _context = context;

            // uploadPath = configuration.GetValue<string>("UploadPath");

        }

        [HttpPost]
        public async Task<ActionResult<List<Order>>>  PostOrder(List<Order> order)
        {
               List<Product> orderedProducts = new List<Product>();
             foreach(var o in order)
             {
                 Product product = await _context.products.FirstAsync(x => x.id.ToString() == o.productId);
                 orderedProducts.Add(product);
             }

          //  string orderIds = "";
           
            foreach(var o in order)
                {
                
                  var item = _context.orders.Add(o);
                await _context.SaveChangesAsync();

                // orderIds += CreatedAtAction("GetCustomer", new { id = o.Id}, o);
               /* if (i == order.Count)
                {
                    orderIds += o.Id;
                    break;
                }
                 orderIds += o.Id;
                orderIds += " ";
                i++;*/
                }     
            return Ok(order);
           

        }



    }
}
