using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock/")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToDto());

            if (stocks == null || !stocks.Any())
            {
                return NotFound("No stocks found.");
            }

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);

            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }

            return Ok(stock.ToDto());
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] StockCreateDto stockDto)
        {
            var stock = stockDto.ToStockFromRequest();
            _context.Add(stock);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { Id = stock.Id }, stock.ToDto());
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] StockUpdateDto stockUpdateDto)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found that you are trying to update.");
            }

            stock.Symbol = stockUpdateDto.Symbol;
            stock.CompanyName = stockUpdateDto.CompanyName;
            stock.Purchase = stockUpdateDto.Purchase;
            stock.LastDiv = stockUpdateDto.LastDiv;
            stock.Industry = stockUpdateDto.Industry;
            stock.MarketCap = stockUpdateDto.MarketCap;

            _context.Stocks.Update(stock);
            _context.SaveChanges();

            return Ok(stock.ToDto());
        }
    }
}
