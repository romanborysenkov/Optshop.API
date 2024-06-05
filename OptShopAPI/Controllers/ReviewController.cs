using Microsoft.AspNetCore.Mvc;
using OptShopAPI.Data;
using OptShopAPI.Models;
using OptShopAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OptShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {

        private readonly DataContext _context;

        public ReviewController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<ReviewController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var result = _context.reviews.ToList();

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpGet("/Middle")]
        public ActionResult<int> GetMiddleReview()
        {
            var reviews = _context.reviews.ToList();
            int MiddleReview = 0;
            foreach(Review review in reviews)
            {
                MiddleReview += review.StarCount;
            }
            MiddleReview = MiddleReview/reviews.Count;
            return Ok(MiddleReview);
        }

        // POST api/<ReviewController>
        [HttpPost]
        public async Task<ActionResult<Review>> Post(Review review)
        {
            review.Id = Guid.NewGuid();
            _context.reviews.Add(review);
          await  _context.SaveChangesAsync();

            return Ok();
        }

        // PUT api/<ReviewController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReviewController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
