using api.Data;
using api.Dtos.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _context.Stocks.ToListAsync();
            var stocksDto = stocks.Select(stock => stock.ToDto()).ToList();

            if (stocksDto == null || !stocksDto.Any())
            {
                return NotFound("No stocks found.");
            }

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound($"Stock with ID {id} not found.");
            }

            return Ok(stock.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockCreateDto stockDto)
        {
            var stock = stockDto.ToStockFromRequest();
            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { Id = stock.Id }, stock.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] StockUpdateDto stockUpdateDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

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

            await _context.SaveChangesAsync();

            return Ok(stock.ToDto());
        }

        [HttpDelete]
        [Route("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if(stock == null)
            {
                return NotFound($"Stock with id {id} isn't found that u are trying to delete");
            }

            _context.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
