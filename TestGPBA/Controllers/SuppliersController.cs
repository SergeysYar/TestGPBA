using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;


namespace TestGPBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SuppliersController(AppDbContext context)
        {
            _context = context;
        }

        // Метод со списком популярных поставщиков
        [HttpGet("popular")]
        public async Task<ActionResult<IEnumerable<object>>> GetPopularSuppliers()
        {
            var suppliers = await _context.Suppliers
                .Select(s => new
                {
                    s.Name,
                    OfferCount = s.Offers.Count
                })
                .OrderByDescending(s => s.OfferCount)
                .Take(3)
               .ToListAsync();

            return Ok(suppliers);
        }
    }
}
