using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestGPBA.Model;

namespace TestGPBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OffersController(AppDbContext context)
        {
            _context = context;
        }

        // Метод создания оффера
        [HttpPost]
        public async Task<ActionResult<Offer>> CreateOffer([FromBody] Offer offer)
        {
            if (offer == null)
            {
                return BadRequest();
            }

            _context.Offers.Add(offer);
            object value = await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOffer), new { id = offer.Id }, offer);
        }

        // Метод поиска офферов
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Offer>>> SearchOffers(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Query parameter cannot be empty.");
            }

            var offers = await _context.Offers
                .Include(o => o.Supplier)
                .Where(o => o.Brand.Contains(query) || o.Model.Contains(query) || o.Supplier.Name.Contains(query))
                .ToListAsync();

            var result = new
            {
                Offers = offers,
                TotalCount = offers.Count()
            };

            return Ok(result);
        }

        // Дополнительный метод для получения оффера по ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOffer(int id)
        {
            var offer = await _context.Offers
                .Include(o => o.Supplier)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (offer == null)
            {
                return NotFound();
            }

            return offer;
        }
    }
}
