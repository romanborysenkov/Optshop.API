using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptShopAPI.Data;
using OptShopAPI.Models;
using OptShopAPI.IServices;
using OptShopAPI.Services;
using System.Configuration;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace OptShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DataContext _context;
        IFilesService _filesService;

        public ProductsController(DataContext context, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _context = context;
            _filesService = new FilesService(webHostEnvironment);

           // uploadPath = configuration.GetValue<string>("UploadPath");

        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.products == null)
          {
              return NotFound();
          }
            return await _context.products.ToListAsync();
        }

        [HttpGet("/api/Find/{name}/{desc}/{count}")]
        public  async Task<ActionResult<IEnumerable<Product>>> GetFoundProducts(string name = "", string desc = "highest", string count="12")
        {
            if(_context.products == null)
            {
                return NotFound();
            }

            List<Product> result = new List<Product>();
            if (desc == "highest")
            {
                 result = _context.products
                    .Where(x => x.name.ToLower().Contains(name.ToLower()) ||
                    EF.Functions.Like(x.name.ToLower(), name.ToLower() + "%") ||
                    EF.Functions.Like(x.name.ToLower(), "%"+name.ToLower()) || 
                    EF.Functions.Like(x.name.ToLower(), "%"+name.ToLower()+"%"))
                    .Take(int.Parse(count))
                    .OrderByDescending(p => p.price).ToList();//from product in _context.products where product.name == name select product;
            }

            if (desc == "lowest")
            {
                result = _context.products.Where(x => x.name.ToLower().Contains(name.ToLower()) ||
                    EF.Functions.Like(x.name.ToLower(), name.ToLower() + "%") ||
                    EF.Functions.Like(x.name.ToLower(), "%" + name.ToLower()) ||
                    EF.Functions.Like(x.name.ToLower(), "%" + name.ToLower() + "%")).Take(int.Parse(count)).OrderBy(p => p.price).ToList();//from product in _context.products where product.name == name select product;

                //result.Reverse();
            }


            if (result.Count() > 0)
                {
                    foreach(var r in result)
                        {
                          var firstImage =StringService.SplitString(r.photoSrc);
                          r.photoSrc = firstImage.First();
                        }
                    return result.ToList();
                }
                else
                {
                    return NotFound(name);
                }
           
        }


        [HttpGet("{query}")]
        public ActionResult<IEnumerable<string>> FindingOffers(string query)
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            // var result = from product in _context.products where product.name == query select product.name;
            var result = from p in _context.products where p.name.ToLower().Contains(query.ToLower()) select p.name;
            if (result.Count() > 0)
            {
                return result.Distinct().ToList();
            }
            else
            {
                return NotFound(query);
            }
        }

        [HttpGet("api/Prod/{keyword}")]
        public ActionResult<IEnumerable<Product>> Recomendations(string keyword)
        {
            if(_context.products == null)
            {
                return NotFound();
            }
            List<Product>result = new List<Product>();
            if(keyword == "undefined")
            {
                
                result = _context.products.Take(12).ToList(); 
            }else{
              
             result = _context.products.Where(x => x.name.ToLower().Contains(keyword.ToLower()) ||
                    EF.Functions.Like(x.name.ToLower(), keyword.ToLower() + "%") ||
                    EF.Functions.Like(x.name.ToLower(), "%" + keyword.ToLower()) ||
                    EF.Functions.Like(x.name.ToLower(), "%" + keyword.ToLower() + "%"))
                    .Take(12).ToList(); 
            }

            if(result.Count() > 0) {
                foreach (var r in result)
                {
                    var firstImage = StringService.SplitString(r.photoSrc);
                    r.photoSrc = firstImage.First();
                }
                return result;
            }
            else { return NotFound(keyword); }
        }


        // GET: api/Products/5
        [HttpGet("/api/Product/{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.products == null)
          {
              return NotFound();
          }
            var product = await _context.products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }
            
            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<Product>> PutProduct([FromBody] Product product)
        {

            var lastproduct = await _context.products.FirstAsync(x=>x.id == product.id);

            if (string.IsNullOrEmpty(product.photoName))
                product.photoSrc = lastproduct.photoSrc;
            else
                product.photoSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, product.photoSrc);

            _context.products.Update(product);
            await _context.SaveChangesAsync();
            return product;

        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
           var names = StringService.SplitString(product.photoName);
            product.photoSrc = "";
            foreach(var name in names)
            {
                product.photoSrc += String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, name) + "   ";
            }
           //  product.photoSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, product.photoName);
            

            _context.products.Add(product);
            await _context.SaveChangesAsync();
            return product;

        }

        [HttpPost("{UploadFile}")]
        public async Task<IActionResult> PostImage()
        {
          var httpRequest = HttpContext.Request;
          
          await _filesService.SaveFile(httpRequest.Form.Files[0]);
         
            return Ok();
        }
    


        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.products == null)
            {
                return NotFound();
            }
            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.products?.Any(e => e.id == id)).GetValueOrDefault();
        }

      
    }
}
